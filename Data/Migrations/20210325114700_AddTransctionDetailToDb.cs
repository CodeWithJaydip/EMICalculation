using Microsoft.EntityFrameworkCore.Migrations;

namespace EMI_Calculator.Data.Migrations
{
    public partial class AddTransctionDetailToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TransactionDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Installments = table.Column<double>(nullable: false),
                    OpeningAmonut = table.Column<double>(nullable: false),
                    Principal = table.Column<double>(nullable: false),
                    Interest = table.Column<double>(nullable: false),
                    EMI = table.Column<double>(nullable: false),
                    ClosingAmount = table.Column<double>(nullable: false),
                    CumulativeInterest = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionDetails", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransactionDetails");
        }
    }
}
