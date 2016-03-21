using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTraktate.Core.Interfaces.BusinessLogic.Orders.Netting;
using VTraktate.Domain;


namespace VTraktate.BL.Orders
{
    public class NettingManager : INettingManager
    {
        public void EnsureDaughterJob(JobPart entity)
        {
            if (entity.IsProviderOffice())
            {
                if (entity.HasDaughterJob())
                {
                    MapToDaughterJob(entity);
                }
                else
                {
                    CreateDaughterJob(entity);
                }
            }
        }

        public void MapToDaughterJob(JobPart entity)
        {
            var job = entity.DaughterJob;
            var officeId = entity.Provider.Office.Id;
            job.Initial = entity.Initial;
            job.Final = entity.Final;
            job.LanguageId = entity.LanguageId;
            job.JobTypeId = entity.JobTypeId;
            job.Document = entity.Job.Document;
            job.OrderId = entity.Job.OrderId;
            job.StatusId = MapJobParticipantStatusToJobStatus(entity.StatusId);
            job.StartDate = entity.StartDate;
            job.CompletionDate = entity.CompletionDate;
            job.EndDate = entity.EndDate;
            job.ParentJobPart = entity;
            job.CurrencyId = entity.CurrencyId;
            job.UOMId = entity.UOMId;
            job.DelegatedToOfficeId = officeId;
            job.CreatedBy = entity.CreatedBy;
            job.CreatedDate = entity.CreatedDate;
            job.ModifiedBy = entity.ModifiedBy;
            job.ModifiedDate = entity.ModifiedDate;
        }

        private int MapJobParticipantStatusToJobStatus(int statusId)
        {
            switch (statusId)
            {
                case 1: return 1;
                case 2: return 2;
                case 3: return 5;
                case 4: return 3;
            }
            throw new InvalidOperationException();
        }

        public void CreateDaughterJob(JobPart entity)
        {
            entity.DaughterJob = new Job();
            MapToDaughterJob(entity);
        }


        public void UpdateParentParticipant(Job job)
        {
            if (job.ParentJobPart != null)
            {
                MapToParent(job);
            }
        }

        public void MapToParent(Job entity)
        {
            var parent = entity.ParentJobPart;
            parent.Initial = entity.Initial;
            parent.Final = entity.Final;

            parent.LanguageId = entity.LanguageId;
            parent.JobTypeId = entity.JobTypeId;
            
            
            parent.StatusId = MapJobStatusToParticipantStatus(entity.StatusId); // TODO : MAGIC NUMBER!!! 
            parent.StartDate = entity.StartDate;
            parent.CompletionDate = entity.CompletionDate;
            parent.EndDate = entity.EndDate;

            parent.CurrencyId = entity.CurrencyId;
            parent.UOMId = entity.UOMId;

            parent.CreatedBy = entity.CreatedBy;
            parent.CreatedDate = entity.CreatedDate;
            parent.ModifiedBy = entity.ModifiedBy;
            parent.ModifiedDate = entity.ModifiedDate;
        }

        public int MapJobStatusToParticipantStatus(int statusId)
        {
            switch (statusId)
            {
                case 1: return 1;
                case 2: return 2;
                case 3: return 4;
                case 4: return 3;
                case 5: return 3;
            }
            throw new InvalidOperationException();
        }
    }

    public static class JobPartExtensions
    {
        public static bool IsProviderOffice(this JobPart @this)
        {
            return @this.Provider.Office != null;
        }

        public static bool HasDaughterJob(this JobPart @this)
        {
            return @this.DaughterJob != null;
        }
    }
}
