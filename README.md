# TelemetryDeck SDK for Unity C# - Example TelemetryClient project

## Lightweight Analytics That's Not Evil

Please visit [TelemetryDeck.com](https://telemetrydeck.com/) to learn more.

## About this repository

This repository contains the development project for the TelemetryDeck Unity C# SDK, as well as an example usage of the API (see [TestApp](/TelemetryClient/Assets/TestApp/) folder).

To add TelemetryDeck to your project, please visit the [TelemetryDeck Unity C# SDK](https://github.com/conath/TelemetryClient-for-UnityCSharp/) repository.

This repository is updated to UNITY 2022.3 and has its dependencies trimmed down to minimum.

## Dependencies

Please see the [README of the TelemetryDeck Unity C# SDK](https://github.com/TelemetryDeck/UnityCSharpSDK).  
`Newtonsoft.Json` dependcy is now easier to use as it is built-in UNITY Package Manager package.

## Cloning

You must clone with submodules to receive a working Unity Project:

`git clone https://github.com/jpetays/UnityCSharpSDK-Example.git --recurse-submodules`

### Submodules

We have a 'library' dependecy to UNITY TelemetryDeck SDK implementation here (in`.gitmodules`):

````
path = TelemetryClient/Assets/TelemetryClient
url = https://github.com/conath/TelemetryClient-for-UnityCSharp.git
````

Alternatively you can copy required SDK files manully where you want or for example import UNITY package. 

## License

TelemetryDeck SDK for Unity C# and the sample code is licensed unter a [MIT No Attribution License](/LICENSE.md).

This means you can use the Unity Package or source code in your projects without including the license text.

Of course, attribution is very much appreciated. <3

## 3rd Party Packages

This project uses the [Newtonsoft.Json for Unity](https://docs.unity3d.com/Packages/com.unity.nuget.newtonsoft-json@3.0/manual/index.html) package (aka Json.NET).  
It is built-in UNITY package.
