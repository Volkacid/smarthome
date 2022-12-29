#include <SoftwareSerial.h>
SoftwareSerial EEBlue(10, 11); // RX | TX

int redPin = 6;
int greenPin = 7;
int bluePin = 5;
int counter = 0;
byte rcv_str[4];

void setup(){
  pinMode(redPin, OUTPUT);
  pinMode(greenPin, OUTPUT);
  pinMode(bluePin, OUTPUT);
  Serial.begin(38400);
  EEBlue.begin(38400);
}

void loop(){
  byte controlByte[1];
  EEBlue.readBytes(controlByte, 1);
  if (controlByte[0] > 200) {
    if (EEBlue.readBytes(rcv_str, 4) < 4) return;
  }
  setColor(rcv_str[1], rcv_str[2], rcv_str[3]);
}

void setColor(int red, int green, int blue)
{
  analogWrite(redPin, red);
  analogWrite(greenPin, green);
  analogWrite(bluePin, blue);
}
