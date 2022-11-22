namespace Multilinks.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Customer")]
    public partial class Customer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Customer()
        {
           Orders = new HashSet<Order>();
        }

        [Key]
        public int Customer_ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Customer_Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Customer_Email { get; set; }

        [Required]
        [StringLength(50)]
        public string Customer_Password { get; set; }

        [StringLength(50)]
        public string Customer_Contact { get; set; }

        [StringLength(200)]
        public string Customer_Address { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }
    }
}