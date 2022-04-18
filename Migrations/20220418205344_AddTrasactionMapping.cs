using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExpenseManagementSystem1.Migrations
{
    public partial class AddTrasactionMapping : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Transcation_Id",
                table: "Transcation",
                newName: "Transaction_Id");

            migrationBuilder.CreateTable(
                name: "TransactionMappings",
                columns: table => new
                {
                    TranscationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Payer = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Payee = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionMappings", x => x.TranscationId);
                    table.ForeignKey(
                        name: "FK_TransactionMappings_User_Payee",
                        column: x => x.Payee,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_TransactionMappings_User_Payer",
                        column: x => x.Payer,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TransactionMappings_Payee",
                table: "TransactionMappings",
                column: "Payee");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionMappings_Payer",
                table: "TransactionMappings",
                column: "Payer");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransactionMappings");

            migrationBuilder.RenameColumn(
                name: "Transaction_Id",
                table: "Transcation",
                newName: "Transcation_Id");
        }
    }
}
