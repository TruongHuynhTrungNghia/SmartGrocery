using MediatR;

namespace SmartGrocery.UseCase.Customer
{
    public class SearchCustomerQuery : IRequest<CustomerDetailsDto[]>
    {
        public string SearchTerm { get; set; }
    }
}