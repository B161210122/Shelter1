using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shelter.Domain.Migrations
{
    /// <inheritdoc />
    public partial class DbElementsCreation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Genus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Animals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GenusId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AcceptionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Animals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Animals_Genus_GenusId",
                        column: x => x.GenusId,
                        principalTable: "Genus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Adoptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    AnimalId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adoptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Adoptions_Animals_AnimalId",
                        column: x => x.AnimalId,
                        principalTable: "Animals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Adoptions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "PasswordHash", "PasswordSalt", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 8, 5, 22, 50, 49, 77, DateTimeKind.Local).AddTicks(1301), new byte[] { 76, 41, 75, 52, 207, 132, 160, 253, 16, 29, 176, 98, 48, 71, 207, 63, 222, 245, 71, 173, 58, 152, 109, 139, 139, 55, 229, 201, 11, 137, 80, 146, 27, 155, 21, 193, 180, 75, 37, 183, 30, 230, 98, 177, 190, 114, 125, 247, 237, 57, 171, 115, 49, 100, 226, 70, 65, 71, 88, 11, 1, 179, 77, 151 }, new byte[] { 20, 92, 177, 199, 54, 108, 99, 136, 137, 222, 205, 149, 82, 86, 126, 34, 68, 27, 222, 211, 94, 48, 255, 59, 38, 129, 68, 81, 101, 57, 90, 64, 40, 93, 202, 58, 125, 194, 106, 110, 58, 102, 180, 218, 49, 133, 235, 88, 35, 254, 61, 229, 86, 98, 178, 137, 239, 72, 120, 206, 127, 239, 54, 48, 168, 53, 103, 141, 5, 110, 119, 223, 142, 36, 70, 60, 162, 13, 60, 223, 26, 111, 91, 194, 228, 167, 215, 248, 114, 131, 203, 180, 199, 22, 12, 204, 27, 154, 161, 194, 186, 44, 90, 107, 32, 97, 202, 251, 151, 133, 65, 109, 191, 47, 173, 2, 60, 7, 63, 219, 205, 241, 206, 107, 86, 2, 97, 198 }, new DateTime(2023, 8, 5, 22, 50, 49, 77, DateTimeKind.Local).AddTicks(1313) });

            migrationBuilder.CreateIndex(
                name: "IX_Adoptions_AnimalId",
                table: "Adoptions",
                column: "AnimalId");

            migrationBuilder.CreateIndex(
                name: "IX_Adoptions_UserId",
                table: "Adoptions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Animals_GenusId",
                table: "Animals",
                column: "GenusId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Adoptions");

            migrationBuilder.DropTable(
                name: "Animals");

            migrationBuilder.DropTable(
                name: "Genus");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "PasswordHash", "PasswordSalt", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 8, 5, 22, 12, 54, 891, DateTimeKind.Local).AddTicks(2512), new byte[] { 79, 121, 254, 127, 211, 107, 52, 192, 89, 79, 101, 157, 242, 75, 201, 1, 9, 167, 74, 99, 243, 35, 151, 249, 78, 208, 144, 143, 244, 148, 148, 158, 201, 235, 141, 99, 46, 252, 67, 196, 2, 102, 22, 241, 12, 101, 170, 114, 97, 239, 161, 232, 68, 153, 156, 233, 198, 44, 89, 77, 189, 63, 153, 252 }, new byte[] { 121, 120, 81, 121, 93, 130, 206, 20, 73, 149, 131, 239, 189, 173, 165, 50, 166, 223, 116, 128, 157, 135, 44, 46, 198, 98, 219, 201, 162, 214, 34, 172, 115, 160, 2, 27, 4, 118, 39, 0, 93, 209, 95, 239, 81, 41, 91, 234, 193, 12, 44, 159, 153, 21, 127, 219, 78, 49, 104, 91, 187, 83, 100, 80, 102, 12, 181, 235, 173, 114, 86, 19, 66, 80, 206, 31, 142, 217, 11, 213, 251, 168, 125, 254, 165, 156, 251, 201, 149, 151, 156, 122, 129, 131, 28, 239, 192, 108, 141, 212, 2, 25, 243, 125, 117, 9, 92, 115, 131, 106, 12, 5, 166, 224, 131, 246, 249, 94, 193, 15, 9, 40, 75, 128, 21, 208, 93, 88 }, new DateTime(2023, 8, 5, 22, 12, 54, 891, DateTimeKind.Local).AddTicks(2522) });
        }
    }
}
