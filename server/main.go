package main

import (
	"context"
	"fmt"
	"github.com/Volkacid/smarthome/handlers"
	"github.com/Volkacid/smarthome/service"
	"github.com/go-chi/chi/v5"
	"go.bug.st/serial"
	"log"
	"net/http"
)

func main() {
	ports, err := serial.GetPortsList()
	if err != nil {
		log.Fatal(err)
	}
	if len(ports) == 0 {
		log.Fatal("No serial ports found!")
	}
	for _, port := range ports {
		fmt.Printf("Found port: %v\n", port)
	}
	stripePorts := service.OpenStripePorts()
	go service.StartUDPService(stripePorts)
	defer stripePorts.CloseStripePorts()
	alarmService := service.StartAlarmService(15, 15, stripePorts)
	defer alarmService.StopAlarmService()
	ctx, cancel := context.WithCancel(context.Background())
	router := chi.NewRouter()
	router.Route("/", func(r chi.Router) {
		router.Get("/", handlers.MainPage(alarmService))
		router.Post("/", handlers.PostRGB(stripePorts, ctx, cancel))
		router.Get("/alarm", handlers.AlarmClockPage(alarmService))
		router.Post("/alarm", handlers.SetAlarm(alarmService, stripePorts))
	})
	log.Fatal(http.ListenAndServe(":80", router))
}
