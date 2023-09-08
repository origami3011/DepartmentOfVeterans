using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DepartmentOfVeterans.Persistence.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Picture = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CreatedBy", "CreatedDate", "FirstName", "Gender", "LastModifiedBy", "LastModifiedDate", "LastName", "Picture", "UserEmail", "UserName", "UserPassword" },
                values: new object[] { new Guid("3540f776-84dd-4dcd-a90b-0f72203dd574"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Karl", "male", null, null, "Johnson", "https://randomuser.me/api/portraits/med/men/6.jpg", "karl.johnson@example.com", "bigpeacock170", "scuba1" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CreatedBy", "CreatedDate", "FirstName", "Gender", "LastModifiedBy", "LastModifiedDate", "LastName", "Picture", "UserEmail", "UserName", "UserPassword" },
                values: new object[] { new Guid("5d39272c-7756-4c90-bcd7-706248244211"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Margot", "female", null, null, "Roche", "https://randomuser.me/api/portraits/med/women/21.jpg", "margot.roche@example.com", "goldenrabbit215", "tiger234" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CreatedBy", "CreatedDate", "FirstName", "Gender", "LastModifiedBy", "LastModifiedDate", "LastName", "Picture", "UserEmail", "UserName", "UserPassword" },
                values: new object[] { new Guid("a9a1364a-c39a-440c-83bc-b39ae9b45e27"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Potishana", "Male", null, null, "Buryak", "https://randomuser.me/api/portraits/med/men/6.jpg", "potishana.buryak@example.com", "tinymeercat589", "betty05" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
