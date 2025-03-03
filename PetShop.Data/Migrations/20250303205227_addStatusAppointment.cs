using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetShop.Data.Migrations
{
    /// <inheritdoc />
    public partial class addStatusAppointment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "TotalPrice",
                table: "Appointments",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real",
                oldPrecision: 8,
                oldScale: 2);

            migrationBuilder.AddColumn<string>(
                name: "StatusAppointment",
                table: "Appointments",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StatusAppointment",
                table: "Appointments");

            migrationBuilder.AlterColumn<float>(
                name: "TotalPrice",
                table: "Appointments",
                type: "real",
                precision: 8,
                scale: 2,
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");
        }
    }
}
