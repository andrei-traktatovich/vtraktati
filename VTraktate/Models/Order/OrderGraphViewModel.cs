using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using VTraktate.Domain.Snapshots;

namespace VTraktate.Models.Order
{
    public class OrderGridViewModel
    {
        public int Id { get; set; }
        public int RowType { get { return 0; } }
        public class OrderGridViewCustomerViewModel 
        {
            public int Id { get; set; }
            public string LongName { get; set; }
            public string ShortName { get; set; }
            public string Code { get; set; }
            public OrderContactPersonModel ContactPerson { get; set; }
        }
        public IdNamePairBindingModel PaymentStatus { get; set; }
        public OrderGridViewCustomerViewModel Customer { get; set; }

        public IdNamePairBindingModel Order { get; set; }

        public string Document { get; set; }
        public IdNamePairBindingModel Domain1 { get; set; }
        public IdNamePairBindingModel Domain2 { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public IdNamePairBindingModel Status { get; set; }
        public IdNamePairBindingModel JobType { get; set; }
        public IdNamePairBindingModel Language { get; set; }
        public JobVolumeAndPricingModel Initial { get; set; }
        public JobVolumeAndPricingModel Final { get; set; }
        public IdNamePairBindingModel Currency { get; set; }
        public IdNamePairBindingModel UOM { get; set; }
        public IEnumerable<OrderGridJobPartViewModel> JobParts { get; set; }
        public int? DelegatedToOfficeId { get;set; }    
        public string CreatedByName { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedByName { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }

    public class OrderGridJobPartViewModel
    {
        public int Id { get; set; }
        public int RowType { get { return 1; } }
        public DateTime EndDate { get; set; }
        public DateTime StartDate { get; set; }

        public int JobId { get; set; }
        public IdNamePairBindingModel Currency { get; set; }
        public IdNamePairBindingModel UOM { get; set; }
        public IdNamePairBindingModel Status { get; set; }
        public IdNamePairBindingModel JobType { get; set; }
        public IdNamePairBindingModel Language { get; set; }
        public JobVolumeAndPricingModel Initial { get; set; }
        public JobVolumeAndPricingModel Final { get; set; }
        public int? DaughterJobId { get; set; }
        public IdNamePairBindingModel Provider { get; set; }
        public string CreatedByName { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedByName { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool WorkInHouse { get; set; }
    }
}