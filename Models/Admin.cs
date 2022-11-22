namespace Multilinks.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Admin")]
    public partial class Admin
    {
        [Key]
        public int Admin_ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Admin_Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Admin_Email { get; set; }

        [Required]
        [StringLength(50)]
        public string Admin_Password { get; set; }

        [StringLength(50)]
        public string Admin_Contact { get; set; }

        [StringLength(200)]
        public string Admin_Address { get; set; }

    }
}
