using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTraktate.Domain
{
    public class ProviderAvailabilityStatus : IEntity
    {
        public const int Free = 1;
        public const int Busy = 2;
        public const int Engaged = 3;
        public const int Adulter = 4;
        public const int Booked = 5;
        public const int Absent = 6;
        public const int Inactive = 7;
        
        public int Id { get; set; }

        public bool Availability { get; set; }

        public string Name { get; set; }
    }
}
