//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VTraktate.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class CalendarPeriod
    {
        public int ID { get; set; }
        public int StatusID { get; set; }
        public System.DateTime StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public Nullable<int> OfficeID { get; set; }
        public Nullable<int> EmploymentID { get; set; }
        public int ProviderID { get; set; }
    
        public virtual Office Office { get; set; }
        public virtual Provider Provider { get; set; }
        public virtual Employment Employment { get; set; }
    }
}
