package service

import (
	"fmt"
	"net"
)

func StartUDPService(stripePorts *StripePorts) {
	udpServer, err := net.ListenPacket("udp", ":5001")
	if err != nil {
		fmt.Println("UDP service starting error: ", err)
	}
	for {
		udpData := make([]byte, 5)
		_, _, err := udpServer.ReadFrom(udpData)
		if err != nil {
			fmt.Println("UDP receiver error: ", err)
		}
		fmt.Println("UDP data: ", udpData)
		stripePorts.WriteSerial(udpData)
	}
}
