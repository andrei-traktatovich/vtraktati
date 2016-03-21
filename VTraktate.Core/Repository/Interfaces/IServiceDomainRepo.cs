using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTraktate.Domain;

namespace VTraktate.Core.Repository.Interfaces
{
    public interface IServiceDomainRepo : IRepo<ServiceDomainInfo>
    {
        void BindGrades(ServiceDomainInfo domain);
    }
}
