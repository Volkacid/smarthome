package service

import (
	"fmt"
	"go.bug.st/serial"
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
}

func OpenStripePorts() *StripePorts {
	portMode := &serial.Mode{BaudRate: 38400}
	bedPort, err := serial.Open("/dev/ttyUSB0", portMode)
	if err != nil {
		fmt.Println("Bed port error: ", err)
	}
	tablePort, err := serial.Open("/dev/ttyUSB1", portMode)
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
	climatePort, err := serial.Open("/dev/ttyUSB2", portMode)
	if err != nil {
		fmt.Println("Climate port error: ", err)
	}
	return &ControlPorts{climatePort: climatePort}
}

func (cp *ControlPorts) CloseControlPorts() {
	cp.climatePort.Close()
}

func (cp *ControlPorts) ReadSerial() {
	buf := make([]byte, 30)
	n, err := cp.climatePort.Read(buf)
	if err != nil {
		fmt.Println("Climate port reading error: ", err)
		return
	}
	fmt.Println(buf[:n])
}
