using MediatR;
using System;

namespace SmartGrocery.UseCase.Product
{
    public class GetBaseproductByIdQuery : IRequest<BaseProductDto>
    {
        public Guid ProductId { get; set; }
    }
}