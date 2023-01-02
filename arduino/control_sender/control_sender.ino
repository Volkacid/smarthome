#include "DHT.h"

#include <SoftwareSerial.h>
SoftwareSerial EEBlue(10, 11); // RX | TX

#define DHTPIN 12     // Digital pin connected to the DHT sensor
// Feather HUZZAH ESP8266 note: use pins 3, 4, 5, 12, 13 or 14 --
// Pin 15 can work but DHT must be disconnected during program upload.
#define DHTTYPE DHT22   // DHT 22  (AM2302), AM2321
DHT dht(DHTPIN, DHTTYPE);

byte rcv_str[3];
unsigned long timer;

void setup() {
  Serial.begin(38400);
  EEBlue.begin(38400);
  dht.begin();
  timer = millis();
}

void loop() {
  if (Serial.available()) {
    if (Serial.readBytes(rcv_str, 3) < 3) return;
      EEBlue.write(rcv_str, 3);
  }
  byte buf[3];
  buf[0] = 254;
  buf[2] = 255;
  EEBlue.write(buf, 3);
  Serial.println(buf[0]);
  Serial.println(buf[1]);
  Serial.println(buf[2]);
  delay(2000);
  buf[2] = 254;
  EEBlue.write(buf, 3);
  delay(2000);
  // Wait a few seconds between measurements.
  if (millis() - timer > 10000) {
    timer = millis();
    dhtMeasures();
  }
}

void dhtMeasures() {
  // Reading temperature or humidity takes about 250 milliseconds!
  // Sensor readings may also be up to 2 seconds 'old' (its a very slow sensor)
  float h = dht.readHumidity();
  // Read temperature as Celsius (the default)
  float t = dht.readTemperature();

  // Check if any reads failed and exit early (to try again).
  if (isnan(h) || isnan(t)) {
    Serial.println(F("Failed to read from DHT sensor!"));
    return;
  }
  String measurements = "Hum" + String(h);
  measurements += "Temp" + String(t);
  Serial.println(measurements);
  //EEBlue.write(h);
  //EEBlue.write(t);
}
