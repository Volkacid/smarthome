#include <SoftwareSerial.h>
SoftwareSerial EEBlue(3, 4); // RX | TX

int btPowerPin = 2;
int btATPin = 5;
byte rcv_str[5];
byte incomingByte = 0;

void setup() {
  pinMode (btPowerPin, OUTPUT);
  pinMode (btATPin, OUTPUT);
  Serial.begin(38400);
  EEBlue.begin(38400);
  Serial.println("start prg");
  //bindModule();
  digitalWrite(btPowerPin, HIGH);
}

void loop() {
  if (Serial.readBytes(rcv_str, 5) < 5) return;
  EEBlue.write(rcv_str, 5);
}

void bindModule() {
  digitalWrite(btPowerPin, LOW);
  digitalWrite(btATPin, LOW);
  delay(100);
  digitalWrite(btPowerPin, HIGH);
  digitalWrite(btATPin, HIGH);
  delay(3000);
  EEBlue.write("AT+BIND=98D3,33,F5A442\r\n"); //Module 04
  digitalWrite(btATPin, LOW);
  delay(100);
  EEBlue.write("AT+RESET\r\n");
  delay(13000);
  Serial.println("Done!");
}
