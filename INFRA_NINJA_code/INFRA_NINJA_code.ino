#include <IRremote.h>

int pwrpin=A2;
int IRpin = A0;
IRrecv irrecv(IRpin);
decode_results results;

void setup()
{
  pinMode(pwrpin,OUTPUT);
  analogWrite(pwrpin,150);
  Serial.begin(9600);
  irrecv.enableIRIn(); // Start the receiver
 
}

void loop() 
{
  
 
  if (irrecv.decode(&results)) 
    {
      Serial.println(results.value, DEC); // Print the Serial 'results.value'
      irrecv.resume();   // Receive the next value
    }
  
  
}
