using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace AddressBook.DataAccessLayer.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Settlements",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    PostalCode = table.Column<string>(nullable: true),
                    TypeOfSettlement = table.Column<byte>(nullable: false),
                    CityId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settlements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Settlements_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: false),
                    Address = table.Column<string>(nullable: false),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    SettlementId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contacts_Settlements_SettlementId",
                        column: x => x.SettlementId,
                        principalTable: "Settlements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TelephoneNumbers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Number = table.Column<string>(nullable: true),
                    ContactId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelephoneNumbers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TelephoneNumbers_Contacts_ContactId",
                        column: x => x.ContactId,
                        principalTable: "Contacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 101, "Zagreb" },
                    { 102, "Velika Gorica" }
                });

            migrationBuilder.InsertData(
                table: "Settlements",
                columns: new[] { "Id", "CityId", "Name", "PostalCode", "TypeOfSettlement" },
                values: new object[,]
                {
                    { 201, 101, "Sloboština", "10010", (byte)2 },
                    { 202, 101, "Maksimir", "10000", (byte)2 },
                    { 204, 101, "Črnomerec", "10000", (byte)2 },
                    { 203, 102, "Velika Gorica centar", "10408", (byte)2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cities_Name",
                table: "Cities",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_Address",
                table: "Contacts",
                column: "Address",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_Name",
                table: "Contacts",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_SettlementId",
                table: "Contacts",
                column: "SettlementId");

            migrationBuilder.CreateIndex(
                name: "IX_Settlements_CityId",
                table: "Settlements",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Settlements_Name",
                table: "Settlements",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TelephoneNumbers_ContactId",
                table: "TelephoneNumbers",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_TelephoneNumbers_Number",
                table: "TelephoneNumbers",
                column: "Number",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TelephoneNumbers");

            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropTable(
                name: "Settlements");

            migrationBuilder.DropTable(
                name: "Cities");
        }
    }
}
