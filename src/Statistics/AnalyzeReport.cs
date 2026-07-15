using DroneFleetDataProcessing.src.Models.Drones;
using DroneFleetDataProcessing.src.Storage;
using DroneFleetDataProcessing.src.Models.Drones;
using Microsoft.VisualBasic;
using System.Text.RegularExpressions;

namespace DroneFleetDataProcessing.Statistics
{
    public class AnalyzeReport
    {
        public string ReportStatistics;

        private ValidDroneRepository<Drone> _repository;

        public AnalyzeReport(ValidDroneRepository<Drone> repository)
        {
            _repository = repository;
        }

        public List<Drone> NotOperationalDrones()
        {
            List<Drone> filteredList = _repository.GetAllDrones()
                .Where(d => d.Status != "OPERATIONAL")
                .ToList();
            return filteredList;
        }

        public List<Drone> FiveTopFlightHoures()
        {
            var filteredList = _repository.GetAllDrones()
               .OrderByDescending(d => d.FlightHours)
                .Take(5)
                .ToList();
            return filteredList;
        }

        public List<string> DistinctDronesModels()
        {
            List<string> filteredList = _repository.GetAllDrones()
                .Select(d => d.Model)
                .Distinct()
                .ToList();
            return filteredList;
        }

        public Dictionary<string,int> GetDroneByBase()
        {
            var filteredList = _repository.GetAllDrones()
                .GroupBy(d => d.BaseLocation)
                .ToDictionary(
                group => group.Key,
                group => group.Count()
                );
            return filteredList;
        }

        public Dictionary<string,double> AvgModelBatteryHealth()
        {
            var filteredList = _repository.GetAllDrones()
                .GroupBy(d => d.Model)
                .ToDictionary(
                g => g.Key,
                g => g.Average(d => d.BatteryHealth)
                );
            return filteredList;
        }

        public KeyValuePair<string, int> ModelWithMostMissionsCompleted()
        {
            var filteredList = _repository.GetAllDrones()
                .GroupBy(d => d.Model)
                .Select(
                g => new KeyValuePair<string, int>(g.Key, g.Sum(d => d.MissionsCompleted))
                )
                .OrderByDescending(kvp => kvp.Value)
                .FirstOrDefault();
            return filteredList;
        }

        public IEnumerable<KeyValuePair<string, double>> ThreeModelsHighestAvg()
        {
            var filteredList = _repository.GetAllDrones()
                .GroupBy(d => d.Model)
                .Select(
                    g => new KeyValuePair<string, double>(g.Key.ToString(), g.Average(d => d.FlightHours))
                )
                .OrderByDescending(d => d.Value)
                .Take(3);
            return filteredList;
        }
    }
}