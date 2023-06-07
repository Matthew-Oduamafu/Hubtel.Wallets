using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Hubtel.Wallets.Persistence.Migrations
{
    public partial class AddCreditAccountTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblAsAccountScheme",
                columns: table => new
                {
                    asIDpk = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    asTypeIDfk = table.Column<int>(nullable: false),
                    asSchemeName = table.Column<string>(maxLength: 150, nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false, defaultValueSql: "(getdate())"),
                    EditedDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblAsAccountScheme", x => x.asIDpk);
                });

            migrationBuilder.CreateTable(
                name: "tblTType",
                columns: table => new
                {
                    tIDpk = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tTypeName = table.Column<string>(maxLength: 100, nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false, defaultValueSql: "(getdate())"),
                    EditedDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblTType", x => x.tIDpk);
                });

            migrationBuilder.CreateTable(
                name: "tblUcaUserCreditAccounts",
                columns: table => new
                {
                    ucaIDpk = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ucaUserIDfk = table.Column<string>(maxLength: 50, nullable: false),
                    ucaTypeIDfk = table.Column<int>(nullable: false),
                    ucaSchemeIDfk = table.Column<int>(nullable: false),
                    ucaAccountNumber = table.Column<string>(maxLength: 50, nullable: false),
                    ucaCreationDate = table.Column<DateTime>(nullable: false, defaultValueSql: "(getdate())"),
                    ucaEditedDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblUcaUserCreditAccounts", x => x.ucaIDpk);
                });

            migrationBuilder.InsertData(
                table: "tblAsAccountScheme",
                columns: new[] { "asIDpk", "asSchemeName", "asTypeIDfk", "CreationDate", "EditedDate" },
                values: new object[,]
                {
                    { 1, "Mastercard", 2, new DateTime(2023, 5, 19, 15, 7, 56, 720, DateTimeKind.Utc).AddTicks(3825), null },
                    { 2, "Visa", 2, new DateTime(2023, 5, 19, 15, 7, 56, 720, DateTimeKind.Utc).AddTicks(4731), null },
                    { 3, "MTN", 1, new DateTime(2023, 5, 19, 15, 7, 56, 720, DateTimeKind.Utc).AddTicks(4756), null },
                    { 4, "Vodafone", 1, new DateTime(2023, 5, 19, 15, 7, 56, 720, DateTimeKind.Utc).AddTicks(4758), null },
                    { 5, "AirtelTigo", 1, new DateTime(2023, 5, 19, 15, 7, 56, 720, DateTimeKind.Utc).AddTicks(4760), null }
                });

            migrationBuilder.InsertData(
                table: "tblTType",
                columns: new[] { "tIDpk", "CreationDate", "EditedDate", "tTypeName" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 5, 19, 15, 7, 56, 717, DateTimeKind.Local).AddTicks(1584), null, "Momo" },
                    { 2, new DateTime(2023, 5, 19, 15, 7, 56, 718, DateTimeKind.Local).AddTicks(1707), null, "Card" }
                });

            migrationBuilder.InsertData(
                table: "tblUcaUserCreditAccounts",
                columns: new[] { "ucaIDpk", "ucaAccountNumber", "ucaCreationDate", "ucaEditedDate", "ucaSchemeIDfk", "ucaTypeIDfk", "ucaUserIDfk" },
                values: new object[,]
                {
                    { 1, "0552235521", new DateTime(2023, 5, 19, 15, 7, 56, 721, DateTimeKind.Utc).AddTicks(9846), null, 3, 1, "0be64bd1-d201-7821-9000-18937492a66d" },
                    { 2, "0552474843", new DateTime(2023, 5, 19, 15, 7, 56, 722, DateTimeKind.Utc).AddTicks(1816), null, 4, 1, "0be64bd1-d201-7821-9000-18937492a66d" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblAsAccountScheme");

            migrationBuilder.DropTable(
                name: "tblTType");

            migrationBuilder.DropTable(
                name: "tblUcaUserCreditAccounts");
        }
    }
}