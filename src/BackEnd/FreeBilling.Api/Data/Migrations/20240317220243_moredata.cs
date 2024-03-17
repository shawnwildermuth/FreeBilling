using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FreeBilling.Migrations
{
    /// <inheritdoc />
    public partial class moredata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "Id", "AddressLine1", "AddressLine2", "AddressLine3", "City", "Country", "PostalCode", "StateProvince" },
                values: new object[] { 3, "128 Peachtree St.", "4th Floor", "Attn: Accounting", "Atlanta", null, "30303", "GA" });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "AddressId", "CompanyName", "Contact", "PhoneNumber" },
                values: new object[] { 3, 3, "Oliveoil Games", "Mitch Heading", "678-555-1212" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
