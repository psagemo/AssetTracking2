using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetTracking2.Models
{
    internal class MobilePhone
    {
        public int MobilePhoneId { get; set; }
        public string Brand { get; set; } = null!;
        public string Model { get; set; } = null!; 
    }
}
