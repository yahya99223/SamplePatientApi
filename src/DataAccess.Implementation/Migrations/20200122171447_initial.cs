using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Implementation.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    FileNo = table.Column<int>(nullable: false),
                    CitizenId = table.Column<string>(nullable: true),
                    Birthdate = table.Column<DateTime>(nullable: true),
                    Gender = table.Column<int>(nullable: false),
                    Vip = table.Column<bool>(nullable: false),
                    Nationality = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    Street = table.Column<string>(nullable: true),
                    Address1 = table.Column<string>(nullable: true),
                    Address2 = table.Column<string>(nullable: true),
                    ContactPerson = table.Column<string>(nullable: true),
                    ContactRelation = table.Column<string>(nullable: true),
                    ContactPhone = table.Column<string>(nullable: true),
                    Image = table.Column<string>(nullable: true),
                    FirstVisitDate = table.Column<DateTime>(nullable: true),
                    RecordCreationDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Patients");
        }
    }
}
