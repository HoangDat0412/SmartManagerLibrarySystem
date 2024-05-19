namespace SmartManagerLibrarySystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddImageToBooks : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "Image", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Books", "Image");
        }
    }
}
