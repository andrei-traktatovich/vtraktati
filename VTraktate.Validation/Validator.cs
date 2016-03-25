using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using VTraktate.DataAccess;

namespace VTraktate.Validation
{
    public class Validator
    {
        private TraktatContext _context;
        public Validator(TraktatContext context)
        {
            _context = context;
        }

        public bool IsLinguisticServiceType(int id)
        {
            var serviceType = _context.ServiceTypes.Find(id);
            
            if (serviceType == null)
                throw new ArgumentException("несуществующий id вида услуг");
            
            return serviceType.SpecifyLanguage;
        }
        
    }
}
