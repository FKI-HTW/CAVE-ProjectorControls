# Cave calibrator
Standalone GUI application to connect to Unity apps running the HTW Cave package.

## Running
To create an executable simply open the project in Visual Studio and press "run".  
The executable can then be found in `CaveCalibrator\bin\Debug\`. 
  
Note that you may need to manually install the correct `hidapi.dll` for your system in the build directory for joycon support to work.

## JoyCons  
JoyCon input is supported for fast and easy calibration from inside of the cave.  
The button mapping can be found in application by clicking on the `?` or [here](CaveCalibrator/Resources/joycon_help.png)

## Credits
Alot code of the original calibration software for Unity editor of [dn9090](https://github.com/dn9090) could be reused in this project, including the SimpleTcp client and big parts of the protocol.

Additionally, credits to Looking-Glass, the creator of JoyConLib for reading JoyCon inputs via bluetooth.

## Useful links
https://gitlab.rz.htw-berlin.de/cave/cave
