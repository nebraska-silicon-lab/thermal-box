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

The following images show the supplied jumper cables and the connection port, mentioned above. There are 4 3-pin male to male jumper cables. The connector port houses 1 12-position female to female jumper cable and 1 10-position female to female jumper cable. The temperature sensors connect to the 12-position port on one side, while the supplied jumper cables will bridge the gap between the breadboard and the other side of the connection port.  

<img src="https://user-images.githubusercontent.com/48451319/128375957-ea9ce99d-c077-43e8-a0a1-5585211595e3.JPG" width="600" height="450">  <img src="https://user-images.githubusercontent.com/48451319/128263446-fb335195-e8de-450e-88e7-d2f264c41a0a.JPG" width="300" height="450">

### Code

Three pieces of Arduino code exist within the repository. 

- TempSensorTester_4Sensors: Returns the temperatures (in C) of up to 4 connected temperature sensors. Returns by index. Used to test individual sets of sensors. 
- TempSensor_GetAddress: Returns the address (in HEX) of any number of connected temperature sensors. Used to find addresses to input into CoolBoxTempSensor_16Sensor code. 
- CoolBoxTempSensor_16Sensor: Returns the temperatures (in C) of up to 16 connected temperature sensors. Returns by sensor address. Used to properly configure Arduino for readout in Cold Box code. 
  - Addresses of 16 labeled temperature sensors are pre-loaded into code. 16 additional sensor addresses are commented in code in case of need to replace original 16 sensors. 
  - **Configure Arduino with this code before running Cold Box Control code**
     
## Power Supply Set Up

The supplies for the power supply include:
- Connection Port (shown above)
- 2 2-pin male Jumper Cables
- 2 3-pin male Jumper Cables
- 1 10-pin male to female Jumper Cable

On the interior of the box, the Connection Port and the Service Hybrid will be bridged by 1 10-pin male to female Jumper Cable shown below. 

<img src="https://user-images.githubusercontent.com/48451319/128734177-f72bd4e4-3f8f-4422-92d0-08379ec3a3e5.jpg" width="500" height="500">

The female end of the Jumper Cable will connect to the pins at the end of the Service Hybrid, also shown below. 

<img src="https://user-images.githubusercontent.com/48451319/128734721-1829f153-883b-458c-9ff7-f7d7c5580277.JPG" width="500" height="600">

On the outside of the box, 2 2-pin male Jumper Cables and 2 3-pin male Jumper Cables will connect to the connection port. The unfinished ends of these cables will be affixed to the power and ground of 2 power supplies. Shown below are the finished ends of the cables. 

<img src="https://user-images.githubusercontent.com/48451319/128734320-420bb89b-30de-4b77-9002-68baa250f3c5.jpg" width="600" height="450">

The image below is the design of the service hybrid. The pins are grouped such that Positions 7 and 9 should be connected to ground of one power supply. Positions 8 and 10 should be connected to the power of that same power supply. This power supply will be denoted as Power Supply 2 (PS2). The bottom pins should be connected to the second power supply in a similar manner. This latter power supply will be dnoted as Power Supply (PS1).

<img src="https://user-images.githubusercontent.com/48451319/128737063-7cfc01d0-6e91-4472-b635-4b427dcad6a1.png" width="500" height="600">

## Cold Box Control

![](https://user-images.githubusercontent.com/48451319/128401455-b2351d1d-a750-4cda-b3cd-47a332acce11.PNG)

Above is an image of the interface used to run the Cold Box system. To navigate to this window, open the file ColdBoxControl.csproj (located in ColdBoxControl/ColdBoxControl/ directories) in Visual Studio. This should launch several files within Visual Studio, including Form1.resx, Form1.cs and Form1.Designer.cs. Of these files, only Form1.cs can be altered; the other 2 files are autogenerated when running the code. To launch the interface above, click the 'Start" button at the top of the Visual Studio window.

        
### Configuring Devices

 **Power Supply 1 Serial:**
 
          - Using the drop down box, select port attributed to Power Supply 1 (PS1). 
          - Click Open.
          - Set Voltage and Current using the V and I sets on the far left of the interface. 
                    - Input desired values in volts and ohms respectively. 
                              - Starting values for Power Supply 1: 1.23 A @ 44 V
                    - Click Set V/I Limit 
                    - Click Enable Output to turn on PS1.
                    
**Power Supply 2 Serial:** 

          - Using the drop down box, select port attributed to Power Supply 2 (PS2). 
          - Click Open.
          - Set Voltage and Current using the V and I sets to the right of the V/I controls for PS1. 
                    - Input desired values in volts and ohms respectively.
                              - Starting values for Power Supply 1: 0.4 A @ 44 V
                    - Click Set V/I Limit 
                    - Click Enable Output to turn on PS2.
                    
 **Chiller Serial:**
 
          - The Chiller Interface was configured to control a "". Other cooling devices may not easily couple to this set up. 
          - Using the drop down box, select port attributed to Chiller. 
          - Click open.
          - Set temperature in Celsius using the box labeled **Temp set**. 
                    - Click **Set temp** to program the chiller to this temperature.
          - In the Time On/Off box, set your desired half cycle time in seconds.
                    - This is how long the chiller will spend on and off. A time input of 1800 seconds will turn the chiller on for 30 minutes, then off for 30 minutes. 
          - The # of Cycles button lets you control how many times you want the chiller to be on and off. 
                    - Following the previous example, an input of 2 will have the chiller turn on for 30 minutes and off for 30 minutes twice for a total of 2 hours. 
                              
**Arduino Serial:**

          - Using the drop down box, select port attributed to Arudino. 
          - Click open. 
                              
                            
  ### Recording Data
 **Data Log File:**
 This is where the collected data will be recorded. 
 
          -Either open a preexisting file using the Open button, or create a New File using the New File button.
 **Collect Data, Seconds per Poll:**
 This determines how often the program will record data. 
 Using 1 second will collect data once per second. 
                    
          - Click Enable Auto Collect beneath the input box to begin taking data.
        
 **Graphs:**
The three graphs at the bottom of the interface track the temperature over time. They should begin automatically once you click Enable Auto Collect. 
        
        
