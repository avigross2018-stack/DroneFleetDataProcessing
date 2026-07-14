
namespace DroneFleetDataProcessing.Storage
{
    public class DroneRepository<T>
    {
        private List<T> items;

        public DroneRepository()
        {
            items = new List<T>();
        }
    }
}