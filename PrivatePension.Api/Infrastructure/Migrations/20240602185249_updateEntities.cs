using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "AQAAAAIAAYagAAAAEIfgip0++4f8Pnmh0AbgMThPSzSxPgrfFELwjRsBOnLqfvFImgUmJ0V5wVav/r0EfA==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "Password",
                value: "AQAAAAIAAYagAAAAEKhl42PhWbORdLH245FSK10yjrHFzI+1DIl0xnRrcm6BAzGm5VXPkFi7G8AUjeCbRg==");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "AQAAAAIAAYagAAAAECgg7ZFBrrhHqf4kKeYq9rP7+nqrXwMN3gC6jVKbaJs1yD6wUlzywMJ44HdSw//2ZQ==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "Password",
                value: "AQAAAAIAAYagAAAAEBe8G7jscdkbHer5eLkX7G5mQ96RP3kM8f7VSAuqAQXtm7W2KJq8s3YWi0CivUwJew==");
        }
    }
}
