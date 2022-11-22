namespace Multilinks.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Contact")]
    public partial class Contact
    {
        [Key]
        public int Contact_ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Contact_Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Contact_Email { get; set; }

        [Required]
        [StringLength(1000)]
        public string Contact_Message { get; set; }
        [Required]
        [StringLength(50)]
        public string Contact_Subject { get; set; }

    }
}
