namespace Multilinks.Models
{
    using System;
    using System.Web;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Company")]
    public partial class Company
    {
        [Key]
        public int Company_ID { get; set; }

        [StringLength(50)]
        public string Company_Email { get; set; }

        [StringLength(50)]
        public string Company_Contact { get; set; }

        [StringLength(200)]
        [Required]
        public string Company_Address { get; set; }

        [StringLength(200)]
        public string Company_Logo { get; set; }
        [NotMapped]
        public  HttpPostedFileBase Com_Logo { get; set; }
    }
}
