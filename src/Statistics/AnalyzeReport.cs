using DroneFleetDataProcessing.src.Models.Drones;
using DroneFleetDataProcessing.src.Storage;
using DroneFleetDataProcessing.src.Models.Drones;

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
    }
}