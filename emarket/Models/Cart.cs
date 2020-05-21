using emarket.DbContext;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace emarket.Models
{
    public class Cart
    {
        [Required]
        [Key]
        [ForeignKey("Product")]
        [Column("product_id")]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        [Column("added_at")]
        public DateTime AddedAt { get; set; }
    }
}