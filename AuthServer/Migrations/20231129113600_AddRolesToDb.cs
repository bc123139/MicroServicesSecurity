using Microsoft.EntityFrameworkCore.Migrations;

namespace AuthServer.Migrations
{
    public partial class AddRolesToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "42c5448c-e9a9-4300-b84c-29b7e9aab7fd", "64e71738-ea63-4138-95bb-6cef9d39e8c0", "Àdmin", "ÀDMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0309d39b-fac2-4b25-b163-8a96db6ec720", "6800a8f5-d09f-47c2-ab4f-e76a8a074a6b", "Visitor", "VISITOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0309d39b-fac2-4b25-b163-8a96db6ec720");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "42c5448c-e9a9-4300-b84c-29b7e9aab7fd");
        }
    }
}
