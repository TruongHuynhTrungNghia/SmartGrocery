using AutoMapper;
using MediatR;
using SmartGrocery.Model.Product;
using SmartGrocery.UseCase.DAL;
using System.Linq;

namespace SmartGrocery.UseCase.Product
{
    internal class GetProductByNumberQueryHandler : IRequestHandler<GetProductByNumberQuery, BaseProductDto>
    {
        private readonly SmartGroceryContext context;
        private readonly IMapper mapper;

        public GetProductByNumberQueryHandler(SmartGroceryContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public BaseProductDto Handle(GetProductByNumberQuery query)
        {
            var baseProduct = context.Set<BaseProduct>()
                .AsNoTracking()
                .SingleOrDefault(x => x.ProductNumber.Contains(query.ProductNumber));

            return mapper.Map<BaseProductDto>(baseProduct);
        }
    }
}