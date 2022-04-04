using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NBD_TractionFive.ViewModels
{
    public class AssignedLabourVM
    {
        public int ID { get; set; }
        public string displayText { get; set; }
        public bool assigned { get; set; }
        public double displayNumber { get; set; }
    }
}
