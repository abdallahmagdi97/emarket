namespace emarket.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingTheCartEntityToDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Carts",
                c => new
                    {
                        product_id = c.Int(nullable: false),
                        added_at = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.product_id)
                .ForeignKey("dbo.Products", t => t.product_id)
                .Index(t => t.product_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Carts", "product_id", "dbo.Products");
            DropIndex("dbo.Carts", new[] { "product_id" });
            DropTable("dbo.Carts");
        }
    }
}
