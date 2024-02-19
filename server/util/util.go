package util

import "log"

func CheckFatal(err error) {
	if err != nil {
		log.Fatal(err)
	}
}
