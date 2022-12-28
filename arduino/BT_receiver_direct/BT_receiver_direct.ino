#include <SoftwareSerial.h>
SoftwareSerial EEBlue(10, 11); // RX | TX

#include "RGBdriver.h"
#define CLK 4//pins definitions for the driver        
#define DIO 5
RGBdriver Driver(CLK,DIO);

byte rcv_str[4];

void setup()
{
  Serial.begin(38400);
  EEBlue.begin(38400);
}

void loop()
{
  byte controlByte[1];
  EEBlue.readBytes(controlByte, 1);
  if (controlByte[0] > 200) {
    if (EEBlue.readBytes(rcv_str, 4) < 4) return;
  }
  setColor(rcv_str[1], rcv_str[2], rcv_str[3]);
}

void setColor(int red, int green, int blue) {
  Driver.begin();
  Driver.SetColor(red, green, blue);
  Driver.end();
}
