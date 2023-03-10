package main

import (
	"context"
	"fmt"
	"github.com/Volkacid/smarthome/handlers"
	"github.com/Volkacid/smarthome/service"
	"github.com/go-chi/chi/v5"
	"github.com/go-chi/chi/v5/middleware"
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
	fmt.Println("UDP service started")
	defer stripePorts.CloseStripePorts()
	controlPorts := service.OpenControlPorts()
	defer controlPorts.CloseControlPorts()
	go controlPorts.StartClimateService()
	alarmService := service.StartAlarmService(9, 15, stripePorts)
	defer alarmService.StopAlarmService()
	ctx, cancel := context.WithCancel(context.Background())
	router := chi.NewRouter()
	router.Route("/", func(r chi.Router) {
		router.Mount("/debug", middleware.Profiler())
		router.Get("/", handlers.MainPage(alarmService, controlPorts))
		router.Post("/", handlers.PostRGB(stripePorts, ctx, cancel))
		router.Get("/alarm", handlers.AlarmClockPage(alarmService))
		router.Post("/alarm", handlers.SetAlarm(alarmService, stripePorts))
	})
	log.Fatal(http.ListenAndServe(":80", router))
}
