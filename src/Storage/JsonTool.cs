

using System.Text.Json;

namespace DroneFleetDataProcessing.src.Storage
{
    class JsonTool : IDataHandler
    {
        public List<T> Load<T>(string path) //This return empty list in catch case
        {
            try
            {
                string json = File.ReadAllText(path);
                List<T> objList = JsonSerializer.Deserialize<List<T>>(json);

                return objList;
            }

            catch (JsonException ex)
            {
                Console.WriteLine($"Error: {ex.Message}"); //TODO reThorw?????
                
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            } 
            catch (IOException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            catch (NotSupportedException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            return new List<T>();
            }

        public void Save<T>(string path, List<T> objList)
        {
            try
            {
                JsonSerializerOptions options = new JsonSerializerOptions //Mybe return bool that the proccess worked?
                {
                    WriteIndented = true
                };
                string json = JsonSerializer.Serialize(objList, options);

                File.WriteAllText(path, json);
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (NotSupportedException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}