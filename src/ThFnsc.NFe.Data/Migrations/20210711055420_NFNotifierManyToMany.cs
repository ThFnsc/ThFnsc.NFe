using Microsoft.EntityFrameworkCore.Migrations;

namespace ThFnsc.NFe.Data.Migrations
{
    public partial class NFNotifierManyToMany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NFNotifiers_ScheduledGenerations_ScheduledGenerationId",
                table: "NFNotifiers");

            migrationBuilder.DropIndex(
                name: "IX_NFNotifiers_ScheduledGenerationId",
                table: "NFNotifiers");

            migrationBuilder.DropColumn(
                name: "ScheduledGenerationId",
                table: "NFNotifiers");

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
                name: "IX_NFNotifierScheduledGeneration_ScheduledGenerationsId",
                table: "NFNotifierScheduledGeneration",
                column: "ScheduledGenerationsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NFNotifierScheduledGeneration");

            migrationBuilder.AddColumn<int>(
                name: "ScheduledGenerationId",
                table: "NFNotifiers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_NFNotifiers_ScheduledGenerationId",
                table: "NFNotifiers",
                column: "ScheduledGenerationId");

            migrationBuilder.AddForeignKey(
                name: "FK_NFNotifiers_ScheduledGenerations_ScheduledGenerationId",
                table: "NFNotifiers",
                column: "ScheduledGenerationId",
                principalTable: "ScheduledGenerations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
