namespace VTraktate.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using VTraktate.Domain.ComplexTypes;
    using VTraktate.Domain.Interfaces;

    public partial class Person : ISoftDelete, ITimeStamped, IEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        

        public int Id { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        
        // ATTN: theoretically, several accounts / roles per person. In practice, I am currently always using FirstOrDefault()
        public virtual ICollection<AspNetUser> AspNetUsers { get; set; }
        public virtual ICollection<Email> Emails { get; set; }

        public virtual ICollection<Phone> Phones { get; set; }
        public virtual ICollection<OtherContact> OtherContacts { get; set; }


        public IndividualName PersonName { get; set; }

        public bool IsDeleted { get; set; }

        

        public string  Comment { get; set; }
        public DateTime? BirthDate { get; set; }

        public DateTime CreatedDate { get; set; }
        

        public Person CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public Person ModifiedBy { get; set; }
        
        
        public int? ProviderId { get; set; }
        
        [ForeignKey("ProviderId")]
        public Provider Provider { get; set; }

        public ICollection<Customer> Customers { get; set; }

        [ForeignKey("CreatedBy")]
        public int? CreatedById { get;set;}

        [ForeignKey("ModifiedBy")]
        public int? ModifiedById { get;set;}

        public int? Old_Id { get; set; }

        public virtual PersonOfficialInfo PersonOfficialInfo { get; set; }

        public bool IsDefaultContactPerson { get; set; }
    }
}
