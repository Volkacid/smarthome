package service

import (
	"fmt"
	"go.bug.st/serial"
)

//const bedPort = "COM4"
//const tablePort = "COM5"

type StripePorts struct {
	bedPort   serial.Port
	tablePort serial.Port
}

func OpenStripePorts() *StripePorts {
	portMode := &serial.Mode{BaudRate: 38400}
	bedPort, err := serial.Open("COM4", portMode)
	if err != nil {
		fmt.Println("Bed port error: ", err)
	}
	tablePort, err := serial.Open("COM5", portMode)
	if err != nil {
		fmt.Println("Table port error: ", err)
	}
	return &StripePorts{bedPort: bedPort, tablePort: tablePort}
}

func (sp *StripePorts) CloseStripePorts() {
	sp.bedPort.Close()
	sp.tablePort.Close()
}

func (sp *StripePorts) WriteSerial(data []byte) {
	sp.bedPort.Write(data)
	sp.tablePort.Write(data)
}
