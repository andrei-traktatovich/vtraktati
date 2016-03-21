using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTraktate.Core.Interfaces.BusinessLogic.Orders.JobParts;
using VTraktate.Core.Repository.Interfaces;
using VTraktate.Domain;
using System.Data.Entity;
using VTraktate.Core.Interfaces.BusinessLogic.Orders.Netting;

namespace VTraktate.BL.Orders
{
    public class JobPartManager : IJobPartManager
    {
        public JobPartManager(IRepo<JobPart> repo, INettingManager nettingManager)
        {
            this.Repo = repo;
            this.NettingManager = nettingManager;
        }
        IRepo<JobPart> Repo;
        INettingManager NettingManager;

        public async Task<JobPart> CreateJobPartAsync(int jobId, JobPart jobPart, int UserId)
        {
            jobPart.JobId = jobId;
            Repo.AddOrUpdate(jobPart);
            await Repo.SaveAsUserAsync(UserId);
            var jobPartGraph = Repo.GetGraphs(x => x.Id == jobPart.Id).SingleOrDefault();
            NettingManager.EnsureDaughterJob(jobPartGraph);
            await Repo.SaveAsUserAsync(UserId);
            return jobPartGraph;
        }

        public async Task<JobPart> UpdateJobPartAsync(JobPart jobPart, int userId)
        {
            Repo.AddOrUpdate(jobPart);
            await Repo.SaveAsUserAsync(userId);
            // ACTUALLY, should return something that I can send back as viewmodel ... 
            // just to make sure it works ...
            //var result = Repo.GetGraphs(x => x.Id == jobPart.Id).SingleOrDefault();
            //if (result == null)
            //  throw new InvalidCastException();
            //return result;
            return jobPart;
        }
        public async Task ModifyJobPartStatusAsync(int jobPartId, int statusId, int asUserId)
        {
            var jobPart = Repo.GetGraphs(x => x.Id == jobPartId).SingleOrDefault();
            if (jobPart == null || jobPart.Status == null)
                throw new InvalidOperationException();

            if (jobPart.Status.RequiresCompletion)
                jobPart.CompletionDate = DateTime.Now;
            else
                jobPart.CompletionDate = null;

            jobPart.StatusId = statusId;
            await Repo.SaveAsUserAsync(asUserId);
            NettingManager.EnsureDaughterJob(jobPart);
            await Repo.SaveAsUserAsync(asUserId);
        }


        public JobPart GetById(int jobPartId)
        {
            var jobPart = Repo.Get(x => x.Id == jobPartId).SingleOrDefault();
            return jobPart;
        }


        public async Task DeleteAsync(int id, int UserId)
        {
            await Repo.DeleteAsync(id);
            await Repo.SaveAsUserAsync(UserId);
        }
    }
}
