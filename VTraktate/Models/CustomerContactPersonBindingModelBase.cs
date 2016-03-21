using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VTraktate.Domain;
using VTraktate.Domain.ComplexTypes;

namespace VTraktate.Models
{
    public class CustomerContactPersonBindingModelBase
    {
        public IndividualName PersonName { get; set; }
        public PersonOfficialInfo PersonOfficialInfo { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Comment { get; set; }
    }

    public class CustomerContactPersonEditBindingModel : CustomerContactPersonBindingModelBase
    {
        public int Id { get; set; }
    }
}