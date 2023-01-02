package service

import (
	"fmt"
	"go.bug.st/serial"
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
	climatePort serial.Port
	climateData string
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

func (cp *ControlPorts) StartClimateService(climateData string) {
	for {
		buf := make([]byte, 30)
		n, err := cp.climatePort.Read(buf)
		if err != nil {
			fmt.Println("Climate port reading error: ", err)
			return
		}
		cp.climateData = string(buf[:n])
		//fmt.Println(string(buf[:n]))
	}
}

func (cp *ControlPorts) GetClimateData() (string, string) {
	climateNow := cp.climateData
	if climateNow == "" {
		return "0", "0"
	}
	_, hum, _ := strings.Cut(climateNow, "Hum")
	hum, _, _ = strings.Cut(hum, "Temp")
	_, temp, _ := strings.Cut(climateNow, "Temp")
	hum += "%"
	temp += "°C"
	return hum, temp
}
