using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VTraktate.Models.Order
{
    public class OrderCreateBindingModel 
    {
        [Required]
        public int CustomerId { get; set; }
        [Required]
        public int OfficeId { get; set; }


        [Required]
        public DateTime PlannedDeliveryDate { get; set; }

        public OrderContactPersonModel ContactPerson { get; set; }
        [Required]
        public JobCreateModel[] Jobs { get; set; }

        public string Comment { get; set; }
        public string TransliterationRequirements { get; set; }

        public int? Number { get; set; }
        // TODO: prepayment info - add here ... 
    }

    public class OrderContactPersonModel
    {
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Ext { get; set; }
        public string Email { get; set; }
        public string Comment { get; set; }
    }

}