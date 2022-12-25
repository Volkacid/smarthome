package main

import (
	"github.com/Volkacid/smarthome/handlers"
	"github.com/go-chi/chi/v5"
	"log"
	"net/http"
)

func main() {
	router := chi.NewRouter()
	router.Route("/", func(r chi.Router) {
		router.Get("/", handlers.MainPage)
		router.Post("/rgb", handlers.PostRGB)
	})
	log.Fatal(http.ListenAndServe("localhost:80", router))
	/*udpServer, err := net.ListenPacket("udp", ":5001")
	if err != nil {
		fmt.Println(err)
	}
	for {
		buf := make([]byte, 5)
		_, addr, err := udpServer.ReadFrom(buf)
		if err != nil {
			fmt.Println()
		}
		fmt.Println(buf, addr)
	}*/
}
