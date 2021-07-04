using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

namespace ThFnsc.NFe.Data.Migrations
{
    public partial class AddedScheduledGenerations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ScheduledGenerations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    CronPattern = table.Column<string>(type: "text", nullable: true),
                    ProviderId = table.Column<int>(type: "int", nullable: true),
                    MailList = table.Column<string>(type: "text", nullable: true),
                    MailTemplateId = table.Column<int>(type: "int", nullable: true),
                    ToDocumentId = table.Column<int>(type: "int", nullable: true),
                    Value = table.Column<float>(type: "float", nullable: false),
                    AliquotPercentage = table.Column<float>(type: "float", nullable: false),
                    ServiceId = table.Column<int>(type: "int", nullable: false),
                    ServiceDescription = table.Column<string>(type: "text", nullable: true),
                    Enabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp", nullable: false),
                    DeletedAt = table.Column<DateTimeOffset>(type: "timestamp", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduledGenerations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScheduledGenerations_Documents_ToDocumentId",
                        column: x => x.ToDocumentId,
                        principalTable: "Documents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ScheduledGenerations_MailTemplates_MailTemplateId",
                        column: x => x.MailTemplateId,
                        principalTable: "MailTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ScheduledGenerations_Providers_ProviderId",
                        column: x => x.ProviderId,
                        principalTable: "Providers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledGenerations_CreatedAt",
                table: "ScheduledGenerations",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledGenerations_DeletedAt",
                table: "ScheduledGenerations",
                column: "DeletedAt");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledGenerations_Enabled",
                table: "ScheduledGenerations",
                column: "Enabled");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledGenerations_MailTemplateId",
                table: "ScheduledGenerations",
                column: "MailTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledGenerations_ProviderId",
                table: "ScheduledGenerations",
                column: "ProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledGenerations_ToDocumentId",
                table: "ScheduledGenerations",
                column: "ToDocumentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ScheduledGenerations");
        }
    }
}
