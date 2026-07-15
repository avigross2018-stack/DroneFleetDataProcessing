using DroneFleetDataProcessing.src.Storage;
using DroneFleetDataProcessing.src.Models.Drones;
using DroneFleetDataProcessing.src.Validations;
using DroneFleetDataProcessing.src.Statistics;

namespace DroneFleetDataProcessing.src.Pipeline
{
    public class Pipeline
    {
        private IDataHandler dataHandler;

        private DroneRepository<Drone> _droneRepository;
        private ValidDroneRepository<Drone> _validDroneRepository;
        private DroneValidator _droneValidator;
        private AnalyzeReport _analyzeReport;


        public Pipeline(IDataHandler dataHandler)
        {
            this.dataHandler = dataHandler;

            _droneRepository = new DroneRepository<Drone>();
            _validDroneRepository = new ValidDroneRepository<Drone>();
            _droneValidator = new DroneValidator();
            _analyzeReport = new(_validDroneRepository, _droneRepository);

        }

        public void LoadFileToRepo(string path) //Mybe bool flag?
        {
            try{
                List<Drone> objList = dataHandler.Load<Drone>(path);
                _droneRepository.AddToRepo(objList); // this is overriding the exists object in the repo if exists
            }
            catch(Exception ex)
            {
                System.Console.WriteLine($"Error: {ex.Message}");
                Environment.Exit(1);
            }
        }

        public void FilterAddValidRepo(List<Drone> drones)
        {
            foreach (Drone obj in drones)
            {
                if (_droneValidator.ValidateAll(obj))
                {
                    AddToValidRepo(obj);
                }

            }
        }

        public void AddToValidRepo(Drone obj)
        {   
            _validDroneRepository.AddToRepo(obj);
        }

        public void ToOutputFile(string path)
        {
            List<Drone> allDrones = _validDroneRepository.GetAllDrones();

            dataHandler.Save(path, allDrones);
        }

        public void CheckEmptyValidList()
        {

            if(_validDroneRepository.GetAllDrones().Count == 0)
            {
                System.Console.WriteLine("Valid records: 0");
                System.Console.WriteLine($"Rejected records: {_droneRepository.GetAllDrones().Count}");
                throw new ArgumentException("No valid records found for analysis!");
            }
        }


        public void Run(string inputPath , string outPath , string analyzePath)
        {
            System.Console.WriteLine("=== Drone Fleet Data Processing System ===\n");
            System.Console.WriteLine("Step 1: Reading raw data... Read records from raw file\n");
            LoadFileToRepo(inputPath); // This insert to the raw repo

            try{
                System.Console.WriteLine("Step 2: Validating data and creating clean dataset... Valid records: Rejected records\n");
                FilterAddValidRepo(_droneRepository.GetAllDrones()); // This Filtering only the valid repo and insert to the ValidRepo
                CheckEmptyValidList();
            }
            catch(Exception ex)
            {
                System.Console.WriteLine($"Error: {ex.Message}");
                Environment.Exit(1);
            }
            System.Console.WriteLine($"Step 3: Saving clean data... Clean data saved to: {outPath}\n");           
            ToOutputFile(outPath); // This stores the validated drones into output file
            System.Console.WriteLine("Step 4: Reloading clean data... Loaded records from clean dataset\n");
            System.Console.WriteLine("Step 5: Performing analysis... Analysis completed successfully\n");
            string summary = _analyzeReport.GetSummary();
            System.Console.WriteLine($"Step 6: Generating report... Report generated successfully: {analyzePath}\n");
            _analyzeReport.ToTextFile(summary, analyzePath);
            System.Console.WriteLine("=== Process completed successfully! ===");
        }
    }
}