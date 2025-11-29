using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_students_StudentId",
                table: "Attendances");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Attendances",
                table: "Attendances");

            migrationBuilder.RenameTable(
                name: "Attendances",
                newName: "attendances");

            migrationBuilder.RenameIndex(
                name: "IX_Attendances_StudentId",
                table: "attendances",
                newName: "IX_attendances_StudentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_attendances",
                table: "attendances",
                column: "Id");

            migrationBuilder.InsertData(
                table: "students",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), "Alice" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), "Bob" },
                    { new Guid("33333333-3333-3333-3333-333333333333"), "Charlie" }
                });

            migrationBuilder.InsertData(
                table: "attendances",
                columns: new[] { "Id", "IsPresent", "OccurredAt", "StudentId" },
                values: new object[,]
                {
                    { new Guid("aaaaaaa1-1111-1111-1111-111111111111"), true, new DateTime(2025, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("11111111-1111-1111-1111-111111111111") },
                    { new Guid("aaaaaaa2-1111-1111-1111-111111111111"), false, new DateTime(2025, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("11111111-1111-1111-1111-111111111111") },
                    { new Guid("aaaaaaa3-1111-1111-1111-111111111111"), true, new DateTime(2025, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("22222222-2222-2222-2222-222222222222") },
                    { new Guid("aaaaaaa4-1111-1111-1111-111111111111"), true, new DateTime(2025, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("33333333-3333-3333-3333-333333333333") }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_attendances_students_StudentId",
                table: "attendances",
                column: "StudentId",
                principalTable: "students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_attendances_students_StudentId",
                table: "attendances");

            migrationBuilder.DropPrimaryKey(
                name: "PK_attendances",
                table: "attendances");

            migrationBuilder.DeleteData(
                table: "attendances",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaa1-1111-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "attendances",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaa2-1111-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "attendances",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaa3-1111-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "attendances",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaa4-1111-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "students",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "students",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"));

            migrationBuilder.DeleteData(
                table: "students",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"));

            migrationBuilder.RenameTable(
                name: "attendances",
                newName: "Attendances");

            migrationBuilder.RenameIndex(
                name: "IX_attendances_StudentId",
                table: "Attendances",
                newName: "IX_Attendances_StudentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Attendances",
                table: "Attendances",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_students_StudentId",
                table: "Attendances",
                column: "StudentId",
                principalTable: "students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
