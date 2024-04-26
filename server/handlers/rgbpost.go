package handlers

import (
	"context"
	"fmt"
	"github.com/Volkacid/smarthome/service"
	"io"
	"net/http"
	"strconv"
	"strings"
	"time"
)

func PostRGB(bSockets *service.BluetoothSockets, ctx context.Context, cancel context.CancelFunc) http.HandlerFunc {
	return func(writer http.ResponseWriter, request *http.Request) {
		cancel()
		ctx, cancel = context.WithCancel(context.Background())
		body, _ := io.ReadAll(request.Body)
		bodyStr := string(body)
		fmt.Println(bodyStr)
		recRed, recGreen, recBlue := findValues(bodyStr)
		if strings.Contains(bodyStr, "Static") {
			data := []byte{service.EffectSolid, service.BothStripes, recRed, recGreen, recBlue}
			bSockets.QueueWrite(data)
			if int(recRed)+int(recGreen)+int(recBlue) == 0 { //Resend black color(sometimes not all LEDs are disabling for a first time)
				time.Sleep(250 * time.Millisecond)
				bSockets.QueueWrite(data)
			}
		} else if strings.Contains(bodyStr, "Pulse") {
			go bSockets.EffectsPulse(recRed, recGreen, recBlue, service.BothStripes, ctx)
		} else if strings.Contains(bodyStr, "Rainbow") {
			go bSockets.EffectsRainbow(service.BothStripes, ctx)
		} else if strings.Contains(bodyStr, "Overflow") {
			go bSockets.EffectsOverflow(recRed, recGreen, recBlue, service.BothStripes, ctx)
		}

		http.Redirect(writer, request, "/", http.StatusFound)
	}
}

func findValues(inputStr string) (byte, byte, byte) {
	_, redValue, _ := strings.Cut(inputStr, "redRange=")
	redValue, _, _ = strings.Cut(redValue, "&")
	_, greenValue, _ := strings.Cut(inputStr, "greenRange=")
	greenValue, _, _ = strings.Cut(greenValue, "&")
	_, blueValue, _ := strings.Cut(inputStr, "blueRange=")
	blueValue, _, _ = strings.Cut(blueValue, "&")

	redInt, _ := strconv.Atoi(redValue)
	greenInt, _ := strconv.Atoi(greenValue)
	blueInt, _ := strconv.Atoi(blueValue)
	return byte(redInt), byte(greenInt), byte(blueInt)
}
