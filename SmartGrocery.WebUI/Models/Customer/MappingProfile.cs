using AutoMapper;
using SmartGrocery.Infrastructure;
using SmartGrocery.WebApi.Contracts.Customer;

namespace SmartGrocery.WebUI.Models.Customer
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CustomerDetailsContract, CustomerViewModel>()
                .ForMember(dest => dest.CustomerFullName, opt => opt.ResolveUsing(src => GetFullName(src.LastName, src.FirstName)));

            CreateMap<CreateCustomerViewModel, CreateCustomerRequest>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.CustomerFirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.CustomerLastName));

            CreateMap<CustomerDetailsContract, CreateCustomerViewModel>()
                .ForMember(dest => dest.CustomerFirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.CustomerLastName, opt => opt.MapFrom(src => src.LastName));

            CreateMap<CreateCustomerViewModel, EditCustomerRequest>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.CustomerFirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.CustomerLastName));

            CreateMap<EmotionalData, CustomerEmotionViewModel>()
                .ForMember(dest => dest.Emotion, opt => opt.MapFrom(src => src.Emotion))
                .ForMember(dest => dest.Percentage, opt => opt.MapFrom(src => src.Probability));
        }

        private string GetFullName(string lastName, string firstName)
        {
            return $"{firstName} {lastName}";
        }
    }
}