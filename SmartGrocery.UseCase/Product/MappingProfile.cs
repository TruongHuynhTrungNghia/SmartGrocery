using AutoMapper;
using SmartGrocery.Model.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartGrocery.UseCase.Product
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BaseProductDto, BaseProduct>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<CreateBaseProductCommand, BaseProduct>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}
