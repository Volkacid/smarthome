package util

import (
	"fmt"
	"log"
	"runtime"
	"strconv"
	"strings"
)

func CheckFatal(err error) {
	if err != nil {
		log.Fatalf("App crashed: %v\n%s", err, stackTrace(2))
	}
}

// Str2ba converts MAC address string representation to little-endian byte array
func Str2ba(addr string) [6]byte {
	a := strings.Split(addr, ":")
	var b [6]byte
	for i, tmp := range a {
		u, _ := strconv.ParseUint(tmp, 16, 8)
		b[len(b)-1-i] = byte(u)
	}
	return b
}

func stackTrace(skip int) string {
	var str strings.Builder
	for ; ; skip++ {
		pc, file, line, ok := runtime.Caller(skip)
		if !ok {
			break
		}
		if file[len(file)-1] == 'c' {
			continue
		}
		f := runtime.FuncForPC(pc)
		fmt.Fprintf(&str, "   %s:%d %s()\n", file, line, f.Name())
	}

	return str.String()
}
