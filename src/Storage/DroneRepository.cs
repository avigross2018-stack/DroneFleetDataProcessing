
namespace DroneFleetDataProcessing.src.Storage
{
    public class DroneRepository<T>
    {
        private List<T> _items;

        public List<T> Items { get => _items; }
        public DroneRepository()
        {
            _items = new List<T>();
        }

        public void AddToRepo(List<T> objItems)
        {
            _items = objItems;
        }

        public List<T> GetAllDrones()
            => Items;
    }
}