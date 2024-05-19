namespace SmartManagerLibrarySystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLoansToApplicationUser : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Loans",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        BookId = c.Int(nullable: false),
                        LoanDate = c.DateTime(nullable: false),
                        ReturnDate = c.DateTime(),
                        Returned = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Books", t => t.BookId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.BookId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Loans", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Loans", "BookId", "dbo.Books");
            DropIndex("dbo.Loans", new[] { "BookId" });
            DropIndex("dbo.Loans", new[] { "UserId" });
            DropTable("dbo.Loans");
        }
    }
}
