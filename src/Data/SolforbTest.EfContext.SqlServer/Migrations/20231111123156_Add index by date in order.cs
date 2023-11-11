using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SolforbTest.EfContext.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class Addindexbydateinorder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Order_Date",
                table: "Order",
                column: "Date");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Order_Date",
                table: "Order");
        }
    }
}
