package service

import (
	"context"
	"fmt"
	"strconv"
	"time"
)

type AlarmClock struct {
	AlHour int
	AlMin  int
	ports  *StripePorts
	ctx    context.Context
	cancel context.CancelFunc
}

func StartAlarmService(alHour int, alMin int, stripePorts *StripePorts) *AlarmClock {
	ctx, cancel := context.WithCancel(context.Background())
	alarmClock := &AlarmClock{AlHour: alHour, AlMin: alMin, ports: stripePorts, ctx: ctx, cancel: cancel}
	go alarmClock.timeChecker()
	fmt.Printf("Setting up alarm service on %v:%v \n", alHour, alMin)
	return alarmClock
}

func (ac *AlarmClock) StopAlarmService() {
	ac.cancel()
	data := make([]byte, 5)
	data[0] = 251
	ac.ports.WriteSerial(data)
}

func (ac *AlarmClock) timeChecker() {
	for {
		select {
		case <-ac.ctx.Done():
			return
		default:
			curHour, curMin, _ := time.Now().Clock()
			if curHour == ac.AlHour && (curMin-ac.AlMin) >= 0 {
				go ac.ports.EffectsAlarm(ac.ctx)
				fmt.Println("Alarm clock!")
				time.Sleep(1 * time.Hour)
			}
			time.Sleep(1 * time.Minute)
		}
	}
}

func (ac *AlarmClock) GetAlarmTime() string {
	alarmTime := ""
	if ac.AlHour < 10 {
		alarmTime += "0"
	}
	alarmTime += strconv.Itoa(ac.AlHour) + ":"
	if ac.AlMin < 10 {
		alarmTime += "0"
	}
	alarmTime += strconv.Itoa(ac.AlMin)
	return alarmTime
}
