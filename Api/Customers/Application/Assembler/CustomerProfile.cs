using AutoMapper;
using EnterprisePatterns.Api.Customers.Application.Dto;
using EnterprisePatterns.Api.Customers;

namespace EnterprisePatterns.Api.Customers.Application.Assembler
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<Customer, CustomerDto>()
                .ForMember(
                    dest => dest.OrganizationName,
                    x => x.MapFrom(src => src.OrganizationName)
                );
        }
    }
}
