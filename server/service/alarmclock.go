package service

import (
	"context"
	"time"
)

type AlarmClock struct {
	alHour int
	alMin  int
	ports  *StripePorts
	ctx    context.Context
	cancel context.CancelFunc
}

func StartAlarmService(alHour int, alMin int, stripePorts *StripePorts) *AlarmClock {
	ctx, cancel := context.WithCancel(context.Background())
	alarmClock := &AlarmClock{alHour: alHour, alMin: alMin, ports: stripePorts, ctx: ctx, cancel: cancel}
	go alarmClock.timeChecker()
	return alarmClock
}

func (ac *AlarmClock) StopAlarmService() {
	ac.cancel()
}

func (ac *AlarmClock) timeChecker() {
	for {
		select {
		case <-ac.ctx.Done():
			return
		default:
			curHour, curMin, _ := time.Now().Clock()
			if curHour == ac.alHour && (curMin-ac.alMin) >= 0 {
				go ac.ports.EffectsAlarm(ac.ctx)
				time.Sleep(1 * time.Hour)
			}
			time.Sleep(1 * time.Minute)
		}
	}
}
