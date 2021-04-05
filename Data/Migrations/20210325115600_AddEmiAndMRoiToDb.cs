using Microsoft.EntityFrameworkCore.Migrations;

namespace EMI_Calculator.Data.Migrations
{
    public partial class AddEmiAndMRoiToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "EMI",
                table: "LoanData",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "MonthlyRateOfInterest",
                table: "LoanData",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EMI",
                table: "LoanData");

            migrationBuilder.DropColumn(
                name: "MonthlyRateOfInterest",
                table: "LoanData");
        }
    }
}
