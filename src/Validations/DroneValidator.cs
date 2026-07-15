using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DroneFleetDataProcessing.src.Models.Drones;

namespace DroneFleetDataProcessing.src.Validations
{
    /// <summary>
    /// This class validate all business roles of drone.
    /// </summary>
    public class DroneValidator
    {
        //CONST
        private const int MIN_FLIGHT_HOURS = 0;
        private const int MAX_FLIGHT_HOURS = 2500;
        private const int MIN_BATTERY_HEALTH = 0;
        private const int MAX_BATTERY_HEALTH = 100;
        private const int MIN_MAX_RANGE = 1;
        private const int MAX_MAX_RANGE = 150;
        private const int MIN_MISSIONS_COMPLETED = 0;
        private const int MAX_MISSIONS_COMPLETED = 5000;

        public List<int> IdNums{ get; set;}
        public List<string> SerialNumbers{ get; set;}

        public DroneValidator()
        {
            IdNums = new();
            SerialNumbers = new();
        }

        public bool ValidateAll(Drone drone)
        {
            if(
                ValidateId(drone) &&
                ValidateSerialNumber(drone) &&
                ValidateModel(drone) &&
                ValidateCategory(drone) &&
                ValidateBaseLocation(drone) &&
                ValidateFlightHours(drone) &&
                ValidateBatteryHealth(drone) &&
                ValidateMaxRangeKm(drone) &&
                ValidateMissionCompleted(drone) &&
                ValidateStatus(drone) &&
                ValidateOperateByHealth(drone)
            )
            {
                return true;
            }
            return false;
        }

        // Validate unique ID and ID > 0.
        public bool ValidateId(Drone drone)
        {
            if(drone.Id <= 0)
            {

                return false;
            }
            if (!IdNums.Contains(drone.Id))
            {
                IdNums.Add(drone.Id);
                return true;
            }
            return false;
        }

        // Validate SerialNumber if unique, not empty, 2 starting letters in uppercase,
        // length is 7, last 4 characters are numbers.
        public bool ValidateSerialNumber(Drone drone)
        {
            if(string.IsNullOrWhiteSpace(drone.SerialNumber) || 
                drone.SerialNumber[0..2] != drone.SerialNumber[0..2].ToUpper() ||
                drone.SerialNumber.Length != 7 ||
                drone.SerialNumber[2] != '-' ||
                !int.TryParse(drone.SerialNumber[3..], out _) ||
                drone.SerialNumber[3..].Length != 4)
            {
                return false;
            }
            if (SerialNumbers.Contains(drone.SerialNumber))
            {
                return false;
            }
            SerialNumbers.Add(drone.SerialNumber);
            return true; 
        }

        // Validate if model is valid input.
        public bool ValidateModel(Drone drone)
        {
            string[] models = ["Falcon-X", "Raven-M", "SkyEye-2", "CargoBee", "Storm-4", "Scout-Lite"];

            if(!models.Contains(drone.Model)){return false;}
            return true;
        }

        // Validate if category is valid input. 
        public bool ValidateCategory(Drone drone)
        {
            string[] categories = ["Recon", "Patrol", "Mapping", "Delivery", "Search"];

            if(!categories.Contains(drone.Category)){return false;}
            return true;
        }

        //Validate if base location is valid input.
        public bool ValidateBaseLocation(Drone drone)
        {
            string[] BaseLocations = ["North", "South", "Central", "East", "West"];

            if(!BaseLocations.Contains(drone.BaseLocation)){return false;}
            return true;
        }

        // Validate if flight hours is > 0 && < 2500.
        public bool ValidateFlightHours(Drone drone)
        {
            if(drone.FlightHours < MIN_FLIGHT_HOURS || drone.FlightHours > MAX_FLIGHT_HOURS)
            {
                return false;
            }
            return true;
        }

        // Validate if battery health is > 0 && < 100.
        public bool ValidateBatteryHealth(Drone drone)
        {
            if(drone.BatteryHealth < MIN_BATTERY_HEALTH || drone.BatteryHealth > MAX_BATTERY_HEALTH)
            {
                return false;
            }
            return true;
        }

        // Validate if max range km is > 1 && < 150.
        public bool ValidateMaxRangeKm(Drone drone)
        {
            if(drone.MaxRangeKm < MIN_MAX_RANGE || drone.MaxRangeKm > MAX_MAX_RANGE)
            {
                return false;
            }
            return true;
        }

        // Validate if flight hours is > 0 && < 2500.
        public bool ValidateMissionCompleted(Drone drone)
        {
            if(drone.MissionsCompleted < MIN_MISSIONS_COMPLETED || drone.MissionsCompleted > MAX_MISSIONS_COMPLETED)
            {
                return false;
            }
            return true;
        }

        // Validate if status is valid input. 
        public bool ValidateStatus(Drone drone)
        {
            string[] statusList = ["Operational", "Maintenance", "Grounded", "Training"];

            if(!statusList.Contains(drone.Status)){return false;}
            return true;
        }

        // Validate if status operational && battery health > 20.
        public bool ValidateOperateByHealth(Drone drone)
        {
            if(drone.Status == "Operational" && drone.BatteryHealth < 20)
            {
                return false;
            }
            return true;
        }
    } 
}