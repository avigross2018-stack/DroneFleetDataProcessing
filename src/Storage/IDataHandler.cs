
namespace DroneFleetDataProcessing.src.Storage
{
    public interface IDataHandler // Must to handel with data files
    {
        List<T> Load<T>(string path);
        void Save<T>(string path, List<T> data);
    }
}

