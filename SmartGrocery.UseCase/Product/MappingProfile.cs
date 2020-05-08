using AutoMapper;
using SmartGrocery.Model.Product;

namespace SmartGrocery.UseCase.Product
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BaseProductDto, BaseProduct>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.ProductSnapshot, opt => opt.Ignore());

            CreateMap<CreateBaseProductCommand, BaseProduct>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.ProductSnapshot, opt => opt.Ignore());

            CreateMap<ProductSnapshot, UpdatedProductSnapshot>()
                .ForMember(dest => dest.ProductNumber, opt => opt.Ignore())
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.Price, opt => opt.Ignore())
                .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.NumberOfSoldProduct));

            CreateMap<ProductSnapshotDto, UpdatedProductSnapshot>()
                .ForMember(dest => dest.ProductId, opt => opt.Ignore())
                .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.NumberOfSoldProduct));

            CreateMap<ProductSnapshot, ProductSnapshotDto>()
                .ForMember(dest => dest.Price, opt => opt.ResolveUsing(x => GetPriceFromBaseProduct(x.Product, x.NumberOfSoldProduct)))
                .ForMember(dest => dest.ProductNumber, opt => opt.MapFrom(src => src.Product.ProductNumber))
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name));
        }

        private decimal GetPriceFromBaseProduct(BaseProduct product, int numberOfProduct)
        {
            return product.CalculateTotalPrice(numberOfProduct);
        }
    }
}