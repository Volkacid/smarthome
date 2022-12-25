package handlers

import "net/http"

func MainPage(writer http.ResponseWriter, request *http.Request) {
	var form = `<html>
    <head>
    <title></title>
    </head>
    <body>
        <H1>IT WORKS!</H!>
    </body>
</html>`
	writer.Write([]byte(form))
}
