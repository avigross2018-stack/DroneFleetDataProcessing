using DroneFleetDataProcessing.src.Storage;
using DroneFleetDataProcessing.src.Models.Drones;
using DroneFleetDataProcessing.src.Storage;
using DroneFleetDataProcessing.src.Validations;

namespace DroneFleetDataProcessing.src.Pipeline
{
    public class Pipeline
    {
        private IDataHandler dataHandler;

        private DroneRepository<Drone> _droneRepository;
        private ValidDroneRepository<Drone> _validDroneRepository;
        private DroneValidator _droneValidator;


        public Pipeline(IDataHandler dataHandler)
        {
            this.dataHandler = dataHandler;

            _droneRepository = new DroneRepository<Drone>();
            _validDroneRepository = new ValidDroneRepository<Drone>();
            _droneValidator = new DroneValidator();

        }

        public void loadFileToRepo(string path) //Mybe bool flag?
        {
            List<Drone> objList = dataHandler.Load<Drone>(path);
            _droneRepository.AddToRepo(objList); // this is overriding the exists object in the repo if exists
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


    }
}