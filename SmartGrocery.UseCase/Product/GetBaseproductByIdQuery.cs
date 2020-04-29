using MediatR;
using System;

namespace SmartGrocery.UseCase.Product
{
    public class GetBaseProductByIdQuery : IRequest<BaseProductDto>
    {
        public Guid ProductId { get; set; }
    }
}