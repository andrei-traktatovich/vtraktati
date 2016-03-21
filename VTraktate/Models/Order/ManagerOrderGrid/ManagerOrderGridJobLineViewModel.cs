using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VTraktate.Models.Order.ManagerOrderGrid
{
    public class ManagerOrderGridJobLineViewModel : ManagerOrderGridLineBase
    {
        public int OrderId { get; set; }

        // Order 
        public string OrderName { get; set; }
        public int? OrderNumber { get; set; }
        public DateTime JobPlannedDeliveryDate { get; set; }

        public string Comment { get; set; }
        public string TransliterationRequirements { get; set; }

        public IdNamePairBindingModel Customer { get; set; }

        public OrderContactPersonModel ContactPerson { get; set; }

        public IdNamePairBindingModel Office { get; set; }
        public IdNamePairBindingModel RootOwner { get; set; }

        public DateTime OrderCreatedDate { get; set; }
        public string OrderCreatedByName { get; set; }

        public DateTime? OrderModifiedDate { get; set; }
        public string OrderModifiedByName { get; set; }
        
        // Job

        public int JobId { get; set; }
        //public ICollection<JobPart> JobParts { get; set; }

        public string Document { get; set; }
        public IdNamePairBindingModel Domain1 { get; set; }
        public IdNamePairBindingModel Domain2 { get; set; }
        public bool IsDaughterJob
        {
            get { return ParentParticipantId.HasValue; }
        }
        public int? ParentParticipantId { get; set; }
    }

    
}