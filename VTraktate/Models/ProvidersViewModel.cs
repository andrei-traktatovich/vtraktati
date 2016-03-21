using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using VTraktate.Core.Infrastructure;
using VTraktate.Domain;
using VTraktate.Domain.Snapshots;
using System.Data.Entity;

namespace VTraktate.Models
{
    public class ProvidersViewModel
    {
        public int Id { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsBusy { get; set; }

        public bool IsUnavailable { get; set; }

        public bool IsPromoted { get; set; }

        public bool IsNativeSpeaker { get; set; }
        public string Name { get; set; }
        public string TypeName { get; set; }
        public int TypeId { get; set; }
        public string Comment { get; set; }

        public ProviderEmploymentModel Employment { get; set; }

        public CalendarPeriodModel Calendar { get; set; }

        public FreelanceModel Freelance { get; set; }

        public RateModel Rate { get; set; }

        public QAModel Qa { get; set; }

        public AvailabilityModel Availability { get; set; }

        public static Expression<Func<ExtendedProviderSnapshot, ProvidersViewModel>> FromExtendedProviderSnapshot(bool showRateAndQA)
        {
            return x =>
            new ProvidersViewModel
            {
                Id = x.Provider.Id,
                Name = x.Provider.Name,
                TypeId = (int)x.Provider.ProviderTypeId,
                TypeName = x.Provider.ProviderType.Name,
                Employment = x.Employment != null ? new ProviderEmploymentModel
                {
                    OfficeId = x.Employment.OfficeID,
                    OfficeName = x.Employment.Office.Name,
                    StatusId = x.Employment.StatusID,
                    StatusName = x.Employment.Status.Name,
                    Comment = x.Employment.Comment,
                    TitleName = x.Employment.Title.Name,
                    TitleId = x.Employment.TitleId
                } : null,
                Calendar = x.EmployeeCalendarPeriod != null ? new CalendarPeriodModel
                {
                    EndDate = x.EmployeeCalendarPeriod.EndDate,
                    StartDate = x.EmployeeCalendarPeriod.StartDate,
                    OfficeId = x.EmployeeCalendarPeriod.OfficeId,
                    OfficeName = x.EmployeeCalendarPeriod.CurrentOffice.Name,
                    StatusName = x.EmployeeCalendarPeriod.Status.Name
                } : null,
                Freelance = x.Freelance != null ? new FreelanceModel
                {
                    Comment = x.Freelance.Comment,
                    StatusName = x.Freelance.FreelanceStatus.Name,
                    StatusId = x.Freelance.FreelanceStatusID
                } : null,

                Qa = new QAModel
                    {
                        Grade = showRateAndQA ? (x.Language.QA.Grade != null ? x.Language.QA.Grade : x.Service.QA.Grade) : 0,
                        Stars = showRateAndQA ? ((int)x.Language.QA.Stars != null ? (int)x.Language.QA.Stars : (int)x.Service.QA.Stars) : 0,
                        Comment = showRateAndQA ? (x.Language.QA.Comment != null && x.Language.QA.Comment != "" ? x.Language.QA.Comment : x.Service.QA.Comment) : "",
                    },
                Rate = new RateModel
                {
                    CurrencyName = showRateAndQA ? x.Service.Currency.Name : null,
                    UomName = showRateAndQA ? x.Service.ServiceUOM.Name : null,
                    MaxRate = showRateAndQA ? (x.Language.Rate.MaxRate ?? (x.Service.Rate.MaxRate.HasValue ? x.Service.Rate.MaxRate.Value : 0)) : 0,
                    MinRate = showRateAndQA ? (x.Language.Rate.Minrate ?? (x.Service.Rate.Minrate.HasValue ? x.Service.Rate.Minrate.Value : 0)) : 0

                },
                Availability = new AvailabilityModel
                {
                    StatusName = x.FreelanceCalendarPeriod.Status.Name,
                    Comment = x.FreelanceCalendarPeriod.Comment,
                    StatusThrough = x.BusyThrough ?? x.FreelanceCalendarPeriod.EndDate,
                    UltimateStatus = x.AvailabilityStatusId,
                    PendingJobsCount = x.PendingJobParts.Count(),
                    LatestDueDate = x.PendingJobParts.Max(y => y.EndDate.Value)
                },
                IsUnavailable = x.FreelanceCalendarPeriod != null && x.FreelanceCalendarPeriod.StatusId != ProviderAvailabilityStatus.Free,
                IsBusy = x.AvailabilityStatusId == ProviderAvailabilityStatus.Busy,
                IsPromoted = x.Promotion != null
            };
        }
        public class JobPartModel
        {
            public int? Id { get; set; }
            public string Name { get; set; }

            public DateTime? StartDate { get; set; }
            public DateTime? EndDate { get; set; }

            public IdTitlePair Status { get; set; }

        }
        public class ProviderEmploymentModel
        {
            public string OfficeName { get; set; }
            public int OfficeId { get; set; }
            public string TitleName { get; set; }

            public string StatusName { get; set; }


            public string Comment { get; set; }

            public int StatusId { get; set; }

            public int TitleId { get; set; }
        }

        public class CalendarPeriodModel
        {
            public string StatusName { get; set; }
            public DateTime? StartDate { get; set; }
            public DateTime? EndDate { get; set; }

            public string OfficeName { get; set; }
            public int OfficeId { get; set; }
        }

        public class RateModel
        {
            public decimal? MinRate { get; set; }
            public decimal? MaxRate { get; set; }

            public string CurrencyName { get; set; }

            public string UomName { get; set; }

        }

        public class QAModel
        {
            public int? Stars { get; set; }
            public decimal? Grade { get; set; }
            public string Comment { get; set; }
        }

        public class FreelanceModel
        {
            public string StatusName { get; set; }

            public int StatusId { get; set; }
            public string Comment { get; set; }
        }

        public class AvailabilityModel
        {
            public string StatusName { get; set; }
            public DateTime? StatusThrough { get; set; }
            public string Comment { get; set; }

            public int UltimateStatus { get; set; }
            //public  IEnumerable<JobPartModel> PendingJobParts { get; set; }} Can't do this because DISCTINCT fails 

            public int PendingJobsCount { get; set; }
            public DateTime? LatestDueDate { get; set; }
        }
    }
}