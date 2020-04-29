using AutoMapper;
using SmartGrocery.WebApi.Contracts.BaseProduct;

namespace SmartGrocery.WebUI.Models.Products
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<ProductContract, ProductSnapshotViewModel>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.NumberOfSoldProduct, opt => opt.Ignore())
                .ForMember(dest => dest.ProductNumber, opt => opt.MapFrom(src => src.ProductNumber));

        }
    }
}