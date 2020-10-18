namespace Library_Management.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserFKToBookAndBorrowHistory : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "UserId", c => c.String(maxLength: 128));
            AddColumn("dbo.BorrowHistories", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Books", "UserId");
            CreateIndex("dbo.BorrowHistories", "UserId");
            AddForeignKey("dbo.Books", "UserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.BorrowHistories", "UserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BorrowHistories", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Books", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.BorrowHistories", new[] { "UserId" });
            DropIndex("dbo.Books", new[] { "UserId" });
            DropColumn("dbo.BorrowHistories", "UserId");
            DropColumn("dbo.Books", "UserId");
        }
    }
}
