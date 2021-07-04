using Microsoft.EntityFrameworkCore.Migrations;

namespace ThFnsc.NFe.Data.Migrations
{
    public partial class UsingIntOnPort : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Port",
                table: "SMTPs",
                type: "int",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "smallint unsigned");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<short>(
                name: "Port",
                table: "SMTPs",
                type: "smallint unsigned",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
