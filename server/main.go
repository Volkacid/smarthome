package main

import (
	"context"
	"github.com/Volkacid/smarthome/handlers"
	"github.com/Volkacid/smarthome/service"
	"github.com/go-chi/chi/v5"
	"github.com/go-chi/chi/v5/middleware"
	"log"
	"net/http"
	"net/http/pprof"
	_ "net/http/pprof"
)

func main() {
	bSockets := service.OpenBluetoothSockets()
	defer bSockets.CloseSockets()

	go service.StartUDPService(bSockets)

	//controlPorts := service.OpenControlPorts()
	//defer controlPorts.CloseControlPorts()
	//go controlPorts.StartClimateService()

	alarmService := service.StartAlarmService(9, 15, bSockets)
	defer alarmService.StopAlarmService()

	ctx, cancel := context.WithCancel(context.Background())
	router := chi.NewRouter()
	router.Route("/", func(r chi.Router) {
		router.Mount("/debug", middleware.Profiler())
		router.Get("/", handlers.MainPage(alarmService))
		router.Post("/", handlers.PostRGB(bSockets, ctx, cancel))
		router.Get("/alarm", handlers.AlarmClockPage(alarmService))
		router.Post("/alarm", handlers.SetAlarm(alarmService, bSockets))
		router.HandleFunc("/debug/pprof/", pprof.Index)
		router.HandleFunc("/debug/pprof/cmdline", pprof.Cmdline)
		router.HandleFunc("/debug/pprof/profile", pprof.Profile)
		router.HandleFunc("/debug/pprof/symbol", pprof.Symbol)
		router.HandleFunc("/debug/pprof/trace", pprof.Trace)
	})
	addr := ":8080"
	log.Println("Listening on", addr)
	log.Fatal(http.ListenAndServe(addr, router))
}
