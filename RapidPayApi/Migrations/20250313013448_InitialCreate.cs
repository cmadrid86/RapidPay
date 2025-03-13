using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RapidPayApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cards",
                columns: table => new
                {
                    CardNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    CardHolderFirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CardHolderLastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ExpiryMonth = table.Column<byte>(type: "tinyint", nullable: false),
                    ExpiryYear = table.Column<short>(type: "smallint", nullable: false),
                    Cvv = table.Column<short>(type: "smallint", nullable: false),
                    Balance = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false),
                    Limit = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cards", x => x.CardNumber);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    TransactionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false),
                    CardNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    EstablishmentId = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    Fee = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false),
                    FeeAmount = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false),
                    IsCharge = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false),
                    TransactionDateUtc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.TransactionId);
                    table.ForeignKey(
                        name: "FK_Transactions_Cards_CardNumber",
                        column: x => x.CardNumber,
                        principalTable: "Cards",
                        principalColumn: "CardNumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_CardNumber",
                table: "Transactions",
                column: "CardNumber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Cards");
        }
    }
}
