using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DisneyApi.AccessData.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Character",
                columns: table => new
                {
                    CharacterId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Image = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    Name = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<int>(type: "int", nullable: false),
                    History = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Character", x => x.CharacterId);
                });

            migrationBuilder.CreateTable(
                name: "Genre",
                columns: table => new
                {
                    GenreId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    Image = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genre", x => x.GenreId);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "Movie",
                columns: table => new
                {
                    MovieId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Image = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    Title = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    CreationDate = table.Column<DateTime>(type: "date", nullable: false),
                    Qualification = table.Column<int>(type: "int", nullable: false),
                    GenreId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movie", x => x.MovieId);
                    table.ForeignKey(
                        name: "FK_Movie_Genre_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genre",
                        principalColumn: "GenreId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CharacterMovie",
                columns: table => new
                {
                    CharacterId = table.Column<int>(type: "int", nullable: false),
                    MovieId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterMovie", x => new { x.MovieId, x.CharacterId });
                    table.ForeignKey(
                        name: "FK_CharacterMovie_Character_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Character",
                        principalColumn: "CharacterId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CharacterMovie_Movie_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movie",
                        principalColumn: "MovieId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Character",
                columns: new[] { "CharacterId", "Age", "History", "Image", "Name", "Weight" },
                values: new object[,]
                {
                    { 1, 93, "Mickey Mouse is a cartoon character created in 1928 by Walt Disney, and is the mascot of The Walt Disney Company. An anthropomorphic mouse who typically wears red shorts, large yellow shoes, and white gloves, Mickey is one of the world's most recognizable fictional characters", "Image here!", "Mickey Mouse", 10 },
                    { 2, 78, "Donald duck is an animated character created by Walt Disney as a foil to Mickey Mouse.[15] Making his screen debut in The Wise Little Hen on June 9, 1934, Donald is characterized as a pompous, showboating duck wearing a sailor suit, cap and a bow tie", "Image here!", "Donald Duck", 20 },
                    { 3, 14, "Snow White is the titular character and heroine of Disney's first animated feature-length film Snow White and the Seven Dwarfs. She is a young Princess; the Fairest of Them All who, in her innocence, cannot see any of the evil in the world", "Image here!", "Snow White", 50 },
                    { 4, 16, "Mulan is the protagonist of Disney's 1998 animated feature film of the same name and its 2004 direct-to-video sequel. Mulan is the eighth official Disney Princess and first heroine in the line-up who is not actually royalty through either birth or marriage", "Image here!", "Mulan", 55 },
                    { 5, 89, "Goofy is an animated character that first appeared in 1932's Mickey's Revue.He  is predominately known for his slapstick style of comedy, and regularly appears alongside his best friends Mickey Mouse and Donald Duck", "Image here!", "Goofy", 70 }
                });

            migrationBuilder.InsertData(
                table: "Genre",
                columns: new[] { "GenreId", "Image", "Name" },
                values: new object[,]
                {
                    { 1, "Image here!", "Fantasy" },
                    { 2, "Image here!", "animation" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleId", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Admin Role", "Admin" },
                    { 2, "User Role", "User" }
                });

            migrationBuilder.InsertData(
                table: "Movie",
                columns: new[] { "MovieId", "CreationDate", "GenreId", "Image", "Qualification", "Title" },
                values: new object[,]
                {
                    { 1, new DateTime(2004, 8, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Image here!", 1, "Mickey, Donald, Goofy: The Three Musketeers" },
                    { 2, new DateTime(1998, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Image here!", 2, "Mulan" },
                    { 3, new DateTime(1929, 5, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Image here!", 3, "The Karnival Kid" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Email", "Password", "RoleId", "Username" },
                values: new object[,]
                {
                    { 1, "TestAdmin@disney.com", "12345678", 1, "TestAdmin" },
                    { 2, "TestUser@disney.com", "12345678", 2, "TestUser" }
                });

            migrationBuilder.InsertData(
                table: "CharacterMovie",
                columns: new[] { "CharacterId", "MovieId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 },
                    { 5, 1 },
                    { 1, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CharacterMovie_CharacterId",
                table: "CharacterMovie",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_Movie_GenreId",
                table: "Movie",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CharacterMovie");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Character");

            migrationBuilder.DropTable(
                name: "Movie");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Genre");
        }
    }
}
