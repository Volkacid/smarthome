package service

import (
	"context"
	"github.com/Volkacid/smarthome/util"
	"golang.org/x/sys/unix"
	"log"
	"runtime"
	"sync"
	"syscall"
	"time"
)

type BluetoothSockets struct {
	kitchenDown   int
	kitchenDownCh chan []byte
	kitchenUp     int
	kitchenUpCh   chan []byte
}

func OpenBluetoothSockets() *BluetoothSockets {
	log.Println("Initializing bluetooth...")

	macKitchenDown := util.Str2ba("98:D3:33:F5:A4:42") // TODO: from config
	macKitchenUp := util.Str2ba("98:D3:71:F5:ED:A7")

	var fd1, fd2 int
	wg := &sync.WaitGroup{}
	wg.Add(2)

	go func() {
		var err error
		log.Println("Creating fd1 socket")
		defer wg.Done()

		//fd1, err = unix.Socket(syscall.AF_BLUETOOTH, syscall.SOCK_STREAM, unix.BTPROTO_RFCOMM)
		fd1, err = unix.Socket(syscall.AF_BLUETOOTH, syscall.SOCK_DGRAM, unix.BTPROTO_RFCOMM)
		util.CheckFatal(err)
		addr := &unix.SockaddrRFCOMM{Addr: macKitchenDown, Channel: 1}

		log.Println("connecting fd1...")
		err = unix.Connect(fd1, addr)
		util.CheckFatal(err)

		log.Println("fd1 done")
	}()

	go func() {
		var err error
		log.Println("Creating fd2 socket")
		defer wg.Done()

		fd2, err = unix.Socket(syscall.AF_BLUETOOTH, syscall.SOCK_STREAM, unix.BTPROTO_RFCOMM)
		util.CheckFatal(err)
		addr := &unix.SockaddrRFCOMM{Addr: macKitchenUp, Channel: 1}

		log.Println("connecting fd2...")
		err = unix.Connect(fd2, addr)
		util.CheckFatal(err)

		log.Println("fd2 done")
	}()

	wg.Wait()

	sockets := &BluetoothSockets{
		kitchenDown:   fd1,
		kitchenDownCh: make(chan []byte, 100),
		kitchenUp:     fd2,
		kitchenUpCh:   make(chan []byte, 100),
	}
	sockets.QueueReadWorker(context.Background())
	log.Println("Bluetooth initialized")

	return sockets
}

func (b *BluetoothSockets) CloseSockets() {
	unix.Close(b.kitchenDown)
	close(b.kitchenDownCh)
	unix.Close(b.kitchenUp)
	close(b.kitchenUpCh)
}

func (b *BluetoothSockets) QueueReadWorker(ctx context.Context) {
	go func() {
		for {
			select {
			case data := <-b.kitchenDownCh:
				_, err := unix.Write(b.kitchenDown, data)
				util.CheckFatal(err)
				log.Println("kitchenDown write ok")
				break
			case <-ctx.Done():
				return
			}
		}
	}()

	go func() {
		for {
			select {
			case data := <-b.kitchenUpCh:
				_, err := unix.Write(b.kitchenUp, data)
				util.CheckFatal(err)
				log.Println("kitchenUp write ok")
				break
			case <-ctx.Done():
				return
			}
		}
	}()

	log.Println("Bluetooth queue worker started")
}

func (b *BluetoothSockets) QueueWrite(data []byte) {
	switch data[1] { //Stripe index byte
	case BothStripes:
		data[1] = 1

		go func() {
			select {
			case b.kitchenDownCh <- data:
				break
			case <-time.After(2 * time.Second):
				log.Println("Timeout writing to kitchenDownCh")
				break
			}
		}()

		go func() {
			select {
			case b.kitchenUpCh <- data:
				break
			case <-time.After(2 * time.Second):
				log.Println("Timeout writing to kitchenUpCh")
				break
			}
		}()

		break
	case KitchenDownStripe:
		data[1] = 1

		go func() {
			select {
			case b.kitchenDownCh <- data:
				break
			case <-time.After(2 * time.Second):
				log.Println("Timeout writing to kitchenDownCh")
				break
			}
		}()

		break
	case KitchenUpStripe:
		data[1] = 1

		go func() {
			select {
			case b.kitchenUpCh <- data:
				break
			case <-time.After(2 * time.Second):
				log.Println("Timeout writing to kitchenUpCh")
				break
			}
		}()

		break
	}
}

func (b *BluetoothSockets) WriteStripe(data []byte) {
	b.QueueWrite(data)
	log.Printf("STATISTICS: active goroutines - %d", runtime.NumGoroutine())
	/*var err error

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
	}*/
}
