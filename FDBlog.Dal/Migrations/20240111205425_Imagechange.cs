using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FDBlog.Dal.Migrations
{
    public partial class Imagechange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Images_ImageId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "ImageId",
                table: "AspNetUsers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 1, 11, 23, 54, 24, 89, DateTimeKind.Local).AddTicks(9267));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 1, 11, 23, 54, 24, 89, DateTimeKind.Local).AddTicks(9270));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "33aeda0a-252b-4a64-9fae-9cbb7d7e69ec");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "4ce1267c-dde3-49a7-810f-b9e50b2c0e7a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "ad50471d-f0dc-44ba-ac0f-70337c3374b5");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c63d669a-0bdc-40d8-b6e6-ba398a9fecdb", "AQAAAAEAACcQAAAAEEBb2414kr9Dlpx7XoWG0Aw2Sae3HbVZe9leqwIAFSjyb0OgmCnhmR7NwDcnBz2e9w==", "55a0f88f-5bde-4df3-a31b-c81f26b0989e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "706b15d9-68e8-41d2-942d-4f86729f00d2", "AQAAAAEAACcQAAAAEF4NYuCGSYcA1GLUy/2LIvMTsPhWnEaayBllg32xPu6S9v023Pzuvt4QrnbMH0uHxA==", "f8f397e5-afcb-4cd7-80ca-bbf64ebeacda" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 1, 11, 23, 54, 24, 89, DateTimeKind.Local).AddTicks(9465));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 1, 11, 23, 54, 24, 89, DateTimeKind.Local).AddTicks(9468));

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 1, 11, 23, 54, 24, 89, DateTimeKind.Local).AddTicks(9558));

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 1, 11, 23, 54, 24, 89, DateTimeKind.Local).AddTicks(9562));

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Images_ImageId",
                table: "AspNetUsers",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Images_ImageId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "ImageId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 1, 5, 2, 45, 22, 735, DateTimeKind.Local).AddTicks(977));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 1, 5, 2, 45, 22, 735, DateTimeKind.Local).AddTicks(981));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "e611e3d9-8689-4da5-9074-2a5f3056ff33");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "8bf45ec9-936c-450b-8f91-297f93687e8b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "420f860d-9979-4f0a-8564-ee6f84313738");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8922c841-14e5-4f1d-9d0c-228815a734cf", "AQAAAAEAACcQAAAAEM5q9hLfJq+2MyeYCh+uOxh6h4PSWtryshGKC+//mIL3SGlk0F6f+z3OSDKzly5kAw==", "9b73e962-637d-4abb-9d64-0853cc79e575" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9398543b-4b55-44ad-97c3-d7bb564fc92b", "AQAAAAEAACcQAAAAEMWYKQEeEppOBBo2rVlUAQsD6QU7mZTCbmLVHBWI53YA3PdlLTOBVTOuat5jZgsAFg==", "eaea1a18-8449-4123-9bb0-509410255e71" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 1, 5, 2, 45, 22, 735, DateTimeKind.Local).AddTicks(1209));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 1, 5, 2, 45, 22, 735, DateTimeKind.Local).AddTicks(1211));

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 1, 5, 2, 45, 22, 735, DateTimeKind.Local).AddTicks(1284));

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 1, 5, 2, 45, 22, 735, DateTimeKind.Local).AddTicks(1286));

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Images_ImageId",
                table: "AspNetUsers",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
