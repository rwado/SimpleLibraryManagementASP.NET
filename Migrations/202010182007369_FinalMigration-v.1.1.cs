namespace Library_Management.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FinalMigrationv11 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BorrowHistories", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.BorrowHistories", new[] { "UserId" });
            AlterColumn("dbo.BorrowHistories", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.BorrowHistories", "UserId");
            AddForeignKey("dbo.BorrowHistories", "UserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BorrowHistories", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.BorrowHistories", new[] { "UserId" });
            AlterColumn("dbo.BorrowHistories", "UserId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.BorrowHistories", "UserId");
            AddForeignKey("dbo.BorrowHistories", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
