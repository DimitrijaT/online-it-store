namespace OnlineITStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedUnnecessaryTbls : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tbl_ShippingDetails", "MemberId", "dbo.Tbl_Members");
            DropIndex("dbo.Tbl_ShippingDetails", new[] { "MemberId" });
            DropTable("dbo.Tbl_MemberRole");
            DropTable("dbo.Tbl_Members");
            DropTable("dbo.Tbl_Roles");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Tbl_Roles",
                c => new
                    {
                        RoleId = c.Int(nullable: false, identity: true),
                        RoleName = c.String(),
                    })
                .PrimaryKey(t => t.RoleId);
            
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
                "dbo.Tbl_MemberRole",
                c => new
                    {
                        MemberRoleID = c.Int(nullable: false, identity: true),
                        memberId = c.Int(),
                        RoleId = c.Int(),
                    })
                .PrimaryKey(t => t.MemberRoleID);
            
            CreateIndex("dbo.Tbl_ShippingDetails", "MemberId");
            AddForeignKey("dbo.Tbl_ShippingDetails", "MemberId", "dbo.Tbl_Members", "MemberId");
        }
    }
}
