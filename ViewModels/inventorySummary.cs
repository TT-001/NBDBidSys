using NBD_TractionFive.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NBD_TractionFive.ViewModels
{
    public class inventorySummary
    {
        public int ID { get; set; }
        public int quantityOnHands { get; set; }
        public string name { get; set; }
        public decimal price { get; set; }
        public string type { get; set; }
    }
}
