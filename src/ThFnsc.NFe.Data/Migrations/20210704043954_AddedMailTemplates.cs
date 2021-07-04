using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;
using System;

namespace ThFnsc.NFe.Data.Migrations
{
    public partial class AddedMailTemplates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MailTemplates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(767)", nullable: true),
                    Subject = table.Column<string>(type: "text", nullable: true),
                    Body = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp", nullable: false),
                    DeletedAt = table.Column<DateTimeOffset>(type: "timestamp", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MailTemplates", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MailTemplates_CreatedAt",
                table: "MailTemplates",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_MailTemplates_DeletedAt",
                table: "MailTemplates",
                column: "DeletedAt");

            migrationBuilder.CreateIndex(
                name: "IX_MailTemplates_Name",
                table: "MailTemplates",
                column: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MailTemplates");
        }
    }
}
