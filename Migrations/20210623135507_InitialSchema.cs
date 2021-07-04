using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CVForm.Migrations
{
    public partial class InitialSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Nationalities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NationalityName = table.Column<string>(type: "varchar(45)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nationalities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Skills",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SkillName = table.Column<string>(type: "varchar(45)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skills", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "varchar(45)", nullable: false),
                    LastName = table.Column<string>(type: "varchar(45)", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "Date", nullable: false),
                    Sex = table.Column<bool>(type: "bit", nullable: true),
                    Email = table.Column<string>(type: "varchar(90)", nullable: false),
                    Password = table.Column<string>(type: "varchar(45)", nullable: false),
                    Phone = table.Column<string>(type: "varchar(45)", nullable: false),
                    Image = table.Column<byte[]>(type: "Binary", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NationalityUser",
                columns: table => new
                {
                    NationalitiesId = table.Column<int>(type: "int", nullable: false),
                    UsersUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NationalityUser", x => new { x.NationalitiesId, x.UsersUserId });
                    table.ForeignKey(
                        name: "FK_NationalityUser_Nationalities_NationalitiesId",
                        column: x => x.NationalitiesId,
                        principalTable: "Nationalities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NationalityUser_Users_UsersUserId",
                        column: x => x.UsersUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SkillUser",
                columns: table => new
                {
                    SkillsId = table.Column<int>(type: "int", nullable: false),
                    UsersUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkillUser", x => new { x.SkillsId, x.UsersUserId });
                    table.ForeignKey(
                        name: "FK_SkillUser_Skills_SkillsId",
                        column: x => x.SkillsId,
                        principalTable: "Skills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SkillUser_Users_UsersUserId",
                        column: x => x.UsersUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Nationalities",
                columns: new[] { "Id", "NationalityName" },
                values: new object[,]
                {
                    { 1, "Lebanese" },
                    { 2, "Russian" },
                    { 3, "French" },
                    { 4, "American" }
                });

            migrationBuilder.InsertData(
                table: "Skills",
                columns: new[] { "Id", "SkillName" },
                values: new object[,]
                {
                    { 1, "Java" },
                    { 2, "HTML" },
                    { 3, "PHP" },
                    { 4, "C" },
                    { 5, "C++" },
                    { 6, "C#" },
                    { 7, "Dot Net Core" },
                    { 8, "Python" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_NationalityUser_UsersUserId",
                table: "NationalityUser",
                column: "UsersUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SkillUser_UsersUserId",
                table: "SkillUser",
                column: "UsersUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NationalityUser");

            migrationBuilder.DropTable(
                name: "SkillUser");

            migrationBuilder.DropTable(
                name: "Nationalities");

            migrationBuilder.DropTable(
                name: "Skills");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
