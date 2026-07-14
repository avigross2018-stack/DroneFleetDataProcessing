## Drone Fleet Data Processing

# Division of work in the team

Storage/ -- Yoel (joeLevin42)
	JsonTool.cs
	DroneReposiotry.cs
	ValidDroneRepository.cs

Models/drone -- Avi Gross
	drone.cs

Pipeline/
	Pipeline.cs



| Validations/ -- Avi Gross |
| ------------------------- |
| -                         |
| -                         |
| -                         |

## Classes

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

Validations/ -- Avi Gross
	
Pipeline/ 


| Validations/ -- Avi Gross |
| ------------------------- |
| -                         |
| -                         |
| -                         |

## JsonTool

`JsonTool` is a generic utility class responsible for handling JSON file operations.

The class provides two main functions:

- **Serialization**: Converts a list of objects into a formatted JSON file using `JsonSerializer`.
- **Deserialization**: Reads a JSON file and converts its content back into a generic list of objects.

The class uses generics (`T`) so it can work with any object type without needing separate implementations for different classes.

### Methods

### `FromJson<T>(string path)`

Reads a JSON file from the given path and deserializes its content into a `List<T>`.

- Returns a list of objects if the operation succeeds.
- Returns an empty list if an error occurs.
- Handles common exceptions such as:
  - Invalid JSON format (`JsonException`)
  - Missing files (`FileNotFoundException`)
  - File access problems (`IOException`, `UnauthorizedAccessException`)
  - Unsupported object types (`NotSupportedException`)

---

### `ToJson<T>(string path, List<T> objList)`

Serializes a list of objects into a JSON file.

Features:

- Supports any object type using generics.
- Creates readable formatted JSON using `WriteIndented = true`.
- Handles common serialization and file-writing errors.
