namespace SmartManagerLibrarySystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNewAdtribututeForTableBook : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Books", "Image", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Books", "Image", c => c.Int(nullable: false));
        }
    }
}
