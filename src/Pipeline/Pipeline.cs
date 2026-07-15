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

        //This fucntion call to the loader data and set the list of the objects
        //into the raw data reository
        public void LoadFileToRepo(string path) 
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

        //This methid filtering only the valid data and and adds them to the valid repo
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
        //The adding to the valid repo object one by one
        public void AddToValidRepo(Drone obj)
        {   
            _validDroneRepository.AddToRepo(obj);
        }

        //This fucntion saves the list object into file (Seralization)
        public void ToOutputFile(string path)
        {
            List<Drone> allDrones = _validDroneRepository.GetAllDrones();

            dataHandler.Save(path, allDrones);
        }
        //This helper function check if the object list is empty if yes throws exception
        public void CheckEmptyValidList()
        {

            if(_validDroneRepository.GetAllDrones().Count == 0)
            {
                System.Console.WriteLine("Valid records: 0");
                System.Console.WriteLine($"Rejected records: {_droneRepository.GetAllDrones().Count}");
                throw new ArgumentException("No valid records found for analysis!");
            }
        }

        //This method is the run flow of the whole program throught the pipeline
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