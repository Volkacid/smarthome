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

func (sp *StripePorts) EffectsPulse(pRed int, pGreen int, pBlue int, stripe int, ctx context.Context) {
	color1 := StripeColors{pRed, pGreen, pBlue}
	color2 := StripeColors{0, 0, 0}
	for {
		select {
		case <-ctx.Done():
			return
		default:
			sp.EffectsColorTransition(color1, color2, 50, 100*time.Millisecond, stripe, ctx)
			sp.EffectsColorTransition(color2, color1, 50, 100*time.Millisecond, stripe, ctx)
		}
	}
}

func (sp *StripePorts) EffectsOverflow(oRed int, oGreen int, oBlue int, stripe int, ctx context.Context) {
	overflowColors := make([]StripeColors, 6)
	overflowColors[0] = StripeColors{R: oRed, G: oGreen, B: oBlue}
	overflowColors[1] = StripeColors{50, 250, 250}
	overflowColors[2] = StripeColors{250, 50, 250}
	overflowColors[3] = StripeColors{250, 50, 50}
	overflowColors[4] = StripeColors{250, 250, 50}
	overflowColors[5] = StripeColors{50, 250, 50}
	delay := 100 * time.Millisecond
	for {
		select {
		case <-ctx.Done():
			return
		default:
			sp.EffectsColorTransition(overflowColors[0], overflowColors[1], 50, delay, stripe, ctx)
			sp.EffectsColorTransition(overflowColors[1], overflowColors[2], 50, delay, stripe, ctx)
			sp.EffectsColorTransition(overflowColors[2], overflowColors[3], 50, delay, stripe, ctx)
			sp.EffectsColorTransition(overflowColors[3], overflowColors[4], 50, delay, stripe, ctx)
			sp.EffectsColorTransition(overflowColors[4], overflowColors[5], 50, delay, stripe, ctx)
			sp.EffectsColorTransition(overflowColors[5], overflowColors[0], 50, delay, stripe, ctx)
		}
	}
}

func (sp *StripePorts) EffectsColorTransition(colorFrom StripeColors, colorTo StripeColors, steps int, delay time.Duration, stripe int, ctx context.Context) {
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
			sp.WriteSerial(pulseData)
			time.Sleep(delay)
		}
	}
}
