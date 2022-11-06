namespace OnlineITStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSlideUrl : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tbl_SlideImage", "SlideUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tbl_SlideImage", "SlideUrl");
        }
    }
}
