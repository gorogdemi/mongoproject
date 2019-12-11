using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoProject.WebApp.Data.Models
{
    public class Bolt : Component
    {
        public double Size { get; set; }
        public double Length { get; set; }
        public double Head { get; set; }
    }
}
