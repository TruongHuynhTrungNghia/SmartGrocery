using MediatR;
using System.Collections.Generic;

namespace SmartGrocery.UseCase.Product
{
    public class GetAllProductsQuery : IRequest<IEnumerable<BaseProductDto>>
    {
    }
}