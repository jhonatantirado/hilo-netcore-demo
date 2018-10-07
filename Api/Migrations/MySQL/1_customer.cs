using FluentMigrator;

namespace EnterprisePatterns.Api.Migrations.MySQL
{
    [Migration(1)]
    public class CustomerTable : Migration
    {
        public override void Up()
        {
            Execute.EmbeddedScript("1_customer.sql");
        }

        public override void Down()
        {
        }
    }
}
