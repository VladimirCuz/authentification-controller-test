using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace authentification_controller_test.Migrations
{
    /// <inheritdoc />
    public partial class SeedInitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "password",
                table: "Users",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "login",
                table: "Users",
                newName: "Login");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Roles",
                newName: "Name");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleId", "Name", "permissions" },
                values: new object[] { 10, "Admin", "adminpermissions" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Login", "Password" },
                values: new object[] { 1, "Admin", "a3ba521e0e53ca7c60bbdf4f8ff44ad0c3b4f2f5cd993d5e7f630df5932f14a12b1525b9e6837ce70d276e2ecfaf7ba078485edfc1286f5640af2e509c5a27fd" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { 10, 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 10, 1 });

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1);

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Users",
                newName: "password");

            migrationBuilder.RenameColumn(
                name: "Login",
                table: "Users",
                newName: "login");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Roles",
                newName: "name");
        }
    }
}
