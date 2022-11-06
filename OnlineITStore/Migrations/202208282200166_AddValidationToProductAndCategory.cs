namespace OnlineITStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddValidationToProductAndCategory : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tbl_Product", "CategoryId", "dbo.Tbl_Category");
            DropIndex("dbo.Tbl_Product", new[] { "CategoryId" });
            AlterColumn("dbo.Tbl_Product", "ProductName", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Tbl_Product", "CategoryId", c => c.Int(nullable: false));
            AlterColumn("dbo.Tbl_Product", "Quantity", c => c.Int(nullable: false));
            AlterColumn("dbo.Tbl_Product", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Tbl_Category", "CategoryName", c => c.String(nullable: false, maxLength: 100));
            CreateIndex("dbo.Tbl_Product", "CategoryId");
            AddForeignKey("dbo.Tbl_Product", "CategoryId", "dbo.Tbl_Category", "CategoryId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tbl_Product", "CategoryId", "dbo.Tbl_Category");
            DropIndex("dbo.Tbl_Product", new[] { "CategoryId" });
            AlterColumn("dbo.Tbl_Category", "CategoryName", c => c.String());
            AlterColumn("dbo.Tbl_Product", "Price", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Tbl_Product", "Quantity", c => c.Int());
            AlterColumn("dbo.Tbl_Product", "CategoryId", c => c.Int());
            AlterColumn("dbo.Tbl_Product", "ProductName", c => c.String());
            CreateIndex("dbo.Tbl_Product", "CategoryId");
            AddForeignKey("dbo.Tbl_Product", "CategoryId", "dbo.Tbl_Category", "CategoryId");
        }
    }
}
