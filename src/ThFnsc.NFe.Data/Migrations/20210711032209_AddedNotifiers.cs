using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

namespace ThFnsc.NFe.Data.Migrations
{
    public partial class AddedNotifiers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Providers_SMTPs_SMTPId",
                table: "Providers");

            migrationBuilder.DropForeignKey(
                name: "FK_ScheduledGenerations_MailTemplates_MailTemplateId",
                table: "ScheduledGenerations");

            migrationBuilder.DropTable(
                name: "MailTemplates");

            migrationBuilder.DropTable(
                name: "SMTPs");

            migrationBuilder.DropIndex(
                name: "IX_ScheduledGenerations_MailTemplateId",
                table: "ScheduledGenerations");

            migrationBuilder.DropIndex(
                name: "IX_Providers_SMTPId",
                table: "Providers");

            migrationBuilder.DropColumn(
                name: "MailList",
                table: "ScheduledGenerations");

            migrationBuilder.DropColumn(
                name: "MailTemplateId",
                table: "ScheduledGenerations");

            migrationBuilder.DropColumn(
                name: "SMTPId",
                table: "Providers");

            migrationBuilder.CreateTable(
                name: "NFNotifiers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(type: "text", nullable: true),
                    NotifierType = table.Column<string>(type: "text", nullable: true),
                    JsonData = table.Column<string>(type: "text", nullable: true),
                    Delay = table.Column<TimeSpan>(type: "time", nullable: false),
                    ScheduledGenerationId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp", nullable: false),
                    DeletedAt = table.Column<DateTimeOffset>(type: "timestamp", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NFNotifiers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NFNotifiers_ScheduledGenerations_ScheduledGenerationId",
                        column: x => x.ScheduledGenerationId,
                        principalTable: "ScheduledGenerations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NFNotifiers_CreatedAt",
                table: "NFNotifiers",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_NFNotifiers_DeletedAt",
                table: "NFNotifiers",
                column: "DeletedAt");

            migrationBuilder.CreateIndex(
                name: "IX_NFNotifiers_ScheduledGenerationId",
                table: "NFNotifiers",
                column: "ScheduledGenerationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NFNotifiers");

            migrationBuilder.AddColumn<string>(
                name: "MailList",
                table: "ScheduledGenerations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MailTemplateId",
                table: "ScheduledGenerations",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SMTPId",
                table: "Providers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MailTemplates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Body = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp", nullable: false),
                    DeletedAt = table.Column<DateTimeOffset>(type: "timestamp", nullable: true),
                    Name = table.Column<string>(type: "varchar(767)", nullable: true),
                    Subject = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MailTemplates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SMTPs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Account = table.Column<string>(type: "text", nullable: true),
                    AccountName = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp", nullable: false),
                    DeletedAt = table.Column<DateTimeOffset>(type: "timestamp", nullable: true),
                    Host = table.Column<string>(type: "text", nullable: true),
                    Password = table.Column<string>(type: "text", nullable: true),
                    Port = table.Column<int>(type: "int", nullable: false),
                    UseEncryption = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Username = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SMTPs", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledGenerations_MailTemplateId",
                table: "ScheduledGenerations",
                column: "MailTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_Providers_SMTPId",
                table: "Providers",
                column: "SMTPId");

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

            migrationBuilder.CreateIndex(
                name: "IX_SMTPs_CreatedAt",
                table: "SMTPs",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_SMTPs_DeletedAt",
                table: "SMTPs",
                column: "DeletedAt");

            migrationBuilder.AddForeignKey(
                name: "FK_Providers_SMTPs_SMTPId",
                table: "Providers",
                column: "SMTPId",
                principalTable: "SMTPs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduledGenerations_MailTemplates_MailTemplateId",
                table: "ScheduledGenerations",
                column: "MailTemplateId",
                principalTable: "MailTemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
