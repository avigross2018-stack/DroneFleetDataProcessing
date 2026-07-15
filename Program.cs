using System;
using DroneFleetDataProcessing.src.Paths;
using DroneFleetDataProcessing.src.Pipeline;
using DroneFleetDataProcessing.src.Storage;

namespace DroneFleetDataProcessing
{
    class Program
    {
        static void Main(string[] args)
        {
            JsonTool jsonTool = new();
            Pipeline pipeline = new(jsonTool);

            pipeline.Run(
                PathHolder.InputDronesMalformed(),
                PathHolder.OutputDronesClean(),
                PathHolder.outputAnalyze()
            );
        }
    }
}
