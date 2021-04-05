using Microsoft.EntityFrameworkCore.Migrations;

namespace EMI_Calculator.Data.Migrations
{
    public partial class AddLoanIdTodb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LoanId",
                table: "TransactionDetails",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TransactionDetails_LoanId",
                table: "TransactionDetails",
                column: "LoanId");

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionDetails_LoanData_LoanId",
                table: "TransactionDetails",
                column: "LoanId",
                principalTable: "LoanData",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransactionDetails_LoanData_LoanId",
                table: "TransactionDetails");

            migrationBuilder.DropIndex(
                name: "IX_TransactionDetails_LoanId",
                table: "TransactionDetails");

            migrationBuilder.DropColumn(
                name: "LoanId",
                table: "TransactionDetails");
        }
    }
}
