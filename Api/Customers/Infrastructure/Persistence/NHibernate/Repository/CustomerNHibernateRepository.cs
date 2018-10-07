using EnterprisePatterns.Api.Common.Infrastructure.Persistence.NHibernate;
using EnterprisePatterns.Api.Customers.Domain.Repository;
using System.Collections.Generic;
using System;
using System.Linq;

namespace EnterprisePatterns.Api.Customers.Infrastructure.Persistence.NHibernate.Repository
{
    class CustomerNHibernateRepository : BaseNHibernateRepository<Customer>, ICustomerRepository
    {
        public CustomerNHibernateRepository(UnitOfWorkNHibernate unitOfWork) : base(unitOfWork)
        {
        }

        public List<Customer> GetList(
            int page = 0,
            int pageSize = 5)
        {
            List<Customer> customers = new List<Customer>();
            bool uowStatus = false;
            try
            {
                uowStatus = _unitOfWork.BeginTransaction();
                customers = _unitOfWork.GetSession().Query<Customer>()
                    .Skip(page * pageSize)
                    .Take(pageSize)
                    .ToList();
                _unitOfWork.Commit(uowStatus);
            } catch(Exception ex)
            {
                _unitOfWork.Rollback(uowStatus);
                throw ex;
            }
            return customers;
        }
    }
}
