package service

import (
	"fmt"
	"go.bug.st/serial"
	"strconv"
	"strings"
)

const (
	BothStripes = iota
	BedStripe
	TableStripe
)

type StripePorts struct {
	bedPort   serial.Port
	tablePort serial.Port
}

type ControlPorts struct {
	climatePort          serial.Port
	climateData          string
	isHumidifierEnabled  bool
	isHeaterEnabled      bool
	isConditionerEnabled bool
}

func OpenStripePorts() *StripePorts {
	portMode := &serial.Mode{BaudRate: 38400}
	bedPort, err := serial.Open("/dev/ttyUSB1", portMode)
	if err != nil {
		fmt.Println("Bed port error: ", err)
	}
	tablePort, err := serial.Open("/dev/ttyUSB2", portMode)
	if err != nil {
		fmt.Println("Table port error: ", err)
	}
	return &StripePorts{bedPort: bedPort, tablePort: tablePort}
}

func (sp *StripePorts) CloseStripePorts() {
	sp.bedPort.Close()
	sp.tablePort.Close()
}

func (sp *StripePorts) WriteStripe(data []byte) {
	switch data[1] { //Stripe control byte
	case BothStripes:
		sp.bedPort.Write(data)
		sp.tablePort.Write(data)
		return
	case BedStripe:
		data[1] = byte(0)
		sp.bedPort.Write(data)
		return
	case TableStripe:
		data[1] = byte(0)
		sp.tablePort.Write(data)
		return
	}
}

const (
	CommHumidifierEnable   = byte(255)
	CommHumidifierDisable  = byte(254)
	CommHeaterEnable       = byte(253)
	CommHeaterDisable      = byte(252)
	CommConditionerEnable  = byte(251)
	CommConditionerDisable = byte(250)
)

func OpenControlPorts() *ControlPorts {
	portMode := &serial.Mode{BaudRate: 38400}
	climatePort, err := serial.Open("/dev/ttyUSB0", portMode)
	if err != nil {
		fmt.Println("Climate port error: ", err)
	}
	return &ControlPorts{climatePort: climatePort}
}

func (cp *ControlPorts) CloseControlPorts() {
	cp.climatePort.Close()
}

func (cp *ControlPorts) StartClimateService() {
	for {
		buf := make([]byte, 30)
		n, err := cp.climatePort.Read(buf)
		if err != nil {
			fmt.Println("Climate port reading error: ", err)
			return
		}
		cp.climateData = string(buf[:n])
		hum, temp := cp.GetClimateData()
		humidity, _ := strconv.ParseFloat(hum, 64)
		temperature, _ := strconv.ParseFloat(temp, 64)
		if humidity < 50.0 && !cp.isHumidifierEnabled {
			cp.WriteControl(CommHumidifierEnable)
			cp.isHumidifierEnabled = true
		}
		if humidity > 50.0 && cp.isHumidifierEnabled {
			cp.WriteControl(CommHumidifierDisable)
			cp.isHumidifierEnabled = false
		}
		if temperature < 20.0 && !cp.isHeaterEnabled {
			cp.WriteControl(CommHeaterEnable)
			cp.isHeaterEnabled = true
		}
		if temperature > 26.0 && cp.isHeaterEnabled {
			cp.WriteControl(CommHeaterDisable)
			cp.isHeaterEnabled = false
		}
		if temperature > 27.0 && !cp.isConditionerEnabled {
			cp.WriteControl(CommConditionerEnable)
			cp.isConditionerEnabled = true
		}
		if temperature < 23.0 && cp.isConditionerEnabled {
			cp.WriteControl(CommConditionerDisable)
			cp.isConditionerEnabled = false
		}
	}
}

func (cp *ControlPorts) GetClimateData() (string, string) {
	climateNow := cp.climateData
	if climateNow == "" {
		return "No data", "No data"
	}
	_, hum, _ := strings.Cut(climateNow, "Hum")
	hum, _, _ = strings.Cut(hum, "Temp")
	_, temp, _ := strings.Cut(climateNow, "Temp")
	hum = "Humidity: " + hum + "% "
	temp = "Temperature: " + temp + "°C"
	return hum, temp
}

func (cp *ControlPorts) WriteControl(commandType byte) {
	data := make([]byte, 3)
	data[0] = 254 //Control byte
	data[2] = commandType
	cp.climatePort.Write(data)
}
