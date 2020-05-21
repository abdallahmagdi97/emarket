using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace emarket.DbContext
{
    public class Product
    {
        [Required]
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [MaxLength(50)]
        [Column("name")]
        public string Name { get; set; }
        [Column("price")]
        public decimal Price { get; set; }
        [Column("image")]
        [DisplayName("Image")]
        //[FileExtensions(Extensions = "jpg,jpeg,png")]
        public byte[] Image { get; set; }
        [NotMapped]
        [Required]
        public HttpPostedFileBase File { get; set; }
        [Column("description")]
        public string Description { get; set; }
        [ForeignKey("Category")]
        [Column("category_id")]
        [DisplayName("Category")]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}