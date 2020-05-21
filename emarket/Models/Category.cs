using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace emarket.DbContext
{
    public class Category
    {
        [Required]
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [MaxLength(50)]
        [Column("name")]
        [DisplayName("Category")]
        public string Name { get; set; }
        [Column("number_of_products")]
        [Display(Description ="number_of_products", Name = "Number Of Products")]
        public int NumberOfProducts { get; set; }

        //public virtual ICollection<Product> Products { get; set; }
    }
}