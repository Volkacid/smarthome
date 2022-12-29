#include <SoftwareSerial.h>
SoftwareSerial EEBlue(10, 11); // RX | TX

#include "ChainableLED.h"
#define NUM_LEDS 1

ChainableLED leds(4, 5, NUM_LEDS);

byte rcv_str[4];

void setup()
{
  Serial.begin(38400);
  EEBlue.begin(38400);
  leds.init();
}

void loop()
{
  byte controlByte[1];
  EEBlue.readBytes(controlByte, 1);
  if (controlByte[0] > 250) {
    controlByte[0] = 0;
    if (EEBlue.readBytes(rcv_str, 4) < 4) return;
    Serial.println(rcv_str[0]);
    Serial.println(rcv_str[1]);
    Serial.println(rcv_str[2]);
    Serial.println(rcv_str[3]);
    Serial.println("------");
    setColor(rcv_str[1], rcv_str[2], rcv_str[3]);
    memset(rcv_str, 0, sizeof(rcv_str));
  }
}

void setColor(int red, int green, int blue) 
{
  leds.setColorRGB(0, red, green, blue);
}
