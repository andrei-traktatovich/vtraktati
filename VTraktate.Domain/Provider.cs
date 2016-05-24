using CodeContracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using VTraktate.Domain.Interfaces;

namespace VTraktate.Domain
{
    public class Provider : ISoftDelete, ITimeStamped, IEntity
    { 
        public int Id { get; set; }

        public string Name { get; set; }

        public ProviderTypes ProviderTypeId { get; set; }

        public ProviderType ProviderType { get; set; }

        // depending on provider type, there may be a cap on the number of contact persons (1 : 1) enforced in the business logic
        public ICollection<Person> ContactPersons { get; set; }


        public DateTime CreatedDate { get; set; }

        [ForeignKey("CreatedBy")]
        public int? CreatedById { get; set; }
        public Person CreatedBy { get; set; } 

        public DateTime? ModifiedDate { get; set; }


        [ForeignKey("ModifiedBy")]
        public int? ModifiedById { get; set; }
        public Person ModifiedBy { get; set; }

        public bool IsDeleted { get; set; }

        public int RegionId { get; set;}
        public Region Region { get; set; }

        public string City { get; set; }
        public string Address { get; set; }
        public decimal TimeDifference { get; set; }

        public Office Office { get; set; }

        public virtual ICollection<Employment> Employments { get; set; }

        public virtual ICollection<Freelance> Freelances { get; set; }

        public virtual ICollection<Service> Services { get; set; }

        public virtual ICollection<Promotion> Promotions { get; set; }

        public virtual ICollection<FreelanceCalendarPeriod> FreelanceCalendarPeriods { get; set; }

        public virtual ICollection<ProviderGroup> Groups { get; set; }

        public virtual ICollection<ProviderSoft> Soft { get; set; }
        public virtual ICollection<Grade> Grades { get; set; }
        public int? Old_Id { get; set; }

        [ForeignKey("LegalFormId")]
        public virtual LegalForm LegalForm { get; set; }

        public int LegalFormId { get; set; }

        public bool WorksNightly { get; set; }

        

        private void MakeEmployee()
        {
            ProviderTypeId = ProviderTypes.Inhouse;
        }

        public virtual bool IsEmployee => ProviderTypeId == ProviderTypes.Inhouse;
        public virtual bool IsOffice => ProviderTypeId == ProviderTypes.Office;

        public void Employ(Employment employment, bool force = false)
        {
            Requires.NotNull(employment, nameof(employment));
            Requires.NotNull(Employments, nameof(Employments));

            if (IsOffice)
                throw new InvalidOperationException(); // TODO: specify text

            if (!IsEmployee)
            {
                if (force)
                    MakeEmployee();
                else
                    throw new InvalidOperationException(); // TODO: specify text
            }
                
            Employments.Add(employment);
        }

        public void AddFreelance(Freelance freelance)
        {
            Requires.NotNull(freelance, nameof(freelance));
            Requires.NotNull(Freelances, nameof(Freelances));

            Freelances.Add(freelance);
        }
    }
}
