using emarket.DbContext;
using emarket.Models;
using System.Data.Entity;

namespace emarket.Context
{
    public class ProductContext : System.Data.Entity.DbContext
    {
        public DbSet<Product> Product { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Cart> Cart { get; set; }
    }
}