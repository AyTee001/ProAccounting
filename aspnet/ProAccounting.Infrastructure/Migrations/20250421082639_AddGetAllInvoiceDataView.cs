using Microsoft.EntityFrameworkCore.Migrations;
using System.Reflection;

#nullable disable

namespace ProAccounting.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddGetAllInvoiceDataView : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var assembly = Assembly.GetExecutingAssembly();
            using var stream = assembly.GetManifestResourceStream("ProAccounting.Infrastructure.SqlScripts.Views.vw_GetAllInvoiceData.sql");
            using var reader = new StreamReader(stream!);
            var sql = reader.ReadToEnd();
            migrationBuilder.Sql(sql);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
