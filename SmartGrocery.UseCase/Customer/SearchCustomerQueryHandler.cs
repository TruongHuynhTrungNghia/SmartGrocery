using AutoMapper;
using MediatR;
using SmartGrocery.UseCase.DAL;
using System;
using System.Linq;
using System.Linq.Expressions;
using MasterCustomer = SmartGrocery.Model.Customer.Customer;

namespace SmartGrocery.UseCase.Customer
{
    public class SearchCustomerQueryHandler : IRequestHandler<SearchCustomerQuery, CustomerDetailsDto[]>
    {
        private readonly SmartGroceryContext context;
        private readonly IMapper mapper;

        public SearchCustomerQueryHandler(SmartGroceryContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public CustomerDetailsDto[] Handle(SearchCustomerQuery query)
        {
            var predicate = MatchBySearchTerm(query.SearchTerm);
            var customers = context.Set<MasterCustomer>()
               .AsNoTracking()
               .Where(predicate)
               .ToArray();

            return mapper.Map<CustomerDetailsDto[]>(customers);
        }

        private static Expression<Func<MasterCustomer, bool>> MatchBySearchTerm(string searchTerm)
        {
            Expression<Func<MasterCustomer, bool>> predicate;

            if (string.IsNullOrWhiteSpace(searchTerm) || searchTerm == "$$$")
            {
                predicate = customer => true;
            }
            else
            {
                predicate = customer => customer.FirstName == searchTerm
                    || customer.LastName == searchTerm
                    || customer.IdNumber == searchTerm
                    || customer.CustomerId == searchTerm;
            }

            return predicate;
        }
    }
}