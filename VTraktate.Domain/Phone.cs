using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTraktate.Domain.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace VTraktate.Domain
{
    public class Phone : ITimeStamped, ISoftDelete, IEntity
    {
        public int Id { get; set; }
        public string PhoneNumber { get; set; }
        public string Ext { get; set; }
        public bool Active { get; set; }
        
        
        public int TypeId { get; set; }
        [ForeignKey("TypeId")]
        public PhoneType Type { get; set; }


        
        public int ContactPersonId { get; set; }

        [ForeignKey("ContactPersonId"), InverseProperty("Phones")]
        public Person ContactPerson { get; set; }

        public string Comment { get; set; }



        public DateTime CreatedDate { get; set; }
        public int? CreatedById { get; set; }
        public Person CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedById { get; set; }
        public Person ModifiedBy { get; set; }

        public bool IsDeleted { get; set; }
    }
}
