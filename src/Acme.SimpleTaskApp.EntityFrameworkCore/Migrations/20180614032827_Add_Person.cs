using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Acme.SimpleTaskApp.Migrations
{
    public partial class Add_Person : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
             name: "AppPersons",
             columns: table => new
             {
                 Id = table.Column<Guid>(nullable: false),
                 CreationTime = table.Column<DateTime>(nullable: false),
                 CreatorUserId = table.Column<long>(nullable: true),
                 LastModificationTime = table.Column<DateTime>(nullable: true),
                 LastModifierUserId = table.Column<long>(nullable: true),
                 Name = table.Column<string>(maxLength: 32, nullable: false)
             },
             constraints: table =>
             {
                 table.PrimaryKey("PK_AppPersons", x => x.Id);
             });

            migrationBuilder.AddColumn<Guid>(
                name: "AssignedPersonId",
                table: "AppTasks",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppTasks_AssignedPersonId",
                table: "AppTasks",
                column: "AssignedPersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppTasks_AppPersons_AssignedPersonId",
                table: "AppTasks",
                column: "AssignedPersonId",
                principalTable: "AppPersons",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppTasks");

            migrationBuilder.DropTable(
                name: "AppPersons");
        }
    }
}
