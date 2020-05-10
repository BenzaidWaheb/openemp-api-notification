using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OpenempApiNotification.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Notification",
                columns: table => new
                {
                    NotificationId = table.Column<Guid>(nullable: false),
                    NotificationMessage = table.Column<string>(nullable: false),
                    Reciever = table.Column<string>(nullable: false),
                    Sender = table.Column<string>(nullable: false),
                    IsRead = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedBy = table.Column<string>(nullable: true),
                    NotificationCount = table.Column<int>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notification", x => x.NotificationId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notification");
        }
    }
}
