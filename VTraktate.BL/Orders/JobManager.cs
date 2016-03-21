using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VTraktate.Core.Interfaces.BusinessLogic.Orders;
using VTraktate.Core.Interfaces.BusinessLogic.Orders.Netting;
using VTraktate.Core.Repository.Interfaces;
using VTraktate.Domain;

namespace VTraktate.BL.Orders
{
    public class JobManager : IJobManager
    {
        public JobManager(IJobRepo repo, INettingManager nettingManager)
        {
            this.Repo = repo;
            this.NettingManager = nettingManager;
        }
        private IJobRepo Repo;
        private INettingManager NettingManager { get; set; }

        public IQueryable<Job> GetGraphs(Expression<Func<Job, bool>> predicate = null)
        {
            return Repo.GetGraphs(predicate);
        }


        public Task<Job> FindAsync(int id)
        {
            return Repo.FindByIdAsync(id);
        }

        public async Task<Job> UpdateAndSave(Job job, int userId)
        {
            Repo.AddOrUpdate(job);
            await SaveAsync(userId);
            var updated = Repo.GetGraphs(x => x.Id == job.Id).SingleOrDefault();
            NettingManager.UpdateParentParticipant(updated);
            await SaveAsync(userId);
            return updated;
        }

        public Task SaveAsync(int UserId)
        {
            return Repo.SaveAsUserAsync(UserId);
        }



        public async Task DeleteAsync(int id, int userId)
        {
            // TODO: make sure that deletion of each participant should also remove its daughter!!! 
            await Repo.DeleteAsync(id);
            await Repo.SaveAsUserAsync(userId);
        }
    }
}
