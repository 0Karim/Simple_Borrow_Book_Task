namespace SimpleBookBorrow.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IntialEntities : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        AuthorName = c.String(),
                        BookNo = c.Int(),
                        BookType = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.BorrowedBooks",
                c => new
                    {
                        BorrowerID = c.Int(nullable: false),
                        BookID = c.Int(nullable: false),
                        BookStatus = c.Boolean(),
                        Books_ID = c.Int(),
                    })
                .PrimaryKey(t => new { t.BorrowerID, t.BookID })
                .ForeignKey("dbo.Books", t => t.Books_ID)
                .ForeignKey("dbo.Borrowers", t => t.BorrowerID, cascadeDelete: true)
                .Index(t => t.BorrowerID)
                .Index(t => t.Books_ID);
            
            CreateTable(
                "dbo.Borrowers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Firsname = c.String(),
                        Lastname = c.String(),
                        MobileNo = c.Int(),
                        Gender = c.Boolean(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BorrowedBooks", "BorrowerID", "dbo.Borrowers");
            DropForeignKey("dbo.BorrowedBooks", "Books_ID", "dbo.Books");
            DropIndex("dbo.BorrowedBooks", new[] { "Books_ID" });
            DropIndex("dbo.BorrowedBooks", new[] { "BorrowerID" });
            DropTable("dbo.Borrowers");
            DropTable("dbo.BorrowedBooks");
            DropTable("dbo.Books");
        }
    }
}
