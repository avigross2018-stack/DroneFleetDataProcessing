# Drone Fleet Data Processing

## Team Work Division


## Project Structure & Developer Responsibilities


## Work Team Division

Program  
 └── Program.cs — Avi Gross

Pipeline  
 └── Pipeline.cs — Yoel (joeLevin42)

Storage  
 ├── IDataHandler.cs — Yoel (joeLevin42)  
 ├── JsonTool.cs — Yoel (joeLevin42)  
 ├── DroneRepository.cs — Yoel (joeLevin42)  
 └── ValidDroneRepository.cs — Yoel (joeLevin42)

Models  
 └── Drone.cs — Avi Gross

Validations  
 └── DroneValidator.cs — Avi Gross

Paths  
 └── PathHolder.cs — Avi Gross

Statistics  
 └── AnalyzeReport.cs — Yoel (joeLevin42), Avi Gross


---

# System Overview

The Drone Fleet Data Processing System is a console application that processes drone fleet data stored in JSON files.

The system performs the following steps:

1. Reads drone data from a JSON file.
2. Validates every drone according to business rules.
3. Stores only valid drones.
4. Saves the cleaned drone list into a new JSON file.
5. Generates statistics about the valid drones.

---

# Project Structure

```
Program
│
├── Models
│   └── Drone
│
├── Paths
│   └── PathHolder
│
├── Storage
│   ├── IDataHandler
│   ├── JsonTool
│   ├── DroneRepository
│   └── ValidDroneRepository
│
├── Validations
│   └── DroneValidator
│
├── Pipeline
│   └── Pipeline
│
└── Statistics
    └── AnalyzeReport
```

---

# Classes

---

## Drone

### Purpose

Represents one drone in the system.

### Main Properties

- Id
- SerialNumber
- Model
- Category
- BaseLocation
- FlightHours
- BatteryHealth
- MaxRangeKm
- MissionsCompleted
- Status

### Methods

| Method | Description |
|---------|-------------|
| Drone() | Default constructor used when reading JSON. |
| Drone(...) | Creates a drone by setting all its properties. |

### Dependencies

None.

---

## PathHolder

### Purpose

Stores all file paths used by the project.

### Methods

| Method | Description |
|---------|-------------|
| InputRawDroneJson() | Returns the raw input file path. |
| InputAllInvalid() | Returns the "all invalid" test file path. |
| InputDronesEmpty() | Returns the empty test file path. |
| InputDronesMalformed() | Returns the malformed JSON test file path. |
| InputDronesNull() | Returns the null test file path. |
| OutputDronesClean() | Returns the cleaned output JSON path. |
| OutputAnalyze() | Returns the analysis report path. |

### Dependencies

Uses `Directory`, `Path`, and `AppDomain` to build project paths.

---

## IDataHandler

### Purpose

Defines how data files are loaded and saved.

### Methods

| Method | Description |
|---------|-------------|
| Load() | Reads objects from a file. |
| Save() | Saves objects into a file. |

### Dependencies

None.

---

## JsonTool

### Purpose

Reads and writes JSON files.

### Methods

| Method | Description |
|---------|-------------|
| Load() | Reads a JSON file and converts it into objects. |
| Save() | Converts objects into JSON and saves them. |

### Dependencies

- IDataHandler
- JsonSerializer
- File

---

## DroneRepository

### Purpose

Stores every drone loaded from the input file.

### Methods

| Method | Description |
|---------|-------------|
| AddToRepo() | Stores the loaded drones. |
| GetAllDrones() | Returns all stored drones. |

### Dependencies

None.

---

## ValidDroneRepository

### Purpose

Stores only drones that passed validation.

### Methods

| Method | Description |
|---------|-------------|
| AddToRepo() | Adds one validated drone. |
| GetAllDrones() | Returns all validated drones. |

### Dependencies

None.

---

## DroneValidator

### Purpose

Checks whether a drone satisfies all business rules.

### Methods

| Method | Description |
|---------|-------------|
| ValidateAll() | Runs every validation. |
| ValidateId() | Checks that the ID is valid and unique. |
| ValidateSerialNumber() | Checks the serial number format and uniqueness. |
| ValidateModel() | Checks the drone model. |
| ValidateCategory() | Checks the category. |
| ValidateBaseLocation() | Checks the base location. |
| ValidateFlightHours() | Checks the flight hours range. |
| ValidateBatteryHealth() | Checks the battery health range. |
| ValidateMaxRangeKm() | Checks the maximum range. |
| ValidateMissionCompleted() | Checks completed missions. |
| ValidateStatus() | Checks the drone status. |
| ValidateOperateByHealth() | Ensures operational drones have enough battery health. |

### Dependencies

Uses the `Drone` class.

---

## Pipeline

### Purpose

Controls the entire data processing workflow.

### Methods

| Method | Description |
|---------|-------------|
| LoadFileToRepo() | Loads drones from a JSON file into the raw repository. |
| FilterAddValidRepo() | Validates every drone and stores valid drones. |
| AddToValidRepo() | Adds one drone into the valid repository. |
| ToOutputFile() | Saves valid drones into the output JSON file. |
| Run() | Runs the complete processing flow. |

### Dependencies

- IDataHandler
- DroneRepository
- ValidDroneRepository
- DroneValidator

---

## AnalyzeReport

### Purpose

Generates statistics and summary reports for the validated drones.

### Methods

| Method | Description |
|---------|-------------|
| NotOperationalDrones() | Returns drones that are not operational. |
| FiveTopFlightHours() | Returns the five drones with the most flight hours. |
| DistinctDronesModels() | Returns all unique drone models. |
| GetDroneByBase() | Counts drones in each base. |
| AvgModelBatteryHealth() | Calculates average battery health for each model. |
| ModelWithMostMissionsCompleted() | Finds the model with the most completed missions. |
| ThreeModelsHighestAvg() | Returns the three models with the highest average flight hours. |
| GetSummary() | Creates the complete analysis report. |

### Dependencies

- DroneRepository
- ValidDroneRepository

---

## Program

### Purpose

Application entry point.

### Method

| Method | Description |
|---------|-------------|
| Main() | Creates the required objects and starts the pipeline. |

### Dependencies

- Pipeline
- JsonTool
- PathHolder

---

# Dependency Diagram

```
                Program
                   │
                   ▼
               Pipeline
         ┌────────┼─────────┐
         ▼        ▼         ▼
   IDataHandler  Validator  Repositories
         │          │            │
         ▼          ▼            ▼
     JsonTool     Drone      DroneRepository
                                │
                                ▼
                      ValidDroneRepository
                                │
                                ▼
                         AnalyzeReport
```

---

# Program Flow

```
Program.Main()
        │
        ▼
Create JsonTool
        │
        ▼
Create Pipeline
        │
        ▼
Pipeline.Run()
        │
        ▼
Read JSON file
        │
        ▼
Store all drones
        │
        ▼
Validate every drone
        │
        ├── Invalid → Discard
        │
        ▼
Store valid drones
        │
        ▼
Generate statistics
        │
        ▼
Save cleaned JSON
        │   
        ▼
Program Ends
```

---

# Design Summary

The project follows a layered architecture where every class has a single responsibility.

- **Model** represents drone data.
- **Storage** manages reading, writing, and storing data.
- **Validation** checks all business rules.
- **Pipeline** controls the application workflow.
- **Statistics** creates analysis reports.
- **Paths** stores all project file locations.
- **Program** starts the application.

The **Pipeline** acts as the main controller and connects all parts of the system.
