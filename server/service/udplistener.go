package service

import (
	"context"
	"fmt"
	"net"
)

func StartUDPService(stripePorts *StripePorts) {
	udpServer, err := net.ListenPacket("udp", ":5001")
	if err != nil {
		fmt.Println("UDP service starting error: ", err)
	}
	ctx, cancel := context.WithCancel(context.Background())
	for {
		udpData := make([]byte, 5)
		_, _, err := udpServer.ReadFrom(udpData)
		if err != nil {
			fmt.Println("UDP receiver error: ", err)
		}
		fmt.Println("UDP data: ", udpData)
		cancel()
		switch udpData[0] { //Arduino control byte
		case 251:
			ctx, cancel = context.WithCancel(context.Background())
			stripePorts.WriteSerial(udpData)
			break
		case 250:
			ctx, cancel = context.WithCancel(context.Background())
			go stripePorts.EffectsOverflow(int(udpData[2]), int(udpData[3]), int(udpData[4]), int(udpData[1]), ctx)
			break
		case 249:
			ctx, cancel = context.WithCancel(context.Background())
			go stripePorts.EffectsPulse(int(udpData[2]), int(udpData[3]), int(udpData[4]), int(udpData[1]), ctx)
			break
		}
	}
}
