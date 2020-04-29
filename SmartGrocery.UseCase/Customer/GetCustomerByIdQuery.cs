using MediatR;

namespace SmartGrocery.UseCase.Customer
{
    public class GetCustomerByIdQuery : IRequest<CustomerDetailsDto>
    {
        public string CustomerId { get; set; }
    }
}