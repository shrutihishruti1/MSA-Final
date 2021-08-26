using Microsoft.EntityFrameworkCore.Migrations;

namespace MSA_Final.Migrations
{
    public partial class AddViewers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Users_UserId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Contents_Users_UserId",
                table: "Contents");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Contents",
                newName: "ViewerId");

            migrationBuilder.RenameIndex(
                name: "IX_Contents_UserId",
                table: "Contents",
                newName: "IX_Contents_ViewerId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Comments",
                newName: "ViewerId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_UserId",
                table: "Comments",
                newName: "IX_Comments_ViewerId");

            migrationBuilder.CreateTable(
                name: "Viewers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GitHub = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageURI = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Viewers", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Viewers_ViewerId",
                table: "Comments",
                column: "ViewerId",
                principalTable: "Viewers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Contents_Viewers_ViewerId",
                table: "Contents",
                column: "ViewerId",
                principalTable: "Viewers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Viewers_ViewerId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Contents_Viewers_ViewerId",
                table: "Contents");

            migrationBuilder.DropTable(
                name: "Viewers");

            migrationBuilder.RenameColumn(
                name: "ViewerId",
                table: "Contents",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Contents_ViewerId",
                table: "Contents",
                newName: "IX_Contents_UserId");

            migrationBuilder.RenameColumn(
                name: "ViewerId",
                table: "Comments",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_ViewerId",
                table: "Comments",
                newName: "IX_Comments_UserId");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GitHub = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageURI = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Users_UserId",
                table: "Comments",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Contents_Users_UserId",
                table: "Contents",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
