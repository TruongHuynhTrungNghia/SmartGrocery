using MediatR;
using System;

namespace SmartGrocery.UseCase.Product
{
    public class DeleteProductCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}