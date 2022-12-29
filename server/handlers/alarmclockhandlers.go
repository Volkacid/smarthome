package handlers

import (
	"fmt"
	"github.com/Volkacid/smarthome/service"
	"io"
	"net/http"
	"strconv"
	"strings"
)

func AlarmClockPage(alarmService *service.AlarmClock) http.HandlerFunc {
	return func(writer http.ResponseWriter, request *http.Request) {
		var alarmHead = `<p><b>Alarm clock settings</b></p>
						<p>Current alarm:</p>`
		writer.Write([]byte(alarmHead))

		alarmTime := alarmService.GetAlarmTime()
		strconv.Itoa(alarmService.AlHour)
		if alarmTime == "00:00" {
			writer.Write([]byte("Alarm stopped"))
		} else {
			writer.Write([]byte(alarmTime))
		}
		var alarmSettings = `<p>Set up new alarm:</p>
<div>
  <form method="post">
    <select name="alarmHours" id="alarmHours">
    <option value="">Hours</option>
    <option value="4">4</option>
    <option value="5">5</option>
    <option value="6">6</option>
    <option value="7">7</option>
    <option value="8">8</option>
    <option value="9">9</option>
    <option value="10">10</option>
	<option value="11">11</option>
    <option value="12">12</option>
    <option value="13">13</option>
    <option value="14">14</option>
    <option value="15">15</option>
    <option value="16">16</option>
    <option value="17">17</option>
	<option value="18">17</option>
    <option value="19">19</option>
    <option value="20">20</option>
    <option value="21">21</option>
	<option value="22">22</option>
	<option value="23">23</option>
    <option value="1">1</option>
    <option value="2">2</option>
    <option value="3">3</option>
</select>
    <select name="alarmMinutes" id="alarmMinutes">
    <option value="">Minutes</option>
    <option value="0">0</option>
    <option value="5">5</option>
    <option value="10">10</option>
    <option value="15">15</option>
    <option value="20">20</option>
    <option value="25">25</option>
    <option value="30">30</option>
    <option value="35">35</option>
    <option value="40">40</option>
    <option value="45">45</option>
    <option value="50">50</option>
    <option value="55">55</option>
</select>
    <div>
      __________________
    <div>
    <input type="submit" value="Submit" name="alarmSubmit"> <input type="submit" value="Stop Alarm" name="alarmStop">
  </form>
</div>`
		writer.Write([]byte(alarmSettings))
	}
}

func SetAlarm(alarmService *service.AlarmClock, stripePorts *service.StripePorts) http.HandlerFunc {
	return func(writer http.ResponseWriter, request *http.Request) {
		body, _ := io.ReadAll(request.Body)
		bodyStr := string(body)
		if strings.Contains(bodyStr, "alarmSubmit") {
			_, alHour, _ := strings.Cut(bodyStr, "alarmHours=")
			alHour, _, _ = strings.Cut(alHour, "&")
			_, alMin, _ := strings.Cut(bodyStr, "alarmMinutes=")
			alMin, _, _ = strings.Cut(alMin, "&")
			alHourInt, err := strconv.Atoi(alHour)
			if err != nil {
				return
			}
			alMinInt, err := strconv.Atoi(alMin)
			if err != nil {
				return
			}
			alarmService.StopAlarmService()
			*alarmService = *service.StartAlarmService(alHourInt, alMinInt, stripePorts)
		}
		if strings.Contains(bodyStr, "alarmStop") {
			alarmService.AlHour = 0
			alarmService.AlMin = 0
			alarmService.StopAlarmService()
			fmt.Println("Alarm service stopped")
		}
		http.Redirect(writer, request, "/alarm", http.StatusFound)
	}
}
