using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmpDepRoleFulstackProjectJun13.Migrations
{
    /// <inheritdoc />
    public partial class AddDesignationToEmployee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Designation",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Designation",
                table: "Employees");
        }
    }
}
