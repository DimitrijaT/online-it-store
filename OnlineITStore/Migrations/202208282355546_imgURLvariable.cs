namespace OnlineITStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class imgURLvariable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tbl_Product", "ImageURL", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tbl_Product", "ImageURL");
        }
    }
}
