package handlers

import (
	"fmt"
	"io"
	"net/http"
)

func PostRGB(writer http.ResponseWriter, request *http.Request) {
	fmt.Println("Request received!")
	body, _ := io.ReadAll(request.Body)
	fmt.Println(string(body))
}
