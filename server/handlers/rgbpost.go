package handlers

import (
	"context"
	"fmt"
	"github.com/Volkacid/smarthome/service"
	"io"
	"net/http"
	"strconv"
	"strings"
)

func PostRGB(stripePorts *service.StripePorts, ctx context.Context, cancel context.CancelFunc) http.HandlerFunc {
	return func(writer http.ResponseWriter, request *http.Request) {
		cancel()
		body, _ := io.ReadAll(request.Body)
		bodyStr := string(body)
		fmt.Println(bodyStr)
		recRed, recGreen, recBlue := findValues(bodyStr)
		if strings.Contains(bodyStr, "Static") {
			serialData := make([]byte, 5)
			serialData[0] = byte(251)
			serialData[2] = byte(recRed)
			serialData[3] = byte(recGreen)
			serialData[4] = byte(recBlue)
			stripePorts.WriteStripe(serialData)
		}
		if strings.Contains(bodyStr, "Pulse") {
			ctx, cancel = context.WithCancel(context.Background())
			go stripePorts.EffectsPulse(recRed, recGreen, recBlue, service.BothStripes, ctx)
		}
		if strings.Contains(bodyStr, "Overflow") {
			ctx, cancel = context.WithCancel(context.Background())
			go stripePorts.EffectsOverflow(recRed, recGreen, recBlue, service.BothStripes, ctx)
		}
		http.Redirect(writer, request, "/", http.StatusFound)
	}
}

func findValues(inputStr string) (int, int, int) {
	_, redValue, _ := strings.Cut(inputStr, "redRange=")
	redValue, _, _ = strings.Cut(redValue, "&")
	_, greenValue, _ := strings.Cut(inputStr, "greenRange=")
	greenValue, _, _ = strings.Cut(greenValue, "&")
	_, blueValue, _ := strings.Cut(inputStr, "blueRange=")
	blueValue, _, _ = strings.Cut(blueValue, "&")

	redInt, _ := strconv.Atoi(redValue)
	greenInt, _ := strconv.Atoi(greenValue)
	blueInt, _ := strconv.Atoi(blueValue)
	return redInt, greenInt, blueInt
}
