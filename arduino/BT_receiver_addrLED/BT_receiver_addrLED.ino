#include <SoftwareSerial.h>
SoftwareSerial EEBlue(3, 4); // указываем пины rx и tx соответственно

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
  //if (EEBlue.readBytes(rcv_str, 5) == 5) {
  /*if (Serial.readBytes(rcv_serial, 13) == 13) {
    if (rcv_serial[0] == 255) {
      FastLED.show();
    } else {
      setAll();
    }
  }*/
  if (Serial.available()) {
    readSerial();
  } else if (EEBlue.available()) {
    readEEBlue();
  }
  /*if (EEBlue.available()) {
    rcv_temp = EEBlue.read();
    //Serial.println(rcv_temp);
    if(rcv_temp == 40 || rcv_temp == 91) { //( or [
      memset(rcv_str, 0, sizeof(rcv_str));
      counter = 0;
      if (rcv_temp == 40) {
        method = 1;
      } else {
        method = 2;
      }
      //rcv_str[counter]=rcv_temp;
      //counter++;
    } else if(rcv_temp != 41 && rcv_temp != 88) { //( or X
      if (rcv_temp > 48 && rcv_temp < 58) { //0 to 9
        rcv_str[counter]=rcv_temp;
      }
      counter++;
      if (counter >= sizeof(rcv_str)) {
        Serial.println("ERROR!");
        //delay(1000);
        memset(rcv_str, 0, sizeof(rcv_str));
        counter = 0;
      }
    } else {
      //rcv_str[counter]=rcv_temp;
      //Serial.println(rcv_str);
      //for (int i = 0; i < counter; i++) {
        //Serial.print(rcv_str[i]);
        //Serial.print(" ");
      //}
      //Serial.print(rcv_red);
      //Serial.print(" ");
      //Serial.print(rcv_green);
      //Serial.print(" ");
      //Serial.print(rcv_blue);
      //Serial.print(" ");
      //Serial.println(rcv_strip);
      //switch (method) {
        //case 1:
        //setSolid(rcv_red, rcv_green, rcv_blue, rcv_strip);
        //break;
        //case 2:
        //int rcv_index = ((String)rcv_str[11] + (String)rcv_str[12] + (String)rcv_str[13]).toInt();
        //setDifferent(rcv_red, rcv_green, rcv_blue, rcv_strip, rcv_index);
        //break;
      //}
      if (rcv_temp == 88) { //X
        Serial.println("EOT received!");
        //delay(10);
        monitorStrip.show();
      }
      if (counter == 12) {
        setAll();
      }
    }
  }*/
}

/*void setSolid(int r, int g, int b, int strip) {
  switch (strip) {
    case 1:
    monitorStrip.fill(mRGB(r, g, b));
    monitorStrip.show();
    break;
    case 2:
    tableStrip.fill(mRGB(r, g, b));
    tableStrip.show();
    break;
    case 3:
    monitorStrip.fill(mRGB(r, g, b));
    tableStrip.fill(mRGB(r, g, b));
    monitorStrip.show();
    tableStrip.show();
    break;
  }
}

void setDifferent(int r, int g, int b, int strip, int index) {
  switch (strip) {
    case 1:
    monitorStrip.set(index, mRGB(r, g, b));
    monitorStrip.show();
    break;
    case 2:
    tableStrip.set(index, mRGB(r, g, b));
    tableStrip.show();
    break;
  }
}*/

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
  /*Serial.print(rcv_str[1]);
  Serial.print(" ");
  Serial.print(rcv_str[2]);
  Serial.print(" ");
  Serial.print(rcv_str[3]);
  Serial.print(" ");
  Serial.println(rcv_str[4]);*/
  monitorStrip[rcv_serial[1]].setRGB(rcv_serial[3], rcv_serial[2], rcv_serial[4]);
  monitorStrip[rcv_serial[5]].setRGB(rcv_serial[7], rcv_serial[6], rcv_serial[8]);
  monitorStrip[rcv_serial[9]].setRGB(rcv_serial[11], rcv_serial[10], rcv_serial[12]);
}

void setSolid() {
  fill_solid(monitorStrip, MONITOR_LEDS, CRGB(rcv_eeblue[2], rcv_eeblue[1], rcv_eeblue[3]));
  fill_solid(tableStrip, TABLE_LEDS, CRGB(rcv_eeblue[2], rcv_eeblue[1], rcv_eeblue[3]));
  FastLED.show();
}
