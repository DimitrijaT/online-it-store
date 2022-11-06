namespace OnlineITStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SlideImgDescAndActive : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tbl_SlideImage", "SlideDescription", c => c.String());
            AddColumn("dbo.Tbl_SlideImage", "IsActive", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tbl_SlideImage", "IsActive");
            DropColumn("dbo.Tbl_SlideImage", "SlideDescription");
        }
    }
}
