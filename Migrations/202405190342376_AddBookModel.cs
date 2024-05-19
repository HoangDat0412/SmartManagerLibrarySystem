namespace SmartManagerLibrarySystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBookModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        BookId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Author = c.String(nullable: false, maxLength: 50),
                        Genre = c.String(maxLength: 50),
                        Quantity = c.Int(nullable: false),
                        Description = c.String(maxLength: 500),
                        Publisher = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.BookId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Books");
        }
    }
}
