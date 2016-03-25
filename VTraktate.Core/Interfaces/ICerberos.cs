using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTraktate.Core.Interfaces
{
    public interface ICerberos
    {
        Task<bool> CanDeleteProvidersAsync();
        int UserId { get; }
    }
}
