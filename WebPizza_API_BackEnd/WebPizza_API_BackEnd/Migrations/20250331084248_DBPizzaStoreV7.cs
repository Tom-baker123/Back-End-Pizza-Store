using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebPizza_API_BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class DBPizzaStoreV7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string sql1 = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, @"");
            migrationBuilder.Sql(File.ReadAllText(sql1));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
           
        }
    }
}
