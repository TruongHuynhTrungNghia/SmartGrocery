using AutoMapper;
using MediatR;
using SmartGrocery.Model.Product;
using SmartGrocery.UseCase.DAL;
using System;
using System.Linq;

namespace SmartGrocery.UseCase.Product
{
    public class GetBaseProductByIdQueryHandler : IRequestHandler<GetBaseProductByIdQuery, BaseProductDto>
    {
        private readonly SmartGroceryContext context;
        private readonly IMapper mapper;

        public GetBaseProductByIdQueryHandler(SmartGroceryContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public BaseProductDto Handle(GetBaseProductByIdQuery query)
        {
            var product = context.Set<BaseProduct>().SingleOrDefault(x => x.Id == query.ProductId);

            if (product == null)
                throw new Exception();

            var dto = mapper.Map<BaseProductDto>(product);

            return dto;
        }
    }
}