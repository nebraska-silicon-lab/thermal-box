# Cold Box Control

## Setup:

Visual Studio and Arduino software must be downloaded prior to running ColdBoxControl code. Links to download:
- Visual Studio: https://visualstudio.microsoft.com/downloads/
- Arduino: https://www.arduino.cc/en/software
          
## Checkout Instructions:

```
git clone https://github.com/nebraska-silicon-lab/thermal-box.git
```

## Arduino Set Up

### Circuit 

A simple diagram of the circuit used for the Arduino Thermometer is attached below.

![](https://hackster.imgix.net/uploads/attachments/229740/FP8600RIFMRO359.LARGE.jpg?auto=compress%2Cformat&w=1280&h=960&fit=max)

This circuit is modified to accommodate additional temperature sensors. The supplies necessary to accommodate a thermally isolated collection of temperature sensors are listed below:
- Arduino
- Jumper Cables (to connect power, ground and data)
- Breadboard
- Jumper Cable with Mounted 4.7kΩ Resistor
- Jumper Cables (to connect circuit to temperature sensors via thermal box connection port)
- Connection Port 
- Temperature Sensors

Images of the modified set up and supplies are shown below. The first image depicts the Arduino with jumper wires plugged into the positions shown in the above diagram. 

<img src="https://user-images.githubusercontent.com/48451319/128375908-f33a0707-acad-4163-9f16-b4e93a00577b.JPG" width="600" height="450">

The image below shows the breadboard that houses the rest of the circuit. The red, green and black jumper wires are connected to the Arduino (not shown). The jumper cable with the attached resistor is next in succession. Finally, the cables with the yellow shrink wrap are the temperature sensors. While the complete Cold Box set up will have these sensors connected via jumper cables and a connection port (shown below) to the inside of the Cold Box, this image shows their proper alignment - the black marks on the side of the shrink wrap denote which pin should receive power. 

<img src="https://user-images.githubusercontent.com/48451319/128263495-7bdb7660-fc21-4018-8899-5c9a8e1f31b7.JPG" width="600" height="450">

The following images show the supplied jumper cables and the connection port, mentioned above.

<img src="https://user-images.githubusercontent.com/48451319/128375957-ea9ce99d-c077-43e8-a0a1-5585211595e3.JPG" width="600" height="450">

<img src="https://user-images.githubusercontent.com/48451319/128263446-fb335195-e8de-450e-88e7-d2f264c41a0a.JPG" width="300" height="450">

### Code

Three pieces of Arduino code exist within the repository. 

- TempSensorTester_4Sensors: Returns the temperatures (in C) of up to 4 connected temperature sensors. Returns by index. Used to test individual sets of sensors. 
- TempSensor_GetAddress: Returns the address (in HEX) of any number of connected temperature sensors. Used to find addresses to input into CoolBoxTempSensor_16Sensor code. 
- CoolBoxTempSensor_16Sensor: Returns the temperatures (in C) of up to 16 connected temperature sensors. Returns by sensor address. Used to properly configure Arduino for readout in Cold Box code. 
  - Addresses of 16 labeled temperature sensors are pre-loaded into code. 16 additional sensor addresses are commented in code in case of need to replace original 16 sensors. 
  - **Configure Arduino with this code before running Cold Box Control code**
     
          

## Understanding Visual Studio
There are (3??) files below the top toolbar
  - Form1.resx
  - Form1.cs includes annotation to help read the code
    - This code can be altered to your heart's content <3
  - Form1.Designer.cs 
    - Do not touch! This code is automatically generated, so messing with it can shut down the whole thing. 
      - Trust me. I tried fixing the spelling of "coolent" in the code. It did not go well. 
## Operating Interface
  To see the operating interface, click on Run at the top of the screen.  
  A white box should appear on your screen. Below, I will how to navigate the buttons on the interface. Descriptions of the buttons can also be found in the annotated code.
        
### Turning on appliances
 **Power Supply 1 Serial:**
 
          - Using the drop down box, select port that coincides with where the power supply unit 1 is connected.
          - Click Open. This opens the port and allows communication with PSU1 and VS.
          - Set Voltage (V) and Current (I) using the V and I sets to the furthest left side of the interface.
                    - Input desired values in volts and ohms respectively.
                    - Click Set V/I Limit 
                    - Click Enable Output to turn on PSU1
**Power Supply 2 Serial:** 

          - Using the drop down box, select port that coincides with where the power supply unit 2 is connected. 
          - Click open. 
          - Find the V set and I set at the middle of the interface. 
                    - Input values for voltage and current.
                    - Click Set V/I Limit 
                    - Click Enable Output to turn on PSU2
 **Chiller Serial:**
 
          - Using the drop down box, select port that coincides with where the chiller is connected.
          - Click open. This opens the port and allows communication between chiller and VS
          - Set temperature in Celsius using the box labeled **Temp set**. 
                    - Click **Set temp** to program the chiller to this temperature.
          - In the Time On/Off box, set your desired half cycle time in seconds.
                    - This is how long the chiller will spend on and off. A time input of 1800 seconds will turn the chiller on for 30 minutes, then off for 30 minutes. 
          - The # of Cycles button lets you control how many times you want the chiller to be on and off. 
                    - Following the previous example, an input of 2 will have the chiller turn on for 30 minutes and off for 30 minutes twice for a total of 2 hours. 
                              
**Arduino Serial:**

          - Using the drop down box, select port that coincides with where the arduino is connected.
          - Click open. This opens the port and allows communication between the Arduino and VS
                              
**Note: Sometimes the Arduino will randomly switch ports.
      If you cannot communicate with the Arduino, check different ports to see if it has changed.**
                            
  ### Recording Data
 **Data Log File:**
 This is where the collected data will be recorded. 
 
          -Either open a preexisting file using the Open button, or create a New File using the New File button.
 **Collect Data, Seconds per Poll:**
 This determines how often the program will record data. 
 Using 1 second will collect data once per second. This can be adjusted as needed, but we recommend sticking to 1 second.
                    
          - Click Enable Auto Collect beneath the input box to begin taking data.
        
 **Graphs:**
The three graphs at the bottom of the interface track the temperature over time. They should begin automatically once you click Enable Auto Collect. 
        
        
## Physical Setup of the Chiller
### Chiller
Chiller includes the following components
          - Styrofoam coldbox
                    - side hole for connecting ports, Arduino with sensors attached
                    - holes for inserting antifreeze pipes
                    - peltier?????
                    - coldplate that does STUFF!!!!!!
                    
