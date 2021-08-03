# Cold Box Control

## Setup:

Visual Studio and Arduino software must be downloaded prior to running ColdBoxControl code. Links to download:
          Visual Studio: https://visualstudio.microsoft.com/downloads/
          Arduino: https://www.arduino.cc/en/software
          
## Checkout Instructions:

```
git clone https://github.com/nebraska-silicon-lab/thermal-box.git
```

## Arduino Set Up

![Image of Circuit Set Up]
(https://hackster.imgix.net/uploads/attachments/229740/FP8600RIFMRO359.LARGE.jpg?auto=compress%2Cformat&w=1280&h=960&fit=max)
          
          
2. Download the arduino codes. For VS to work properly, () must also be open.
   () collects the serial numbers of the thermal sensors for data collection 
   () allows VS to communicate with the arduino. 
3. Download ColdBoxControl.csproj to operate the chiller located under !!!!!!!. 
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
                    
### Ports
### Arduino
### Sensors
## Communicating with the chiller
