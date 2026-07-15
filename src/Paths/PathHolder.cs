using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DroneFleetDataProcessing.src.Paths
{
    /// <summary>
    /// This class holds all relevant file path for this proj. 
    /// </summary>
    public static class PathHolder
    {
        public static string BaseDirectory;
        public static string ProjRoot;
        public static string InputDir;
        public static string OutputDir;
        public static string RawDir;
        public static string TestDir;

        
        static PathHolder()
        {
            BaseDirectory = AppDomain.CurrentDomain.BaseDirectory;  //hold the base dir.
            ProjRoot = Directory.GetParent(BaseDirectory).Parent.Parent.Parent.FullName; // hold the root dir.
            InputDir = Path.Combine(ProjRoot, "input");  //hold the input dir.  
            OutputDir = Path.Combine(ProjRoot, "output");  // hold the output dir.
            RawDir = Path.Combine(InputDir, "raw");  // hold the input/raw dir.
            TestDir = Path.Combine(InputDir, "test_scenarios");  // hold the input/test_scenarios dir.
        }

        public static string InputRawDroneJson()
        {
            return Path.Combine(RawDir, "drones_raw.json");
        }

        public static string InputAllInvalid()
        {
            return Path.Combine(TestDir, "drones_all_invalid.json");
        }

        public static string InputDronesEmpty()
        {
            return Path.Combine(TestDir, "drones_empty.json");
        }

        public static string InputDronesMalformed()
        {
            return Path.Combine(TestDir, "drones_malformed.json");
        }

        public static string InputDronesNull()
        {
            return Path.Combine(TestDir, "drones_null.json");
        }

        public static string outputAnalyze()
        {
            return Path.Combine(OutputDir, "analysis_report.txt");
        }

        public static string OutputDronesClean()
        {
            return Path.Combine(OutputDir, "drones_clean.json");
        }
    }   
}