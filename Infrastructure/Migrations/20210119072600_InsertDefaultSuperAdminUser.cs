using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class InsertDefaultSuperAdminUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(new StoredProcedureInsertAdminUser().SqlCode);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}
