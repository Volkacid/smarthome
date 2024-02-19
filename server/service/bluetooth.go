package service

import (
	"github.com/Volkacid/smarthome/util"
	"golang.org/x/sys/unix"
	"log"
	"syscall"
)

type BluetoothSockets struct {
	kitchenDown int
	kitchenUp   int
}

func OpenBluetoothSockets() *BluetoothSockets {
	log.Println("Initializing bluetooth...")

	macKitchenDown := str2ba("98:D3:33:F5:A4:42") // TODO: from config
	macKitchenUp := str2ba("98:D3:71:F5:ED:A7")

	log.Println("Creating fd1 socket")
	fd1, err := unix.Socket(syscall.AF_BLUETOOTH, syscall.SOCK_STREAM, unix.BTPROTO_RFCOMM)
	util.CheckFatal(err)
	addr := &unix.SockaddrRFCOMM{Addr: macKitchenDown, Channel: 1}
	log.Println("connecting fd1...")
	err = unix.Connect(fd1, addr)
	util.CheckFatal(err)
	defer unix.Close(fd1)
	log.Println("fd1 done")

	fd2, err := unix.Socket(syscall.AF_BLUETOOTH, syscall.SOCK_STREAM, unix.BTPROTO_RFCOMM)
	util.CheckFatal(err)
	addr = &unix.SockaddrRFCOMM{Addr: macKitchenUp, Channel: 1}
	log.Println("connecting fd2...")
	err = unix.Connect(fd2, addr)
	util.CheckFatal(err)
	defer unix.Close(fd2)
	log.Println("fd2 done")

	log.Println("Bluetooth initialized")

	return &BluetoothSockets{kitchenDown: fd1, kitchenUp: fd2}
}

func (b *BluetoothSockets) WriteStripe(data []byte) {
	var err error

	switch data[1] { //Stripe control byte
	case BothStripes:
		data[1] = 1
		_, err = unix.Write(b.kitchenDown, data)
		util.CheckFatal(err)
		_, err = unix.Write(b.kitchenUp, data)
		util.CheckFatal(err)
		break
	case KitchenDownStripe:
		data[1] = 1
		_, err = unix.Write(b.kitchenDown, data)
		util.CheckFatal(err)
		break
	case KitchenUpStripe:
		data[1] = 1
		_, err = unix.Write(b.kitchenUp, data)
		util.CheckFatal(err)
		break
	}
}
