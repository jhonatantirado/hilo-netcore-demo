using FluentMigrator;

namespace EnterprisePatterns.Api.Migrations.MySQL
{
    [Migration(2)]
    public class IdsTable : Migration
    {
        public override void Up()
        {
            Execute.EmbeddedScript("2_IdsTable.sql");
        }

        public override void Down()
        {
        }
    }
}
