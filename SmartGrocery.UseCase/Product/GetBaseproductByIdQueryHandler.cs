using AutoMapper;
using MediatR;
using SmartGrocery.Model.Product;
using SmartGrocery.UseCase.DAL;
using System;
using System.Linq;

namespace SmartGrocery.UseCase.Product
{
    public class GetBaseproductByIdQueryHandler : IRequestHandler<GetBaseproductByIdQuery, BaseProductDto>
    {
        private readonly SmartGroceryContext context;
        private readonly IMapper mapper;

        public GetBaseproductByIdQueryHandler(SmartGroceryContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public BaseProductDto Handle(GetBaseproductByIdQuery query)
        {
            var product = context.Set<BaseProduct>().SingleOrDefault(x => x.Id == query.ProductId);

            if (product == null)
                throw new Exception();

            var dto = mapper.Map<BaseProductDto>(product);

            return dto;
        }
    }
}