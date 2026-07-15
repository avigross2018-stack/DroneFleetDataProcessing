using DroneFleetDataProcessing.src.Models.Drones;
using DroneFleetDataProcessing.src.Storage;
using DroneFleetDataProcessing.src.Models.Drones;

namespace DroneFleetDataProcessing.Statistics
{
    public class AnalyzeReport
    {
        public string ReportStatistics;

        private ValidDroneRepository<Drone> _repository;
        private DroneRepository<Drone> _rawRepository;

        public AnalyzeReport(ValidDroneRepository<Drone> repository , DroneRepository<Drone> rawRepository)
        {
            _repository = repository;
            _rawRepository = rawRepository;
        }

        public List<Drone> NotOperationalDrones()
        {
            List<Drone> filteredList = _repository.GetAllDrones()
                .Where(d => d.Status != "OPERATIONAL")
                .ToList();
            return filteredList;
        }

        public string SummaryNotOperationalDrones(List<Drone> filteredDrones)
        {
            string summary = "";
            foreach (Drone drone in filteredDrones)
            {
                summary += $"{drone.SerialNumber} | {drone.Model} | {drone.BaseLocation} | {drone.Status}\n";
            }
            return summary;
        }

        public List<Drone> FiveTopFlightHours()
        {
            var filteredList = _repository.GetAllDrones()
               .OrderByDescending(d => d.FlightHours)
                .Take(5)
                .ToList();
            return filteredList;
        }
        public string SummaryFiveTopFlightHours(List<Drone> filteredDrones)
        {
            string summary = "";
            foreach (Drone drone in filteredDrones)
            {
                summary += $"{drone.SerialNumber} | {drone.Model} | {drone.FlightHours}\n";
            }
            return summary;
        }

        public List<string> DistinctDronesModels()
        {
            List<string> filteredList = _repository.GetAllDrones()
                .Select(d => d.Model)
                .Distinct()
                .ToList();
            return filteredList;
        }
        public string SummaryDistinctDronesModels(List<string> models)
        {
            string modelsSummary = "";
            foreach (string model in models)
            {
                modelsSummary += $"{model}\n";
            }
            return modelsSummary;
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

        public string SummaryGetDroneByBase(Dictionary<string, int> BaseAndNumber)
        {
            string summary = "";
            foreach (KeyValuePair<string, int> item in BaseAndNumber)
            {
                summary += $"{item.Key} | {item.Value}\n";
            }

            return summary;
        }


        public string GetSummary()
        {
            string totalSummaryReport =
                $"""
                DRONE FLEET ANALYSIS REPORT

                RPROCESSING SUMMARY
                Total raw records: {_rawRepository.GetAllDrones().Count()}
                Valid records:  {_repository.GetAllDrones().Count()}
                Rejected records {_rawRepository.GetAllDrones().Count()- _repository.GetAllDrones().Count()}

                NON-OPERATIONAL DRONES
                {SummaryNotOperationalDrones(NotOperationalDrones())}

                TOP 5 DRONES BY FLIGHT HOURS
                {SummaryFiveTopFlightHours(FiveTopFlightHours())}

                AVAILABLE DRONE MODELS
                {SummaryDistinctDronesModels(DistinctDronesModels())}


                """;
       

                
        }
    }


}