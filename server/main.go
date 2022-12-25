package main

import (
	"fmt"
	"net"
	"time"
)

func main() {
	/*router := chi.NewRouter()
	router.Route("/", func(r chi.Router) {
		router.Get("/", handlers.MainPage)
		router.Post("/rgb", handlers.PostRGB)
	})
	log.Fatal(http.ListenAndServe("localhost:80", router))*/
	udpServer, err := net.ListenPacket("udp", ":5001")
	if err != nil {
		fmt.Println(err)
	}
	for {
		buf := make([]byte, 5)
		_, addr, err := udpServer.ReadFrom(buf)
		if err != nil {
			fmt.Println()
		}
		//go response(udpServer, addr, buf)
		fmt.Println(buf, addr)
	}
}

func response(udpServer net.PacketConn, addr net.Addr, buf []byte) {
	fmt.Println("Received!", buf)
	time := time.Now().Format(time.ANSIC)
	responseStr := fmt.Sprintf("time received: %v. Your message: %v!", time, string(buf))

	udpServer.WriteTo([]byte(responseStr), addr)
}
