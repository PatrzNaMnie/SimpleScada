![N|Solid](https://i.ibb.co/9tpTRxB/Simple-Scada.png)

# User Manual

- [#Requirements](#requirements)
- [#Variables](#variables)
- [#Screens](#screens)
  * [Sub-heading](#sub-heading)
    + [Sub-sub-heading](#sub-sub-heading)

## Heading

This is an h1 heading


## Requirements 
##### SimpleScad cannot start without being connected to a real PLC that contains the appropriate control program. Tia Portal files with the appropriate program for the PLC controller are attached to the repository. 

##### The S7NetPLus library uses the TCP/IP protocol for the connection between the visualization and the PLC.

##### The pre-alpha version only allows connection to a Siemens S7-300 PLC series. Connections with others have not yet been tested and will be available soon. 
#
## Variables

##### The list of variables is available in the repository. Access to list is available by opening the .xlsx file in the Variables folder. Variables List User Guide in progress. 
#
## Screens
#
- ### Connection panel
#

![N|Solid](https://i.ibb.co/z2k93DX/1.png)
![N|Solid](https://i.ibb.co/6X1cRkY/2.png)

##### •	 CpuType allows you to select the type of Siemens PLC you want to connect to.
##### •	 IP Address is the address of PLC controller
##### •	 Rack and Slot position you can get from PLC Hardware configuration
#
##### After set correct IP Address, Rack and Slot user can establish Connection by pressing “Connection” button.  If all goes well, the Main screen will appear.
#
- ### Main screen
#
![N|Solid](https://i.ibb.co/9Tp8Gy9/3.png)

##### This screen is the first screen which we can see when we connect to SimpleScada.
#
##### 1 – Current data and time
##### 2 – Current logged user
##### 3 – Top bar alarm listopad
##### 4 – Bottom select line (Home, Trend, Alarm, Alarm History, Login, Logout, Disconnect). Administration Panel is enable only to users with administration rights.
#
- ### Home
#
![N|Solid](https://i.ibb.co/ZHGTKjW/4.png)
#

##### Home screen shows current status of devices working in this process, current measured values and the state of limit lams. Clicking on the selected device opens detailed window containing exact information about this device. 
##### Process control section includes 4 different sub-processes, clicking on the selected process opens a detailed window with process control options.
#
- ### Trends
#
![N|Solid](https://i.ibb.co/F6cKGnD/5.png)
#

##### Simple Scada can generate real-time graphs from collected data. User can choose measuring point from drop down list.
#
- ### Alarm list
#
![N|Solid](https://i.ibb.co/KDWVSwy/6.png)
#

##### Incoming alarm appears on special Alarm window and in Top Bar Alarm line. Is possible to see list of all incoming alarms by enter “Alarm” button from Bottom select line. In Alarm list user can see Data and Time the alarm was Received, name of incoming alarm variable, alarm value and alarm text. 
#
- ### Historical alarms
#
![N|Solid](https://i.ibb.co/DfJqcsp/7.png)
#

##### When the value of the variable returns to normal, the alarm disappears from the alarm list and will be automatically transferred to the database. By pushing “Alarm History” button the user will go to Alarm History list. Historical alarms are retrived from database.
#
- ### Login panel
#
![N|Solid](https://i.ibb.co/djm8GCk/8.png)
#

##### User in SimpleScada can create three types of users operators, service and administrators. To create new users, add special rights or delete exist, user must log in to the administrator account.
#
- ### Administration panel
#
![N|Solid](https://i.ibb.co/bX29tGv/9.png)
#

##### After log in to Administrator account, Administration Panel button on the Main screen will be enable. 
##### The admin panel includes a simple Crud operation.  Administrator can add new user by Setting Username, Password and Permission Level, edit exist users and finally delete users.
#
- ### Valve station
#
![N|Solid](https://i.ibb.co/8zLKgFb/10.png)
#

##### The control window of selected device appears when operator clicks on the device icon. On the control window are shown particular information about device. The operator has a preview of the current operating modes, the ability to manually turn on and turn off the device, etc.
##### In AUTO mode MODE text changes to „ AUTO” and square below icon changes to blue “A”. In MANUAL mode OPERATION MODE text changes to „ MANUAL” and square below icon changes to orange “M”.
##### In MANUAL mode ON/OFF buttons appears. Operator can switch on and switch off the device.
##### STATUS text will change depending on current drive state.
##### Blockade lamp inform about active blockade on device. 
#
- ### Pump station
#
![N|Solid](https://i.ibb.co/zs765VJ/11.png)
#

##### Running time are increases always when drive is running.  User can reset running time by pressing “RESET” button.
##### SP (Setpoint)  - drive speed in manual mode [0-100%]
##### PV – actual drive speed
#
- ### Process control – Filling Tank T1
#
![N|Solid](https://i.ibb.co/yY4n0yS/12.png)
#

##### Each process can be controllable by user. Automatic mode does not require pressing the START button. But to stop the process, the user has to press STOP. Manual mode is fully controllable, user can start or stop process in any time.
##### This process fills tank T1. When user press "AUTO" or "START" in MANUAL mode, valve UV_1 will open and the filling process will begin.
#
- ### Process control – Transfer from Tank T1 to Tank T2
#
![N|Solid](https://i.ibb.co/cTVg5BQ/13.png)
#

##### Some process stations include pump setpoints. To run process correctly, user must set the setpoint.
##### This process transfers liquid from Tank T1 to Tank T2 by using Pump P1.
#
- ### Process control – Dosing chemicals
#
![N|Solid](https://i.ibb.co/SVSt5Jy/14.png)
#

##### The chemical will be dosed into tank T2 when the pH level is less than 7 after the liquid has been transferred from T1. 
#
- ### Process control – Dosing chemicals
#
![N|Solid](https://i.ibb.co/ZMKK8JY/15.png)
#

##### To start this process, the pH of the liquid must be greater than or equal to 7, then user can empty the tank T2 with pump P2.
#



