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

// Ba2str converts MAC address little-endian byte array to string representation
func Ba2str(addr [6]byte) string {
	return fmt.Sprintf("%2.2X:%2.2X:%2.2X:%2.2X:%2.2X:%2.2X",
		addr[5], addr[4], addr[3], addr[2], addr[1], addr[0])
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

func SliceEqual(a, b []byte) bool {
	if len(a) != len(b) {
		return false
	}
	for i, v := range a {
		if v != b[i] {
			return false
		}
	}
	return true
}

func AverageColors(a, b []byte) {
	a[2] = (a[2] / 2) + (b[2] / 2)
	a[3] = (a[3] / 2) + (b[3] / 2)
	a[4] = (a[4] / 2) + (b[4] / 2)
}
