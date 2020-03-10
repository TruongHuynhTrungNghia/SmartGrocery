using MediatR;

namespace SmartGrocery.UseCase.Product
{
    public class DeleteProductCommand : IRequest
    {
        public string ProductNumber { get; set; }
    }
}