package service

import (
	"context"
	"fmt"
	"log"
	"net"
)

func StartUDPService(bSockets *BluetoothSockets) {
	log.Println("Starting UDP service...")

	udpServer, err := net.ListenPacket("udp", ":5001") //TODO: from config
	if err != nil {
		log.Fatal("UDP service starting error: ", err)
	}
	ctx, cancel := context.WithCancel(context.Background())
	log.Println("UDP service started")

	for {
		udpData := make([]byte, 5)
		_, _, err := udpServer.ReadFrom(udpData)
		if err != nil {
			fmt.Println("UDP receiver error: ", err)
		}
		fmt.Println("UDP data: ", udpData)
		cancel()
		switch udpData[0] { //Arduino control byte
		case 255: //TODO: from config
			ctx, cancel = context.WithCancel(context.Background())
			go bSockets.WriteStripe(udpData)
			break
		case 250:
			ctx, cancel = context.WithCancel(context.Background())
			go bSockets.EffectsOverflow(udpData[2], udpData[3], udpData[4], int(udpData[1]), ctx)
			break
		case 249:
			ctx, cancel = context.WithCancel(context.Background())
			go bSockets.EffectsPulse(udpData[2], udpData[3], udpData[4], int(udpData[1]), ctx)
			break
		}
	}
}
