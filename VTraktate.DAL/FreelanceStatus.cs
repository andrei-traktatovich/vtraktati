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
    
    public partial class FreelanceStatus
    {
        public FreelanceStatus()
        {
            this.Freelances = new HashSet<Freelance>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
    
        public virtual ICollection<Freelance> Freelances { get; set; }
    }
}
