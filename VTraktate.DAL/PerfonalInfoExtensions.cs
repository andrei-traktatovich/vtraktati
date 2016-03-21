using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTraktate.DAL
{
    public partial class PersonalInfo : ISoftDelete
    {
        public Employment CurrentEmployment { get { return (Provider != null && Provider.Employments != null && Provider.Employments.Count > 0) ? Provider.Employments.Current() : null; } }
    }

   
}
