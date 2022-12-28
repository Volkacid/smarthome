#include <SoftwareSerial.h>
SoftwareSerial EEBlue(10, 11); // RX | TX

int redPin = 6;
int greenPin = 7;
int bluePin = 5;
int counter = 0;
byte rcv_str[4];
/*int program = 0;
bool isProgramChanged = false;
int redIntense = 0;
int greenIntense = 0;
int blueIntense = 0;*/

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

/*void ChangeProgram(){
  isProgramChanged = true;
  if (program < 2){
      program++;
    }
    else{
      program = 0;
    }
}

void ShadeLB(){
  isProgramChanged = false;
  redIntense = 0;
  greenIntense = 0;
  blueIntense = 0;
  OverColor(50,250,250,100);
  OverColor(0,0,0,100);
}

void ShadeLY(){
  isProgramChanged = false;
  redIntense = 0;
  greenIntense = 0;
  blueIntense = 0;
  OverColor(250,150,50,100);
  OverColor(0,0,0,100);
}

void Overflow(){
  isProgramChanged = false;
  redIntense = 50;
  greenIntense = 250;
  blueIntense = 250;
  OverColor(50, 50, 250, 200);
  OverColor(250, 50, 250, 200);
  OverColor(250, 50, 50, 200);
  OverColor(250, 250, 50, 200);
  OverColor(50, 250, 50, 200);
  OverColor(50, 250, 250, 200);
}

void OverColor(int rMod, int gMod, int bMod, int smooth){
  float rTemp = redIntense;
  float gTemp = greenIntense;
  float bTemp = blueIntense;
  float rStep = (rMod - redIntense) / smooth;
  float gStep = (gMod - greenIntense) / smooth;
  float bStep = (bMod - blueIntense) / smooth;
  for (int i = 0; i < smooth && !isProgramChanged; i++){
    rTemp = rTemp + rStep;
    gTemp = gTemp + gStep;
    bTemp = bTemp + bStep;
    redIntense = int(rTemp);
    greenIntense = int(gTemp);
    blueIntense = int(bTemp);
    setColor(redIntense, greenIntense, blueIntense);
    delay(smooth);
  }
}*/
