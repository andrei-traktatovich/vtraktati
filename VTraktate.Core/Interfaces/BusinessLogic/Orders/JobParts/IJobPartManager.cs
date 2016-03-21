using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTraktate.Domain;

namespace VTraktate.Core.Interfaces.BusinessLogic.Orders.JobParts
{
    public interface IJobPartManager
    {
        Task<JobPart> CreateJobPartAsync(int jobId, Domain.JobPart jobPart, int UserId);

        Task ModifyJobPartStatusAsync(int jobPartId, int statusId, int asUserId);

        Task<JobPart> UpdateJobPartAsync(JobPart jobPart, int userId);

        JobPart GetById(int jobPartId);

        Task DeleteAsync(int id, int UserId);
    }
}
