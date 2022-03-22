# xpiritBlinkyCommand

## The Problem, this solves
This is a simple solution on getting ones "Disturb"-status. The idea behind is born in our daily "HomeOffice vs. family" conflicts (maybe you know 😀 ).
When you are in an important meeting, where you think background noise will disturb. 
And explaining your members, not to come in is also no option, then you need a kind of indicator, that your nearest now.

## Resolution
Here comes Blinky into play <img src="./BlinkyOnDesk.png" width="200px" style="float:right" />   
That device is a Xpirit - IoT - Fun device, that our members got as gift from our IoT Practice.    
We are all enthusiasts and playing arround with tones of technologies and try to bring it a bit in shape, as this Iot-connected device in form of an esthetic sign.

## How to run

Connect the Blinky to some sort of power (runs with 5V, so usually Powerpacks for mobiles are good enough).

In a couple of seconds it is connected with an Azure IoTHub and ready to receive commands.

By making use of desired properties of a device twin, this can be set to any color and several lightmodes.

## Local setup

Grab the console app, written in .NET 6, from the latest release. Grab the AAD App details 
- TenantId
- ClientId
- ClientSecret 
- IotHub Hostname
- Device Id

and put them into environment variables on your system.

(e.g. MacOs/Linux)   
```
export AZURE_CLIENT_ID=<ClientId>
export AZURE_CLIENT_SECRET=<ClientSecret>
export AZURE_TENANT_ID=<TenantId>
export AZURE_HOSTNAME=<IoTHub Hostname>
export AZURE_DEVICE_ID=<you blinky Id>
```

## Usage

use the console app with a terminal/cmd/powershell ....
```
xpiritBlinkyCommand (options) [-r]

    -h/ --hue (int)   sets base color
    -b/ --brightness (int)   sets the brightness of the color
    -s/ --saturation (int)   sets the saturation of the color
    -r/ --reset (just a flag - like bool)   goes back to breathing mode in awesomeOrange
```
(for MacOs, this makes Blinky light up in red)   
`dotnet xpiritBlinkyCommand.dll -h 255 -b 250 -s 250`
   
(for MacOs, this resets Blinky)   
`dotnet xpiritBlinkyCommand.dll -r`

## Running example
just look at this:
![Example video](./Blinky.gif)