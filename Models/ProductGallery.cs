namespace Multilinks.Models
{
    using System;
    using System.Web;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ProductGallery")]
    public partial class ProductGallery
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        [Key]
        public int Gallery_ID { get; set; }

        [StringLength( 200)]
        public string Gallery_URL { get; set; }

        public int? Product_FID { get; set; }
 
        public virtual Product Product { get; set; }


    }
}