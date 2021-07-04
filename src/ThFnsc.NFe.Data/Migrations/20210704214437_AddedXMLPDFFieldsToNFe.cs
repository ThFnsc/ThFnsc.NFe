using Microsoft.EntityFrameworkCore.Migrations;

namespace ThFnsc.NFe.Data.Migrations
{
    public partial class AddedXMLPDFFieldsToNFe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "ReturnedPDF",
                table: "NFes",
                type: "MEDIUMBLOB",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReturnedXMLContent",
                table: "NFes",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReturnedPDF",
                table: "NFes");

            migrationBuilder.DropColumn(
                name: "ReturnedXMLContent",
                table: "NFes");
        }
    }
}
