
namespace DroneFleetDataProcessing.src.Storage
{
    public class DroneRepository<T>
    {
        private List<T> _items;

        public DroneRepository()
        {
            _items = new List<T>();
        }

           public void AddToRepo(List<T> objItems)
        {
            _items = objItems;
        }
    }
}