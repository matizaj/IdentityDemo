using Microsoft.EntityFrameworkCore.Migrations;

namespace IdentityDemo.Migrations
{
    public partial class AddCounterColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Counter",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Counter",
                table: "AspNetUsers");
        }
    }
}
