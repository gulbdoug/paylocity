using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Api.Data.Migrations
{
    public partial class InitialSeed : Migration
    {
        /// <summary>
        /// Seed the initial data
        /// </summary>
        /// <param name="migrationBuilder"></param>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "FirstName", "LastName", "Salary", "DateOfBirth" },
                values: new object[] { 1, "LeBron", "James", 75420.99m, new DateTime(1984, 12, 30) }
            );

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "FirstName", "LastName", "Salary", "DateOfBirth" },
                values: new object[] { 2, "Ja", "Morant", 92365.22m, new DateTime(1999, 8, 10) }
            );

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "FirstName", "LastName", "Salary", "DateOfBirth" },
                values: new object[] { 3, "Michael", "Jordan", 143211.12m, new DateTime(1963, 2, 17) }
            );

            migrationBuilder.InsertData(
                table: "Dependents",
                columns: new[] { "Id", "FirstName", "LastName", "Relationship", "DateOfBirth", "EmployeeId" },
                values: new object[] { 1, "Spouse", "Morant", "Spouse", new DateTime(1998, 3, 3), 2 }
            );

            migrationBuilder.InsertData(
                table: "Dependents",
                columns: new[] { "Id", "FirstName", "LastName", "Relationship", "DateOfBirth", "EmployeeId" },
                values: new object[] { 2, "Child1", "Morant", "Child", new DateTime(2020, 6, 23), 2 }
            );

            migrationBuilder.InsertData(
                table: "Dependents",
                columns: new[] { "Id", "FirstName", "LastName", "Relationship", "DateOfBirth", "EmployeeId" },
                values: new object[] { 3, "Child2", "Morant", "Child", new DateTime(2021, 5, 18), 2 }
            );

            migrationBuilder.InsertData(
                table: "Dependents",
                columns: new[] { "Id", "FirstName", "LastName", "Relationship", "DateOfBirth", "EmployeeId" },
                values: new object[] { 4, "DP", "Jordan", "DomesticPartner", new DateTime(1974, 1, 2), 3 }
            );
        }

        /// <summary>
        /// Ability to rollback initial seed
        /// </summary>
        /// <param name="migrationBuilder"></param>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Dependents",
                keyColumn: "Id",
                keyValue: 1
            );

            migrationBuilder.DeleteData(
                table: "Dependents",
                keyColumn: "Id",
                keyValue: 2
            );

            migrationBuilder.DeleteData(
                table: "Dependents",
                keyColumn: "Id",
                keyValue: 3
            );

            migrationBuilder.DeleteData(
                table: "Dependents",
                keyColumn: "Id",
                keyValue: 4
            );

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1
            );

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 2
            );

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 3
            );
        }
    }
}