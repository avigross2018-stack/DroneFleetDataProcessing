

using System.Text.Json;

namespace DroneFleetDataProcessing.src.Storage
{
    class JsonTool 
    {
        public List<T> FromJson<T>(string path) //This return empty list in catch case
        {
            try
            {
                string json = File.ReadAllText(path);
                List<T> objList = JsonSerializer.Deserialize<List<T>>(json);

                return objList;
            }

            catch (JsonException ex)
            {
                Console.WriteLine($"{ex.Message}"); //TODO reThorw?????
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine($"{ex.Message}");
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine($"{ex.Message}");
            } 
            catch (IOException ex)
            {
                Console.WriteLine($"{ex.Message}");
            }

            catch (NotSupportedException ex)
            {
                Console.WriteLine($"{ex.Message}");
            }
            return new List<T>();
            }

        public void ToJson<T>(string path, List<T> objList)
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
                Console.WriteLine($"Could not write file: {ex.Message}");
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"JSON serialization error: {ex.Message}");
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine($"No permission to write file: {ex.Message}");
            }
            catch (NotSupportedException ex)
            {
                Console.WriteLine($"Unsupported type: {ex.Message}");
            }
        }
    }
}