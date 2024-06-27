using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EgyptianeInvoicing.Core.Migrations
{
    /// <inheritdoc />
    public partial class add_token_to_company : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EInvoiceToken",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EInvoiceToken",
                table: "Companies");
        }
    }
}
