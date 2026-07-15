

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
                if(objList is null){throw new NullReferenceException();}
                return objList;
            }

            catch (JsonException ex)
            {
<<<<<<< HEAD
                Console.WriteLine($"Error: {ex.Message}"); //TODO reThorw?????
                
=======
                throw;
>>>>>>> 1077791296bd4e4fd909bfa5b094e689a0e7ca3d
            }
            catch (FileNotFoundException ex)
            {
                throw;
            }
            catch (UnauthorizedAccessException ex)
            {
                throw;
            } 
            catch (IOException ex)
            {
                throw;
            }

            catch (NotSupportedException ex)
            {
                throw;
            }
            catch(NullReferenceException ex)
            {
                throw;
            }
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
                throw;
            }
            catch (JsonException ex)
            {
                throw;
            }
            catch (UnauthorizedAccessException ex)
            {
                throw;
            }
            catch (NotSupportedException ex)
            {
                throw;
            }
        }
    }
}