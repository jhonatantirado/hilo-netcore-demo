using AutoMapper;
using EnterprisePatterns.Api.Customers.Application.Dto;
using EnterprisePatterns.Api.Customers;
using System.Collections.Generic;

namespace EnterprisePatterns.Api.Customers.Application.Assembler
{
    public class CustomerAssembler
    {
        private readonly IMapper _mapper;

        public CustomerAssembler(IMapper mapper)
        {
            _mapper = mapper;
        }

        public List<CustomerDto> toDtoList(List<Customer> customerList)
        {
            return _mapper.Map<List<Customer>, List<CustomerDto>>(customerList);
        }
    }
}