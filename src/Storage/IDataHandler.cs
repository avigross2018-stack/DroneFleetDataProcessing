
namespace DroneFleetDataProcessing.src.Storage
{
    public interface IDataHandler
    {
        List<T> Load<T>(string path);
        void Save<T>(string path, List<T> data);
    }
}

