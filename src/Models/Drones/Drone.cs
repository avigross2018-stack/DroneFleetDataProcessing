using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace DroneFleetDataProcessing.src.Models.Drones
{
    public class Drone
    {
        [JsonPropertyName("id")]
        public int Id { get; set;}

        [JsonPropertyName("serialNumber")]
        public string SerialNumber { get; set;}

        [JsonPropertyName("model")]
        public string Model { get; set;}

        [JsonPropertyName("category")]
        public string Category { get; set;}

        [JsonPropertyName("base_location")]
        public string BaseLocation { get; set;}

        [JsonPropertyName("flightHours")]
        public double FlightHours { get; set;}

        [JsonPropertyName("batteryHealth")]
        public int BatteryHealth { get; set;}

        [JsonPropertyName("maxRangeKm")]
        public double MaxRangeKm { get; set;}

        [JsonPropertyName("missionsCompleted")]
        public int MissionsCompleted { get; set;}

        [JsonPropertyName("status")]
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