

namespace DroneFleetDataProcessing.src.Storage
{
    public class ValidDroneRepository<T>
    {
        private List<T> _items;
        public List<T> Items { get; }

        public ValidDroneRepository()
        {
            _items = new List<T>();
        }

        public void AddToRepo(T obj)
        {
            _items.Add(obj);
        }

        public List<T> GetAllDrones()
             => Items;

    }
}