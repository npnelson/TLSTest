# TLSTest
This program will make two simultaneous TLS1.2 connections to the specified IPAddress and Port and report when each connection succeeded (or failed).

To build the software, you must have the .NET Core 2.0 SDK installed http://dot.net. You can build with the SDK directly, or use Visual Studio 2017 or Visual Studio Code. This code is cross platform and will run on any platform supported by .NET Core 2.0. To run compiled software, the machine must have either the .NET Core 2.0 runtime (which is included with the .NET Core 2.0 SDK) or publish the app as a self-contained app.

# Usage:
If using with dotnet runtime installed, once you build, run 
dotnet TLSTest.dll -i [IPAddress:Port]
# Example

To test 127.0.0.1 on 443 use

dotnet TLSTest.dll -i 127.0.0.1:443

