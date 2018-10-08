using Microsoft.AspNetCore.Mvc;
using EnterprisePatterns.Api.Common.Application;
using EnterprisePatterns.Api.Customers.Domain.Repository;
using EnterprisePatterns.Api.Customers;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using EnterprisePatterns.Api.Common.Application.Dto;
using EnterprisePatterns.Api.Customers.Application.Dto;
using EnterprisePatterns.Api.Customers.Application.Assembler;
using Common.Application;

namespace EnterprisePatterns.Api.Controllers
{
    [Route("v1/customers")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICustomerRepository _customerRepository;
        private readonly CustomerAssembler _customerAssembler;

        public CustomersController(IUnitOfWork unitOfWork,
            ICustomerRepository customerRepository,
            CustomerAssembler customerAssembler)
        {
            _unitOfWork = unitOfWork;
            _customerRepository = customerRepository;
            _customerAssembler = customerAssembler;
        }

        [HttpGet]
        public IActionResult Customers([FromQuery] int page = 0, [FromQuery] int size = 5)
        {
            bool uowStatus = false;
            try
            {
                uowStatus = _unitOfWork.BeginTransaction();
                List<Customer> customers = _customerRepository.GetList(page, size);
                _unitOfWork.Commit(uowStatus);
                List<CustomerDto> customersDto = _customerAssembler.toDtoList(customers);
                return StatusCode(StatusCodes.Status200OK, customersDto);
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback(uowStatus);
                Console.WriteLine(ex.StackTrace);
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiStringResponseDto("Internal Server Error"));
            }

        }

        [HttpPost]
        public IActionResult Create([FromBody] CustomerDto customerDto)
        {
            Notification notification = new Notification();
            bool uowStatus = false;
            try
            {
                uowStatus = _unitOfWork.BeginTransaction();

                Customer customer = new Customer
                {
                    OrganizationName = customerDto.OrganizationName
                };
                notification = customer.validateForSave();

                if (notification.hasErrors())
                {
                    return StatusCode(StatusCodes.Status400BadRequest, notification.ToString());
                }

                _customerRepository.Create(customer);

                _unitOfWork.Commit(uowStatus);
                return StatusCode(StatusCodes.Status201Created, new ApiStringResponseDto("Customer created!"));
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback(uowStatus);
                Console.WriteLine(ex.StackTrace);
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiStringResponseDto("Internal Server Error"));

            }
        }

        [Route("hilo")]
        [HttpPost]
        public IActionResult CreateHiLo([FromBody] CustomerDto customerDto, [FromQuery] int n = 1)
        {
            var batch = (n == 0 ? Constants.batchSize:n);
            Notification notification = new Notification();
            bool uowStatus = false;
            try
            {
                uowStatus = _unitOfWork.BeginTransaction();

                for (var i = 1; i <= batch; i++)
                {
                    Customer customer = new Customer
                    {
                        OrganizationName = customerDto.OrganizationName + i
                    };
                    notification = customer.validateForSave();

                    if (notification.hasErrors())
                    {
                        return StatusCode(StatusCodes.Status400BadRequest, notification.ToString());
                    }

                    _customerRepository.Create(customer);
                }

                _unitOfWork.Commit(uowStatus);
                return StatusCode(StatusCodes.Status201Created, new ApiStringResponseDto("Customer created!"));
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback(uowStatus);
                Console.WriteLine(ex.StackTrace);
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiStringResponseDto("Internal Server Error"));

            }
        }

    }
}