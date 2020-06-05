using MediatR;

namespace SmartGrocery.UseCase.Customer
{
    public class SearchCustomerByCustomerNumberQuery : IRequest<CustomerDetailsDto>
    {
        public string CustomerNumber { get; set; }
    }
}