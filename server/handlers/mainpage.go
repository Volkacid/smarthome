package handlers

import "net/http"

func MainPage(writer http.ResponseWriter, request *http.Request) {
	var rgbControl = `
<p><b>LED stripe settings:</b></p>
<div>
 <form method="post">
  <input type="submit" value="Static" name="static"> <input type="submit" value="Overflow" name="overflow"> <input type="submit" value="Pulse" name="pulse">
  <p><input type="range" name="redRange" min="0" max="255" value="0"> Red</p>
  <p><input type="range" name="greenRange" min="0" max="255" value="0"> Green</p>
  <p><input type="range" name="blueRange" min="0" max="255" value="0"> Blue</p>
 </form>
</div>
`
	writer.Write([]byte(rgbControl))
	writer.Write([]byte("somestring"))
}
