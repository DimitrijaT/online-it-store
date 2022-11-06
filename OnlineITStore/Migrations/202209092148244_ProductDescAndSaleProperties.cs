namespace OnlineITStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductDescAndSaleProperties : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tbl_Product", "IsOnSale", c => c.Boolean());
            AddColumn("dbo.Tbl_Product", "ProductDescription", c => c.String());
            AddColumn("dbo.Tbl_Product", "SaleModifier", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tbl_Product", "SaleModifier");
            DropColumn("dbo.Tbl_Product", "ProductDescription");
            DropColumn("dbo.Tbl_Product", "IsOnSale");
        }
    }
}
