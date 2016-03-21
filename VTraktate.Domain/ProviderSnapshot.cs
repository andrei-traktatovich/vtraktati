using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VTraktate.Domain
{
    public class ProviderSnapshot 
    {
        public Provider Provider { get; set; }

        public Employment CurrentEmployment { get; set; }

        public CalendarPeriod CurrentCalendarPeriod { get; set; }

        public Freelance CurrentFreelance { get; set; }
    }

    public class ServiceProviderSnapshot : ProviderSnapshot
    {
        public class ProviderBusyStatus
        { }
        public bool IsPromoted { get; set; }
        public string[] Groups { get; set; }
        public ProviderBusyStatus BusyStatus { get; set; }
    }

    public class ServiceProviderRateSnapshot : ServiceProviderSnapshot
    {
        public ServiceRate Rate { get; set; }

        public Currency Currency { get; set; }

        public QA QA { get; set; }
    }
}
