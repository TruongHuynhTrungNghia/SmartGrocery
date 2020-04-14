using MediatR;

namespace SmartGrocery.UseCase.Customer
{
    public class GetAllCustomerQuery : IRequest<CustomerDetailsDto[]>
    {
    }
}