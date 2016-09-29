//Flight Simulator Code for 'Joystick to Arduino'
//By Dominick Lee

int incomingByte = 0;   // for incoming serial data

int roll = 9;    //Digital Pin for Roll
int pitch = 10;    //Digital Pin for Pitch

int volts (float voltage)    //this function converts voltage to analog 0-255 values
{
  float analogval;
  analogval = ((255 * voltage)/5);    //equation
  
  return analogval;
}


float getFloatFromSerialMonitor(){
  char inData[20];  
  float f=0;    
  int x=0;  
  while (x<1){  
  String str;   
  if (Serial.available()) {
    delay(5); //lower the better
    int i=0;
    while (Serial.available() > 0) {
     char  inByte = Serial.read();
      str=str+inByte;
      inData[i]=inByte;
      i+=1;
      x=2;
    }
    f = atof(inData);
    memset(inData, 0, sizeof(inData));  
  }
  }//END WHILE X<1  
   return f; 
  }


void setup() {
  // initialize serial communication:
  Serial.begin(57600);
  pinMode(13, OUTPUT);  

}


void loop() {
    digitalWrite(13, HIGH);   // set the LED on
  
      // send data only when you receive data:
        if (Serial.available() > 0) {
                // read the incoming byte:
                
                incomingByte = Serial.read();
                
                if (incomingByte == 114) {  //we got roll
                 Serial.println("roll");
                 
                float x = getFloatFromSerialMonitor();
                
                Serial.print(x);
                Serial.print("v");
                Serial.print(" Analog ");
                Serial.println(volts(x));
                
                analogWrite(roll, volts(x));     
                }
                
                if (incomingByte == 112) {  //we got pitch
                 Serial.println("pitch");   
  
                  float x = getFloatFromSerialMonitor();
                
                Serial.print(x);
                Serial.print("v");
                Serial.print(" Analog ");
                Serial.println(volts(x));        
         
                analogWrite(pitch, volts(x));            
                }
                


        }

} //end loop

