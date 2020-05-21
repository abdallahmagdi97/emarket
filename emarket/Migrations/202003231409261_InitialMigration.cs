namespace emarket.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(maxLength: 50),
                        number_of_products = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(maxLength: 50),
                        price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        image = c.Binary(),
                        description = c.String(),
                        category_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Categories", t => t.category_id, cascadeDelete: true)
                .Index(t => t.category_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "category_id", "dbo.Categories");
            DropIndex("dbo.Products", new[] { "category_id" });
            DropTable("dbo.Products");
            DropTable("dbo.Categories");
        }
    }
}
