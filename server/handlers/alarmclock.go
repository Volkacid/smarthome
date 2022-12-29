package handlers

import (
	"github.com/Volkacid/smarthome/service"
	"net/http"
)

func AlarmClockPage(alarmService *service.AlarmClock) http.HandlerFunc {
	return func(writer http.ResponseWriter, request *http.Request) {
		var alarmHead = `<p><b>Alarm clock settings</b></p>
						<p><b>Current alarm:</b></p>`
		writer.Write([]byte(alarmHead))
		alarmTime := alarmService.GetAlarmTime()
		if alarmTime == "0:0" {
			writer.Write([]byte("Alarm stopped"))
		} else {
			writer.Write([]byte(alarmTime))
		}
		var alarmSettings = `<p><b>Set up new alarm:</b></p>
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
      ________________________
    <div>
    <input type="submit" value="Submit" name="alarmSubmit"> <input type="submit" value="Stop Alarm" name="alarmStop">
  </form>
</div>`
		writer.Write([]byte(alarmSettings))
	}
}
