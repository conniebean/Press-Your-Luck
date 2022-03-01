using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PressYourLuck.Migrations
{
    public partial class audits : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Audits",
                columns: table => new
                {
                    AuditID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlayerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    AuditTypeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Audits", x => x.AuditID);
                    table.ForeignKey(
                        name: "FK_Audits_AuditTypes_AuditTypeID",
                        column: x => x.AuditTypeID,
                        principalTable: "AuditTypes",
                        principalColumn: "AuditTypeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Audits_AuditTypeID",
                table: "Audits",
                column: "AuditTypeID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Audits");
        }
    }
}
