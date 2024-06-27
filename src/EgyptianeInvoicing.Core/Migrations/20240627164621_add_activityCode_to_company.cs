using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EgyptianeInvoicing.Core.Migrations
{
    /// <inheritdoc />
    public partial class add_activityCode_to_company : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ActivityCode",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActivityCode",
                table: "Companies");
        }
    }
}
