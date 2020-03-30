using AutoMapper;
using MediatR;
using SmartGrocery.Model.Product;
using SmartGrocery.UseCase.DAL;
using System.Collections.Generic;
using System.Linq;

namespace SmartGrocery.UseCase.Product
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<BaseProductDto>>
    {
        private readonly SmartGroceryContext context;
        private readonly IMapper mapper;

        public GetAllProductsQueryHandler(SmartGroceryContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public IEnumerable<BaseProductDto> Handle(GetAllProductsQuery query)
        {
            var modelProducts = context.Set<BaseProduct>().Where(x => x.Id != null).ToArray();

            var listProductDtos = MapModelProductToDto(modelProducts);

            return listProductDtos;
        }

        private IEnumerable<BaseProductDto> MapModelProductToDto(IEnumerable<BaseProduct> modelProducts)
        {
            var productDtos = new List<BaseProductDto>();

            foreach (var product in modelProducts)
            {
                var dto = mapper.Map<BaseProductDto>(product);

                productDtos.Add(dto);
            }

            return productDtos;
        }
    }
}