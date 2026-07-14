## Drone Fleet Data Processing

# Division of work in the team #

Storage/ -- Yoel (joeLebin42)
	JsonTool.cs
	DroneReposiotry.cs
	ValidDroneRepository.cs

Models/drone -- Avi Gross
	drone.cs

| Validations/ -- Avi Gross |
| ------------------------- |
| -                         |
| -                         |
| -                         |

# Classes

***Class Drone:***

Fields.
int id, => (>0 , Unique)
string serialNumber, (No Empty || Null, Unique, Format = DR-xxxx, )
string model,
string category,
string baseLocation,
double flightHours,
int batteryHealth,
double maxRangeKm,
int missionsCompleted,
string status,
