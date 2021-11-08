using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

namespace ThFnsc.NFe.Data.Migrations;

public partial class Initial : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Addresses",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                Street = table.Column<string>(type: "text", nullable: true),
                StreetNumber = table.Column<string>(type: "text", nullable: true),
                City = table.Column<string>(type: "text", nullable: true),
                CityId = table.Column<int>(type: "int", nullable: false),
                Complement = table.Column<string>(type: "text", nullable: true),
                Neighborhood = table.Column<string>(type: "text", nullable: true),
                State = table.Column<string>(type: "varchar(767)", nullable: true),
                PostalCode = table.Column<string>(type: "varchar(767)", nullable: true),
                CreatedAt = table.Column<DateTimeOffset>(type: "timestamp", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Addresses", x => x.Id);
            });

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
                CreatedAt = table.Column<DateTimeOffset>(type: "timestamp", nullable: false),
                DeletedAt = table.Column<DateTimeOffset>(type: "timestamp", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_NFNotifiers", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Documents",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                DocType = table.Column<string>(type: "varchar(767)", nullable: true),
                DocIdentifier = table.Column<string>(type: "varchar(767)", nullable: true),
                Name = table.Column<string>(type: "text", nullable: true),
                Email = table.Column<string>(type: "varchar(767)", nullable: true),
                AddressId = table.Column<int>(type: "int", nullable: true),
                CreatedAt = table.Column<DateTimeOffset>(type: "timestamp", nullable: false),
                DeletedAt = table.Column<DateTimeOffset>(type: "timestamp", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Documents", x => x.Id);
                table.ForeignKey(
                    name: "FK_Documents_Addresses_AddressId",
                    column: x => x.AddressId,
                    principalTable: "Addresses",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateTable(
            name: "Providers",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                IssuerId = table.Column<int>(type: "int", nullable: true),
                Data = table.Column<string>(type: "text", nullable: true),
                TownHallType = table.Column<string>(type: "text", nullable: true),
                CreatedAt = table.Column<DateTimeOffset>(type: "timestamp", nullable: false),
                DeletedAt = table.Column<DateTimeOffset>(type: "timestamp", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Providers", x => x.Id);
                table.ForeignKey(
                    name: "FK_Providers_Documents_IssuerId",
                    column: x => x.IssuerId,
                    principalTable: "Documents",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateTable(
            name: "NFes",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                ProviderId = table.Column<int>(type: "int", nullable: true),
                Series = table.Column<int>(type: "int", nullable: false),
                VerificationCode = table.Column<string>(type: "varchar(767)", nullable: true),
                IssuedAt = table.Column<DateTimeOffset>(type: "timestamp", nullable: false),
                ReturnedContent = table.Column<string>(type: "text", nullable: true),
                ReturnedXMLContent = table.Column<string>(type: "text", nullable: true),
                ReturnedPDF = table.Column<byte[]>(type: "MEDIUMBLOB", nullable: true),
                SentContent = table.Column<string>(type: "text", nullable: true),
                DocumentToId = table.Column<int>(type: "int", nullable: true),
                Success = table.Column<bool>(type: "tinyint(1)", nullable: true),
                ErrorMessage = table.Column<string>(type: "text", nullable: true),
                ServiceId = table.Column<int>(type: "int", nullable: false),
                ServiceDescription = table.Column<string>(type: "text", nullable: true),
                Value = table.Column<float>(type: "float", nullable: false),
                AliquotPercentage = table.Column<float>(type: "float", nullable: false),
                CreatedAt = table.Column<DateTimeOffset>(type: "timestamp", nullable: false),
                DeletedAt = table.Column<DateTimeOffset>(type: "timestamp", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_NFes", x => x.Id);
                table.ForeignKey(
                    name: "FK_NFes_Documents_DocumentToId",
                    column: x => x.DocumentToId,
                    principalTable: "Documents",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_NFes_Providers_ProviderId",
                    column: x => x.ProviderId,
                    principalTable: "Providers",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateTable(
            name: "ScheduledGenerations",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                CronPattern = table.Column<string>(type: "text", nullable: true),
                ProviderId = table.Column<int>(type: "int", nullable: true),
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
                    name: "FK_ScheduledGenerations_Providers_ProviderId",
                    column: x => x.ProviderId,
                    principalTable: "Providers",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateTable(
            name: "NFNotifierScheduledGeneration",
            columns: table => new
            {
                NotifiersId = table.Column<int>(type: "int", nullable: false),
                ScheduledGenerationsId = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_NFNotifierScheduledGeneration", x => new { x.NotifiersId, x.ScheduledGenerationsId });
                table.ForeignKey(
                    name: "FK_NFNotifierScheduledGeneration_NFNotifiers_NotifiersId",
                    column: x => x.NotifiersId,
                    principalTable: "NFNotifiers",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_NFNotifierScheduledGeneration_ScheduledGenerations_Scheduled~",
                    column: x => x.ScheduledGenerationsId,
                    principalTable: "ScheduledGenerations",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_Addresses_CityId",
            table: "Addresses",
            column: "CityId");

        migrationBuilder.CreateIndex(
            name: "IX_Addresses_CreatedAt",
            table: "Addresses",
            column: "CreatedAt");

        migrationBuilder.CreateIndex(
            name: "IX_Addresses_PostalCode",
            table: "Addresses",
            column: "PostalCode");

        migrationBuilder.CreateIndex(
            name: "IX_Addresses_State",
            table: "Addresses",
            column: "State");

        migrationBuilder.CreateIndex(
            name: "IX_Documents_AddressId",
            table: "Documents",
            column: "AddressId");

        migrationBuilder.CreateIndex(
            name: "IX_Documents_CreatedAt",
            table: "Documents",
            column: "CreatedAt");

        migrationBuilder.CreateIndex(
            name: "IX_Documents_DeletedAt",
            table: "Documents",
            column: "DeletedAt");

        migrationBuilder.CreateIndex(
            name: "IX_Documents_DocIdentifier",
            table: "Documents",
            column: "DocIdentifier");

        migrationBuilder.CreateIndex(
            name: "IX_Documents_DocType",
            table: "Documents",
            column: "DocType");

        migrationBuilder.CreateIndex(
            name: "IX_Documents_Email",
            table: "Documents",
            column: "Email");

        migrationBuilder.CreateIndex(
            name: "IX_NFes_CreatedAt",
            table: "NFes",
            column: "CreatedAt");

        migrationBuilder.CreateIndex(
            name: "IX_NFes_DeletedAt",
            table: "NFes",
            column: "DeletedAt");

        migrationBuilder.CreateIndex(
            name: "IX_NFes_DocumentToId",
            table: "NFes",
            column: "DocumentToId");

        migrationBuilder.CreateIndex(
            name: "IX_NFes_IssuedAt",
            table: "NFes",
            column: "IssuedAt");

        migrationBuilder.CreateIndex(
            name: "IX_NFes_ProviderId",
            table: "NFes",
            column: "ProviderId");

        migrationBuilder.CreateIndex(
            name: "IX_NFes_Series",
            table: "NFes",
            column: "Series");

        migrationBuilder.CreateIndex(
            name: "IX_NFes_Success",
            table: "NFes",
            column: "Success");

        migrationBuilder.CreateIndex(
            name: "IX_NFes_VerificationCode",
            table: "NFes",
            column: "VerificationCode");

        migrationBuilder.CreateIndex(
            name: "IX_NFNotifiers_CreatedAt",
            table: "NFNotifiers",
            column: "CreatedAt");

        migrationBuilder.CreateIndex(
            name: "IX_NFNotifiers_DeletedAt",
            table: "NFNotifiers",
            column: "DeletedAt");

        migrationBuilder.CreateIndex(
            name: "IX_NFNotifierScheduledGeneration_ScheduledGenerationsId",
            table: "NFNotifierScheduledGeneration",
            column: "ScheduledGenerationsId");

        migrationBuilder.CreateIndex(
            name: "IX_Providers_CreatedAt",
            table: "Providers",
            column: "CreatedAt");

        migrationBuilder.CreateIndex(
            name: "IX_Providers_DeletedAt",
            table: "Providers",
            column: "DeletedAt");

        migrationBuilder.CreateIndex(
            name: "IX_Providers_IssuerId",
            table: "Providers",
            column: "IssuerId");

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
            name: "NFes");

        migrationBuilder.DropTable(
            name: "NFNotifierScheduledGeneration");

        migrationBuilder.DropTable(
            name: "NFNotifiers");

        migrationBuilder.DropTable(
            name: "ScheduledGenerations");

        migrationBuilder.DropTable(
            name: "Providers");

        migrationBuilder.DropTable(
            name: "Documents");

        migrationBuilder.DropTable(
            name: "Addresses");
    }
}
