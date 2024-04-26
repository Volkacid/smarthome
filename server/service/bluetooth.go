package service

import (
	"context"
	"github.com/Volkacid/smarthome/util"
	"golang.org/x/sys/unix"
	"log"
	"sync"
	"syscall"
	"time"
)

type BluetoothSockets struct {
	kitchenDown    int
	kitchenDownCh  chan []byte
	kitchenDownMac [6]byte
	kitchenUp      int
	kitchenUpCh    chan []byte
	kitchenUpMac   [6]byte
}

func OpenBluetoothSockets() *BluetoothSockets {
	log.Println("Initializing bluetooth...")
	time.Sleep(5 * time.Second) //Safe reboot delay

	sockets := &BluetoothSockets{
		kitchenDownCh:  make(chan []byte, 30),
		kitchenDownMac: util.Str2ba("98:D3:41:F5:E7:70"), // TODO: from config
		kitchenUpCh:    make(chan []byte, 30),
		kitchenUpMac:   util.Str2ba("98:D3:71:F5:ED:A7"), // TODO: from config
	}

	var fd1, fd2 int
	wg := &sync.WaitGroup{}
	wg.Add(2)

	go func() {
		var err error
		log.Println("Creating kitchenDown socket")
		defer wg.Done()

		fd1, err = unix.Socket(syscall.AF_BLUETOOTH, syscall.SOCK_STREAM, unix.BTPROTO_RFCOMM)
		util.CheckFatal(err)
		addr := &unix.SockaddrRFCOMM{Addr: sockets.kitchenDownMac, Channel: 1}

		log.Println("connecting kitchenDown...")
		err = unix.Connect(fd1, addr)
		util.CheckFatal(err)

		log.Println("kitchenDown done")
	}()

	go func() {
		var err error
		log.Println("Creating kitchenUp socket")
		defer wg.Done()

		fd2, err = unix.Socket(syscall.AF_BLUETOOTH, syscall.SOCK_STREAM, unix.BTPROTO_RFCOMM)
		util.CheckFatal(err)
		addr := &unix.SockaddrRFCOMM{Addr: sockets.kitchenUpMac, Channel: 1}

		log.Println("connecting kitchenUp...")
		err = unix.Connect(fd2, addr)
		util.CheckFatal(err)

		log.Println("kitchenUp done")
	}()

	wg.Wait()

	sockets.kitchenDown = fd1
	sockets.kitchenUp = fd2

	sockets.queueReadWorker(context.Background())
	log.Println("Bluetooth initialized")

	return sockets
}

func (b *BluetoothSockets) reconnect(fd int, deviceMac [6]byte) {
	unix.Close(fd)

	var newFd int
	var err error

	for i := 1; i <= 10; i++ {
		time.Sleep(5 * time.Second)

		newFd, err = unix.Socket(syscall.AF_BLUETOOTH, syscall.SOCK_STREAM, unix.BTPROTO_RFCOMM)
		if err != nil {
			log.Printf("Bluetooth reconnect failed: device - %s, attempt -  %d, err - %s", util.Ba2str(deviceMac), i, err.Error())
			continue
		}

		addr := &unix.SockaddrRFCOMM{Addr: deviceMac, Channel: 1}
		err = unix.Connect(newFd, addr)
		if err != nil {
			log.Printf("Bluetooth reconnect failed: device - %s, attempt -  %d, err - %s", util.Ba2str(deviceMac), i, err.Error())
			continue
		}

		break
	}

	if err != nil {
		log.Fatal("Bluetooth reconnect failed")
	}

	if deviceMac == b.kitchenDownMac { //TODO: rewrite
		b.kitchenDown = newFd
	}
	if deviceMac == b.kitchenUpMac {
		b.kitchenUp = newFd
	}

	log.Printf("Bluetooth reconnect: device - %s reconnected successfully", util.Ba2str(deviceMac))
}

func (b *BluetoothSockets) CloseSockets() {
	unix.Close(b.kitchenDown)
	close(b.kitchenDownCh)
	unix.Close(b.kitchenUp)
	close(b.kitchenUpCh)
}

func (b *BluetoothSockets) queueReadWorker(ctx context.Context) {
	go func() {
		readBuf := make([]byte, 5)
		for {
			select {
			case data := <-b.kitchenDownCh:
				if len(b.kitchenDownCh) > 1 { //Begin smoothing if there are problems with connection
					readBuf = <-b.kitchenDownCh
					if data[0] == readBuf[0] && data[1] == readBuf[1] {
						util.AverageColors(data, readBuf)
					} else { //Sending messages separately if control bytes is different
						_, err := unix.Write(b.kitchenDown, data)
						if err != nil {
							b.reconnect(b.kitchenDown, b.kitchenDownMac)
						}
						data = readBuf
					}
				}
				_, err := unix.Write(b.kitchenDown, data)
				if err != nil {
					b.reconnect(b.kitchenDown, b.kitchenDownMac)
				}

				if len(b.kitchenDownCh) > 0 {
					log.Printf("kitchenDownCh len: %d", len(b.kitchenDownCh))
				}
				break
			case <-ctx.Done():
				return
			}
		}
	}()

	go func() {
		readBuf := make([]byte, 5)
		for {
			select {
			case data := <-b.kitchenUpCh:
				if len(b.kitchenUpCh) > 1 { //Begin smoothing if there are problems with connection
					readBuf = <-b.kitchenUpCh
					if data[0] == readBuf[0] && data[1] == readBuf[1] {
						util.AverageColors(data, readBuf)
					} else { //Sending messages separately if control bytes is different
						_, err := unix.Write(b.kitchenUp, data)
						if err != nil {
							b.reconnect(b.kitchenDown, b.kitchenDownMac)
						}
						data = readBuf
					}
				}
				_, err := unix.Write(b.kitchenUp, data)
				if err != nil {
					b.reconnect(b.kitchenUp, b.kitchenUpMac)
				}

				if len(b.kitchenUpCh) > 0 {
					log.Printf("kitchenUpCh len: %d", len(b.kitchenUpCh))
				}
				break
			case <-ctx.Done():
				return
			}
		}
	}()

	log.Println("Bluetooth queue worker started")
}

//var writeBuf = make([]byte, 5)

func (b *BluetoothSockets) QueueWrite(data []byte) {
	/*if data[0] == EffectSolid && util.SliceEqual(data, writeBuf) { //Remove duplicated commands from streams
		return
	}
	writeBuf = data*/

	switch data[1] { //Stripe index byte
	case BothStripes:

		go func() {
			select {
			case b.kitchenDownCh <- data:
				break
			case <-time.After(time.Second):
				log.Println("Timeout writing to kitchenDownCh")
				break
			}
		}()

		go func() {
			select {
			case b.kitchenUpCh <- data:
				break
			case <-time.After(time.Second):
				log.Println("Timeout writing to kitchenUpCh")
				break
			}
		}()

		break
	case KitchenDownStripe:
		go func() {
			select {
			case b.kitchenDownCh <- data:
				break
			case <-time.After(time.Second):
				log.Println("Timeout writing to kitchenDownCh")
				break
			}
		}()

		break
	case KitchenUpStripe:
		go func() {
			select {
			case b.kitchenUpCh <- data:
				break
			case <-time.After(time.Second):
				log.Println("Timeout writing to kitchenUpCh")
				break
			}
		}()

		break
	}
}
