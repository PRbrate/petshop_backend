using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PetShop.Data.Migrations
{
    /// <inheritdoc />
    public partial class addAudit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Audit",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OccurrenceDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    System = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: false),
                    UserId = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: false),
                    UserName = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: false),
                    Ip = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: false),
                    OperationalSystem = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: false),
                    Browser = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: false),
                    Mobile = table.Column<bool>(type: "boolean", nullable: false),
                    Action = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", unicode: false, maxLength: 500, nullable: false),
                    Model = table.Column<string>(type: "character varying(1000)", unicode: false, maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Audit", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Audit");
        }
    }
}
