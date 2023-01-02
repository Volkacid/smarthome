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
		_, hum, _ = strings.Cut(hum, "Humidity: ")
		hum, _, _ = strings.Cut(hum, "% ")
		_, temp, _ = strings.Cut(temp, "Temperature: ")
		temp, _, _ = strings.Cut(temp, "°C")
		humidity, humErr := strconv.ParseFloat(hum, 64)
		temperature, tempErr := strconv.ParseFloat(temp, 64)
		if humErr == nil && tempErr == nil {
			if humidity < 40.0 && humidity > 0.0 && !cp.isHumidifierEnabled {
				cp.WriteControl(CommHumidifierEnable)
				cp.isHumidifierEnabled = true
				fmt.Println("Humidifier enabled")
			}
			if humidity > 50.0 && cp.isHumidifierEnabled {
				cp.WriteControl(CommHumidifierDisable)
				cp.isHumidifierEnabled = false
				fmt.Println("Humidifier disabled")
			}
			if temperature < 20.0 && temperature > 0.0 && !cp.isHeaterEnabled {
				cp.WriteControl(CommHeaterEnable)
				cp.isHeaterEnabled = true
				fmt.Println("Heater enabled")
			}
			if temperature > 26.0 && cp.isHeaterEnabled {
				cp.WriteControl(CommHeaterDisable)
				cp.isHeaterEnabled = false
				fmt.Println("Heater disabled")
			}
			if temperature > 27.0 && !cp.isConditionerEnabled {
				cp.WriteControl(CommConditionerEnable)
				cp.isConditionerEnabled = true
				fmt.Println("Conditioner enabled")
			}
			if temperature < 23.0 && temperature > 0.0 && cp.isConditionerEnabled {
				cp.WriteControl(CommConditionerDisable)
				cp.isConditionerEnabled = false
				fmt.Println("Conditioner disabled")
			}
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
	temp, _, _ = strings.Cut(temp, "\r\n")
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
