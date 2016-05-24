using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VTraktate.Models.Helpers;

namespace VTraktate.Models.Order.ManagerOrderGrid
{
    public class ManagerOrderGridLineBase
    {
        public IdNamePairBindingModel Status { get; set; }
        public int LineTypeId { get; set; }
        public IdNamePairBindingModel Language { get; set; }
        public IdNamePairBindingModel JobType { get; set; }

        public VolumeAndPricingViewModel Initial { get; set; }
        public VolumeAndPricingViewModel Final { get; set; }

        public DateTime CreatedDate { get; set; }
        public string CreatedByName { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedByName { get; set; }

        public bool IsDeleted { get; set; }
        public IEnumerable<ManagerOrderGridLineBase> Children { get; set; }
    }

    public class ManagerOrderGridJobParticipantViewModel : ManagerOrderGridLineBase
    {
        public IdNamePairBindingModel JobParticipant { get; set; }
        public DateTime ParticipantDeliveryDate { get; set; }
    }

    public class VolumeAndPricingViewModel
    {
        public decimal? VolumePages { get; set; }
        public int? VolumeChars { get; set; }
        public int? VolumeWords { get; set; }

        public decimal PricingRate { get; set; }
        public decimal PricingPrice { get; set; }
        public int PricingDiscount { get; set; }
        public decimal PricingDiscountedPrice { get; set; }
    }
}