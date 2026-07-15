

namespace DroneFleetDataProcessing.src.Storage
{
    public class ValidDroneRepository<T>
    {
        private List<T> _items;
        public List<T> Items { get=> _items; }

        public ValidDroneRepository()
        {
            _items = new List<T>();
        }

        //Adding one object to repo
        public void AddToRepo(T obj)
        {
            _items.Add(obj);
        }
        //Returns all the object list
        public List<T> GetAllDrones()
             => Items;

    }
}