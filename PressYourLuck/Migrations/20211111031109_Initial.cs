using Microsoft.EntityFrameworkCore.Migrations;

namespace PressYourLuck.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuditTypes",
                columns: table => new
                {
                    AuditTypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditTypes", x => x.AuditTypeID);
                });

            migrationBuilder.InsertData(
                table: "AuditTypes",
                columns: new[] { "AuditTypeID", "Name" },
                values: new object[,]
                {
                    { 1, "Cash In" },
                    { 2, "Cash Out" },
                    { 3, "Win" },
                    { 4, "Lose" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditTypes");
        }
    }
}
