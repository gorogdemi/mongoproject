using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoProject.WebApp.Data.Models
{
    public class BLDCMotor : Component
    {
        public double Shaftdiameter { get; set; }
        public double RPMV { get; set; }
        public double Voltagerating { get; set; }
    }
}
