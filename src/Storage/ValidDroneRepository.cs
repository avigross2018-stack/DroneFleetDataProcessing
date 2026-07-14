

namespace DroneFleetDataProcessing.src.Storage
{
    public class ValidDroneRepository<T>
    {
        private List<T> items;

        public ValidDroneRepository()
        {
            items = new List<T>();
        }
    }
}