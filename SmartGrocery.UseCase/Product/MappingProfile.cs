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
                .ForMember(dest => dest.ProductNumer, opt => opt.Ignore())
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.Price, opt => opt.Ignore());

            CreateMap<ProductSnapshotDto, UpdatedProductSnapshot>()
                .ForMember(dest => dest.ProductId, opt => opt.Ignore());
        }
    }
}