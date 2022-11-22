namespace Multilinks.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        [Key]
        public int Order_ID { get; set; }

        public DateTime? Order_Date { get; set; }

        [Required]
        [StringLength(50)]
        public string Order_Name { get; set; }

        [StringLength(50)]
        public string Order_Status { get; set; }

        [StringLength(50)]
        public string Order_Type { get; set; }

        [StringLength(50)]
        public string Order_Email { get; set; }

        [StringLength(50)]
        public string Order_Contact { get; set; }

        [StringLength(200)]
        public string Order_Address { get; set; }

        [StringLength(50)]
        public string Order_City { get; set; }

        [StringLength(50)]
        public string Order_PaymentMode{ get; set; }

        public int? Customer_FID { get; set; }

        public virtual Customer Customer { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
