using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DroneFleetDataProcessing.src.Models
{
    public class Drone
    {
        public int Id { get; set;}
        public string SerialNumber { get; set;}
        public string Model { get; set;}
        public string Category { get; set;}
        public string BaseLocation { get; set;}

    }
}