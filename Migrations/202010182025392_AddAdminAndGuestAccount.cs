namespace Library_Management.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class AddAdminAndGuestAccount : DbMigration
    {
        public override void Up()
        {

            //This will add Admin and Guest account to DB
            Sql(@"
                INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'b0c9511c-1393-4e2a-922b-9663fb968b82', N'admin@library.com', 0, N'AFQ/YVcpGaFoiEDk4jbFb9hMEXHsQMwwltFua5Ym531MJKgNTF4of4NZmfIGJU2qDQ==', N'ea3a37d5-9aac-4418-b516-b2ef0438ae90', NULL, 0, 0, NULL, 1, 0, N'admin@library.com')
                INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'e79b976a-cbb6-475b-89f5-33fbf3d4069f', N'guest@library.com', 0, N'AEs+pyadGODQ9YCwJYnyWjM4w4kBPkSGvhX0Em15G6sjDS4KnDlPBHWvy/IeZw6sdQ==', N'18299800-0d7e-4b93-9bde-d718f5a39df2', NULL, 0, 0, NULL, 1, 0, N'guest@library.com')

                INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'1160c84a-6de9-4ea0-8d9a-346c32f39e7a', N'CanManageLibrary')

                INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'b0c9511c-1393-4e2a-922b-9663fb968b82', N'1160c84a-6de9-4ea0-8d9a-346c32f39e7a')
                ");

        }

        public override void Down()
        {
        }
    }
}
