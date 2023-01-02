#include <SoftwareSerial.h>
SoftwareSerial EEBlue(11, 12); // RX | TX

byte rcv_str[2];
int relayIn1 = 7;
int relayIn2 = 6;

void setup()
{
  Serial.begin(38400);
  EEBlue.begin(38400);
  pinMode(relayIn1, OUTPUT);
  pinMode(relayIn2, OUTPUT);
  //digitalWrite(relayIn1, LOW);
  //digitalWrite(relayIn2, LOW);
}

void loop()
{
  byte controlByte[1];
  EEBlue.readBytes(controlByte, 1);
  if (controlByte[0] == 254) {
    controlByte[0] = 0;
    if (EEBlue.readBytes(rcv_str, 2) < 2) return;
    Serial.println(rcv_str[1]);
    switch (rcv_str[1]) {
      case 255: //CommHumidifierEnable
      digitalWrite(relayIn1, HIGH);
      digitalWrite(relayIn2, HIGH);
      break;
      case 254: //CommHumidifierDisable
      digitalWrite(relayIn1, LOW);
      digitalWrite(relayIn2, LOW);
      break;
      case 253: //CommHeaterEnable
      break;
      case 252: //CommHeaterDisable
      break;
      case 251: //CommConditionerEnable
      break;
      case 250: //CommConditionerDisable
      break;
    }
    memset(rcv_str, 0, sizeof(rcv_str));
  }
}
