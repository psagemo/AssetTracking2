using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetTracking2.Models
{
    internal class Asset
    {
        public int Id { get; set; }
        public int? MobilePhoneId { get; set; }        
        public int? LaptopId { get; set; }        
        public int OfficeId { get; set; }
        public virtual MobilePhone MobilePhone { get; set; }
        public virtual Laptop Laptop { get; set; }
        public Office Office { get; set; } = null!;

    }
}
