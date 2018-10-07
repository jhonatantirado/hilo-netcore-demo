using FluentNHibernate.Mapping;

namespace EnterprisePatterns.Api.Customers.Infrastructure.Persistence.NHibernate.Mapping
{
    public class CustomerMap : ClassMap<Customer>
    {
        public CustomerMap()
        {
            Id(x => x.Id).Column("customer_id");
            Map(x => x.OrganizationName).Column("organization_name");
        }
    }
}
