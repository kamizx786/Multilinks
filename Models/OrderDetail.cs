namespace Multilinks.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OrderDetail")]
    public partial class OrderDetail
    {
        [Key]
        public int OrderDetail_ID { get; set; }

        public int? Order_FID { get; set; }

        public int? Product_FID { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Sale_Price { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Purchase_Price { get; set; }

        public int? Quantity { get; set; }

        public virtual Order Order { get; set; }

        public virtual Product Product { get; set; }
    }
}
