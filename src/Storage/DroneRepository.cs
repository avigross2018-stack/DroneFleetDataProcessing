
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

        //Adding all object and override to hte list
        public void AddToRepo(List<T> objItems)
        {
            _items = objItems;
        }

        //Returns all the list of objects
        public List<T> GetAllDrones()
            => Items;
    }
}