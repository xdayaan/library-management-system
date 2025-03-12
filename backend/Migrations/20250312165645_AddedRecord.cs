using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace _.Migrations
{
    /// <inheritdoc />
    public partial class AddedRecord : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Record",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Book_Id = table.Column<int>(type: "integer", nullable: false),
                    Issue_Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Return_Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Due_Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Fine = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Record", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Record");
        }
    }
}
