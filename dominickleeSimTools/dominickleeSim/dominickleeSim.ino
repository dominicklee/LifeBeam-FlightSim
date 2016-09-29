/*
  
***** Dominick Lee Sim 2DOF code *****

***** Setting up Sim Tools *****
Setup Axis 1 in Axis Assignment to 100% Roll
Setup Axis 2 in Axis Assignment to 100% Pitch
Set the interface settings to 9600,8,n,1
Set Data bits to 8 and decimal
Startup - R127~P127~ @ 20ms this will centre the sim
Interface - R<Axis1>~P<Axis2>~  @ 20ms this is the format the string will be sent like
Shutdown - R127~P127~ @ 20ms this will centre the sim for you to get out before turning off
********************************************************************************************

Original code written by Dominick Lee.
Special thanks to EAORobbie for latest revision.
Designed for usage with SimTools Motion Sofware.

*/

String inputString = "";         // a string to hold incoming data
boolean stringComplete = false;  // whether the string is complete
int roll = 9;    //Digital Pin for Roll
int pitch = 10;    //Digital Pin for Pitch
int valRoll = 127;
int valPitch = 127;

void setup() {
  // initialize serial:
  Serial.begin(9600);
  // reserve 200 bytes for the inputString:
  inputString.reserve(200);

}

void loop() {
  // print the string when a newline arrives:
  if (stringComplete) {
    //Serial.println(inputString); 
    if (inputString.startsWith("R",0)){
    //Serial.println("We have Roll");
    
    String valueString = inputString.substring(1);
    valRoll = valueString.toInt();
    //Serial.println(valRoll);
    analogWrite(roll, valRoll);                 // sets the position 
      
    // clear the string:
    inputString = "";
    valueString = "";
    stringComplete = false;
  
}
  if (inputString.startsWith("P",0)){
    //Serial.println("We have Pitch");
    
    String valueString = inputString.substring(1);
    valPitch = valueString.toInt();
    //Serial.println(valPitch);
    analogWrite(pitch, valPitch);               // sets the position  
      
    // clear the string:
    inputString = "";
    valueString = "";
    stringComplete = false;
}
}      


}

/*  SerialEvent occurs whenever a new data comes in the
 hardware serial RX.  This routine is run between each
 time loop() runs, so using delay inside loop can delay
 response.  Multiple bytes of data may be available.
 */
void serialEvent() {
  while (Serial.available()) {
    // get the new byte:
    char inChar = (char)Serial.read(); 
    // add it to the inputString:
    if (inChar != '~'){
    inputString += inChar;
    }
    // if the incoming character is a newline, set a flag
    // so the main loop can do something about it:
    if (inChar == '~') {
      stringComplete = true;
    } 
  }
}


