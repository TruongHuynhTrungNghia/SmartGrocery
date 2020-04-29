using MediatR;

namespace SmartGrocery.UseCase.Product
{
    public class GetProductByNumberQuery : IRequest<BaseProductDto>
    {
        public string ProductNumber { get; set; }
    }
}