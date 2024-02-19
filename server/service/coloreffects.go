package service

import (
	"context"
	"math"
	"time"
)

type StripeColors struct {
	R int
	G int
	B int
}

func (b *BluetoothSockets) EffectsPulse(pRed, pGreen, pBlue byte, stripe int, ctx context.Context) {
	color1 := StripeColors{int(pRed), int(pGreen), int(pBlue)}
	color2 := StripeColors{0, 0, 0}
	for {
		select {
		case <-ctx.Done():
			return
		default:
			b.EffectsColorTransition(color1, color2, 50, 200*time.Millisecond, stripe, ctx)
			b.EffectsColorTransition(color2, color1, 50, 200*time.Millisecond, stripe, ctx)
		}
	}
}

func (b *BluetoothSockets) EffectsOverflow(oRed, oGreen, oBlue byte, stripe int, ctx context.Context) {
	overflowColors := make([]StripeColors, 6)
	overflowColors[0] = StripeColors{R: int(oRed), G: int(oGreen), B: int(oBlue)}
	overflowColors[1] = StripeColors{50, 250, 250}
	overflowColors[2] = StripeColors{250, 50, 250}
	overflowColors[3] = StripeColors{250, 50, 50}
	overflowColors[4] = StripeColors{250, 250, 50}
	overflowColors[5] = StripeColors{50, 250, 50}
	delay := 500 * time.Millisecond
	for {
		select {
		case <-ctx.Done():
			return
		default:
			b.EffectsColorTransition(overflowColors[0], overflowColors[1], 100, delay, stripe, ctx)
			b.EffectsColorTransition(overflowColors[1], overflowColors[2], 100, delay, stripe, ctx)
			b.EffectsColorTransition(overflowColors[2], overflowColors[3], 100, delay, stripe, ctx)
			b.EffectsColorTransition(overflowColors[3], overflowColors[4], 100, delay, stripe, ctx)
			b.EffectsColorTransition(overflowColors[4], overflowColors[5], 100, delay, stripe, ctx)
			b.EffectsColorTransition(overflowColors[5], overflowColors[0], 100, delay, stripe, ctx)
		}
	}
}

func (b *BluetoothSockets) EffectsAlarm(ctx context.Context) {
	alarmColors := make([]StripeColors, 3)
	alarmColors[0] = StripeColors{0, 0, 0}
	alarmColors[1] = StripeColors{255, 255, 0}
	alarmColors[2] = StripeColors{255, 255, 255}
	b.EffectsColorTransition(alarmColors[0], alarmColors[1], 255, 1*time.Second, BothStripes, ctx)
	b.EffectsColorTransition(alarmColors[1], alarmColors[2], 255, 1*time.Second, BothStripes, ctx)
}

func (b *BluetoothSockets) EffectsColorTransition(colorFrom StripeColors, colorTo StripeColors, steps int, delay time.Duration, stripe int, ctx context.Context) {
	tempRed := float64(colorFrom.R)
	tempGreen := float64(colorFrom.G)
	tempBlue := float64(colorFrom.B)
	stepRed := float64(colorFrom.R-colorTo.R) / float64(steps)
	stepGreen := float64(colorFrom.G-colorTo.G) / float64(steps)
	stepBlue := float64(colorFrom.B-colorTo.B) / float64(steps)
	for i := 0; i < steps; i++ {
		select {
		case <-ctx.Done():
			return
		default:
			tempRed -= stepRed
			tempBlue -= stepBlue
			tempGreen -= stepGreen
			pulseData := make([]byte, 5)
			pulseData[0] = byte(251) //Arduino control byte
			pulseData[1] = byte(stripe)
			pulseData[2] = byte(int(math.Round(tempRed)))
			pulseData[3] = byte(int(math.Round(tempGreen)))
			pulseData[4] = byte(int(math.Round(tempBlue)))
			b.WriteStripe(pulseData)
			time.Sleep(delay)
		}
	}
}
