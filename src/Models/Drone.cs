using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DroneFleetDataProcessing.src.Models
{
    public class Drone
    {
        public int Id { get; set;}
        public string SerialNumber { get; set;}
        public string Model { get; set;}
        public string Category { get; set;}
        public string BaseLocation { get; set;}
        public double FlightHours { get; set;}
        public int BatteryHealth { get; set;}
        public double MaxRangeKm { get; set;}
        public int MissionsCompleted { get; set;}
        public string Status { get; set;}

        public Drone(){}

        public Drone(
            int id,
            string serialNumber,
            string model,
            string category,
            string baseLocation,
            double flightHours,
            int batteryHealth,
            double maxRangeKm,
            int missionsCompleted,
            string status)
        {
            Id = id;
            SerialNumber = serialNumber;
            Model = model;
            Category = category;
            BaseLocation = baseLocation;
            FlightHours = flightHours;
            BatteryHealth = batteryHealth;
            MaxRangeKm = maxRangeKm;
            MissionsCompleted = missionsCompleted;
            Status = status;
        }
    }
}