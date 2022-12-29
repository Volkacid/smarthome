#include <SoftwareSerial.h>
SoftwareSerial EEBlue(3, 4); // rx and tx pins

#include <FastLED.h>
#define MONITOR_LEDS 114
#define MONITOR_PIN 13
CRGB monitorStrip[MONITOR_LEDS];
#define TABLE_LEDS 186
#define TABLE_PIN 12
CRGB tableStrip[TABLE_LEDS];

int counter = 0;
byte rcv_temp = 0;
byte rcv_serial[13];
byte rcv_eeblue[4];
int method = 0;
int strip = 0;
#define LED_SHOW 255
#define SERIAL_SEVERAL 254
#define SERIAL_SOLID 253
#define EEBLUE_SEVERAL 252
#define EEBLUE_SOLID 251

void setup() {
  pinMode(3,INPUT);
  pinMode(4,OUTPUT);
  Serial.begin(115200);
  EEBlue.begin(38400);
  FastLED.addLeds<NEOPIXEL, MONITOR_PIN>(monitorStrip, MONITOR_LEDS);
  FastLED.addLeds<NEOPIXEL, TABLE_PIN>(tableStrip, TABLE_LEDS);
}

void loop() {
  if (Serial.available()) {
    readSerial();
  } else if (EEBlue.available()) {
    readEEBlue();
  }
}

void readSerial() {
  if (Serial.readBytes(rcv_serial, 13) == 13) {
    switch (rcv_serial[0]) {
      case LED_SHOW:
      FastLED.show();
      break;
      case SERIAL_SEVERAL:
      setSeveral();
      break;
    }
  }
}

void readEEBlue() {
  byte controlByte[1];
  EEBlue.readBytes(controlByte, 1);
  if (controlByte[0] > 250) {
    if (EEBlue.readBytes(rcv_eeblue, 4) < 4) return;
  }
  switch (controlByte[0]) {
    case LED_SHOW:
    FastLED.show();
    break;
    case EEBLUE_SEVERAL:
    break;
    case EEBLUE_SOLID:
    setSolid();
    break;
  }
}

void setSeveral() {
  monitorStrip[rcv_serial[1]].setRGB(rcv_serial[3], rcv_serial[2], rcv_serial[4]);
  monitorStrip[rcv_serial[5]].setRGB(rcv_serial[7], rcv_serial[6], rcv_serial[8]);
  monitorStrip[rcv_serial[9]].setRGB(rcv_serial[11], rcv_serial[10], rcv_serial[12]);
}

void setSolid() {
  fill_solid(monitorStrip, MONITOR_LEDS, CRGB(rcv_eeblue[2], rcv_eeblue[1], rcv_eeblue[3]));
  fill_solid(tableStrip, TABLE_LEDS, CRGB(rcv_eeblue[2], rcv_eeblue[1], rcv_eeblue[3]));
  FastLED.show();
}
