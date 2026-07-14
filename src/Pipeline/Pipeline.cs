using DroneFleetDataProcessing.src.Storage;
using DroneFleetDataProcessing.src.Models.Drones;
using DroneFleetDataProcessing.src.Storage;

namespace DroneFleetDataProcessing.src.Pipeline
{
    public class Pipeline
    {
        private IDataHandler dataHandler;

        private DroneRepository<Drone> droneRepository;
        private ValidDroneRepository<Drone> validDroneRepository;
        //DroneValidator droneValidator = new DroneValidator();


        public Pipeline(IDataHandler dataHandler)
        {
            this.dataHandler = dataHandler;

            droneRepository = new DroneRepository<Drone>();
            validDroneRepository = new ValidDroneRepository<Drone>();
        }

        public void loadFileToRepo(string path) //Mybe bool flag?
        {
            List<Drone> objList = dataHandler.Load<Drone>(path);
            droneRepository.AddToRepo(objList); // this is overriding the exists object in the repo if exists
        }
        
        //public void FilterAddValidRepo(List<Drone> drones)
        //{
        //    foreach(Drone obj in drones)
        //    {
        //        if (droneValidator.ValidateAll(obj))
        //        {
        //            AddToValidRepo(obj);
        //        }
        //    }
        //}

        public void AddToValidRepo(Drone obj)
        {   
            validDroneRepository.AddToRepo(obj);
        }

        public void ToOututFile(string path)
        {
            List<Drone> allDrones = validDroneRepository.Items;

            dataHandler.Save(path, allDrones);
        }


    }
}