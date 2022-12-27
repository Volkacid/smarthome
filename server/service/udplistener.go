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
		switch udpData[0] { //Arduino control byte
		case 251:
			stripePorts.WriteSerial(udpData)
			break
		case 250:
			stripePorts.EffectsOverflow(int(udpData[2]), int(udpData[3]), int(udpData[4]), int(udpData[1]))
			break
		case 249:
			stripePorts.EffectsPulse(int(udpData[2]), int(udpData[3]), int(udpData[4]), int(udpData[1]))
			break
		}
	}
}
