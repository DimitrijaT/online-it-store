namespace OnlineITStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tbl_Cart",
                c => new
                    {
                        CartId = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(),
                        MemberId = c.Int(),
                        CartStatusId = c.Int(),
                    })
                .PrimaryKey(t => t.CartId)
                .ForeignKey("dbo.Tbl_Product", t => t.ProductId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Tbl_Product",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        ProductName = c.String(),
                        CategoryId = c.Int(),
                        IsActive = c.Boolean(),
                        IsDelete = c.Boolean(),
                        CreatedDate = c.DateTime(),
                        ModifiedDate = c.DateTime(),
                        ProductImage = c.String(),
                        IsFeatured = c.Boolean(),
                        Quantity = c.Int(),
                        Price = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ProductId)
                .ForeignKey("dbo.Tbl_Category", t => t.CategoryId)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.Tbl_Category",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(),
                        IsActive = c.Boolean(),
                        IsDelete = c.Boolean(),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "dbo.Tbl_CartStatus",
                c => new
                    {
                        CartStatusId = c.Int(nullable: false, identity: true),
                        CartStatus = c.String(),
                    })
                .PrimaryKey(t => t.CartStatusId);
            
            CreateTable(
                "dbo.Tbl_MemberRole",
                c => new
                    {
                        MemberRoleID = c.Int(nullable: false, identity: true),
                        memberId = c.Int(),
                        RoleId = c.Int(),
                    })
                .PrimaryKey(t => t.MemberRoleID);
            
            CreateTable(
                "dbo.Tbl_Members",
                c => new
                    {
                        MemberId = c.Int(nullable: false, identity: true),
                        FristName = c.String(),
                        LastName = c.String(),
                        EmailId = c.String(),
                        Password = c.String(),
                        IsActive = c.Boolean(),
                        IsDelete = c.Boolean(),
                        CreatedOn = c.DateTime(),
                        ModifiedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.MemberId);
            
            CreateTable(
                "dbo.Tbl_ShippingDetails",
                c => new
                    {
                        ShippingDetailId = c.Int(nullable: false, identity: true),
                        MemberId = c.Int(),
                        Adress = c.String(),
                        City = c.String(),
                        State = c.String(),
                        Country = c.String(),
                        ZipCode = c.String(),
                        OrderId = c.Int(),
                        AmountPaid = c.Decimal(precision: 18, scale: 2),
                        PaymentType = c.String(),
                    })
                .PrimaryKey(t => t.ShippingDetailId)
                .ForeignKey("dbo.Tbl_Members", t => t.MemberId)
                .Index(t => t.MemberId);
            
            CreateTable(
                "dbo.Tbl_SlideImage",
                c => new
                    {
                        SlideId = c.Int(nullable: false, identity: true),
                        SlideTitle = c.String(),
                        SlideImage = c.String(),
                    })
                .PrimaryKey(t => t.SlideId);
            
            CreateTable(
                "dbo.Tbl_Roles",
                c => new
                    {
                        RoleId = c.Int(nullable: false, identity: true),
                        RoleName = c.String(),
                    })
                .PrimaryKey(t => t.RoleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tbl_ShippingDetails", "MemberId", "dbo.Tbl_Members");
            DropForeignKey("dbo.Tbl_Product", "CategoryId", "dbo.Tbl_Category");
            DropForeignKey("dbo.Tbl_Cart", "ProductId", "dbo.Tbl_Product");
            DropIndex("dbo.Tbl_ShippingDetails", new[] { "MemberId" });
            DropIndex("dbo.Tbl_Product", new[] { "CategoryId" });
            DropIndex("dbo.Tbl_Cart", new[] { "ProductId" });
            DropTable("dbo.Tbl_Roles");
            DropTable("dbo.Tbl_SlideImage");
            DropTable("dbo.Tbl_ShippingDetails");
            DropTable("dbo.Tbl_Members");
            DropTable("dbo.Tbl_MemberRole");
            DropTable("dbo.Tbl_CartStatus");
            DropTable("dbo.Tbl_Category");
            DropTable("dbo.Tbl_Product");
            DropTable("dbo.Tbl_Cart");
        }
    }
}
