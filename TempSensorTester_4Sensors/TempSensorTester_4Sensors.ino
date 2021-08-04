// Include the libraries we need
#include <OneWire.h>
#include <DallasTemperature.h>

// Data wire is plugged into port 2 on the Arduino
#define ONE_WIRE_BUS 2

// Setup a oneWire instance to communicate with any OneWire devices (not just Maxim/Dallas temperature ICs)
OneWire oneWire(ONE_WIRE_BUS);

// Pass our oneWire reference to Dallas Temperature. 
DallasTemperature sensors(&oneWire);

/*
 * The setup function. We only start the sensors here
 */
void setup(void)
{
  // start serial port
  Serial.begin(9600);

  // Start up the library
  sensors.begin();
}

/*
 * Main function, get and show the temperature
 */
void loop(void)
{ 
  // call sensors.requestTemperatures() to issue a global temperature 
  // request to all devices on the bus
  sensors.requestTemperatures(); // Send the command to get temperatures
  // After we got the temperatures, we can print them here.
  // We use the function ByIndex, and as an example get the temperature from the first sensor only.
  float tempC0 = sensors.getTempCByIndex(0);
  float tempC1 = sensors.getTempCByIndex(1); 
  float tempC2 = sensors.getTempCByIndex(2);
  float tempC3 = sensors.getTempCByIndex(3);

  Serial.print(String(tempC0));
  Serial.print("\t");
  Serial.print(String(tempC1));
  Serial.print("\t");
  Serial.print(String(tempC2));
  Serial.print("\t");
  Serial.println(String(tempC3));
  //, tempC1, tempC2, tempC3);

//  float tempArr[4] = {tempC0, tempC1, tempC2, tempC3};
//
//  // Check if reading was 
//  for (int i = 0; i < 4; i++)
//  {
//    if(tempArr[i] != DEVICE_DISCONNECTED_C) 
//    {
//      Serial.print("Temperature for the device 1 (index ");
//      Serial.print(i);
//      Serial.print(") is: ");
//      Serial.println(tempArr[i]);
//    } 
//    else
//    {
//      Serial.println("Error: Could not read temperature data");
//    }
//  }
}
