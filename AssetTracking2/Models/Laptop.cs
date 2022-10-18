using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetTracking2.Models
{
    internal class Laptop
    {
        public int LaptopId { get; set; }
        public int Price { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string Brand { get; set; } = null!;
        public string Model { get; set; } = null!;
        public int OfficeId { get; set; }
        public Office Office { get; set; } = null!;
    }
}
