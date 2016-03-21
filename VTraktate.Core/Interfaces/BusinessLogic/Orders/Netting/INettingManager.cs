using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTraktate.Domain;

namespace VTraktate.Core.Interfaces.BusinessLogic.Orders.Netting
{
    public interface INettingManager
    {
        void EnsureDaughterJob(JobPart entity);

        void UpdateParentParticipant(Job job);
    }
}
