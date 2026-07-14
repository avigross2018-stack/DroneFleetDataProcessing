using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DroneFleetDataProcessing.src.Models.Drones;

namespace DroneFleetDataProcessing.src.Validations
{
    public class DroneValidator
    {
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

        public bool ValidateSerialNumber(Drone drone)
        {
            if(!string.IsNullOrWhiteSpace(drone.SerialNumber) || 
                drone.SerialNumber[0..2] != drone.SerialNumber[0..2].ToUpper() ||
                drone.SerialNumber.Count() != 7 ||
                drone.SerialNumber[2] != '-' ||
                !int.TryParse(drone.SerialNumber[3..], out _) ||
                drone.SerialNumber[3..].Count() != 4)
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

        public bool ValidateModel(Drone drone)
        {
            string[] models = ["Falcon-X", "Raven-M", "SkyEye-2", "CargoBee", "Storm-4", "Scout-Lite"];

            if(!models.Contains(drone.Model)){return false;}
            return true;
        }

        public bool ValidateCategory(Drone drone)
        {
            string[] categories = ["Recon", "Patrol", "Mapping", "Delivery", "Search"];

            if(!categories.Contains(drone.Category)){return false;}
            return true;
        }

        public bool ValidateBaseLocation(Drone drone)
        {
            string[] BaseLocations = ["North", "South", "Central", "East", "West"];

            if(!BaseLocations.Contains(drone.Category)){return false;}
            return true;
        }

        public bool ValidateFlightHours(Drone drone)
        {
            const int MIN_FLIGHT_HOURS = 0;
            const int MAX_FLIGHT_HOURS = 2500;
            if(drone.FlightHours < MIN_FLIGHT_HOURS || drone.FlightHours > MAX_FLIGHT_HOURS)
            {
                return false;
            }
            return true;
        }

        public bool ValidateBatteryHealth(Drone drone)
        {
            const int MIN_BATTERY_HEALTH = 0;
            const int MAX_BATTERY_HEALTH = 100;
            if(drone.BatteryHealth < MIN_BATTERY_HEALTH || drone.BatteryHealth > MAX_BATTERY_HEALTH)
            {
                return false;
            }
            return true;
        }

        public bool ValidateMaxRangeKm(Drone drone)
        {
            const int MIN_MAX_RANGE = 1;
            const int MAX_MAX_RANGE = 150;
            if(drone.MaxRangeKm < MIN_MAX_RANGE || drone.MaxRangeKm > MAX_MAX_RANGE)
            {
                return false;
            }
            return true;
        }

        public bool ValidateMissionCompleted(Drone drone)
        {
            const int MIN_MISSIONS_COMPLETED = 0;
            const int MAX_MISSIONS_COMPLETED = 5000;
            if(drone.MissionsCompleted < MIN_MISSIONS_COMPLETED || drone.MissionsCompleted > MAX_MISSIONS_COMPLETED)
            {
                return false;
            }
            return true;
        }

        public bool ValidateStatus(Drone drone)
        {
            string[] statusList = ["Operational", "Maintenance", "Grounded", "Training"];

            if(!statusList.Contains(drone.Status)){return false;}
            return true;
        }

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