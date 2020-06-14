using AutoMapper;
using SmartGrocery.UseCase.Customer;
using SmartGrocery.WebApi.Contracts.Customer;

namespace SmartGrocery.WebApi.MappingProfiles
{
    public class CustomerMappingProfile : Profile
    {
        public CustomerMappingProfile()
        {
            CreateMap<CustomerDetailsDto, CustomerDetailsContract>()
                .ForMember(dest => dest.TransactionContracts, opt => opt.MapFrom(src => src.TransactionDtos));

            CreateMap<EditCustomerRequest, EditCustomerCommand>()
                .ForMember(dest => dest.LastestCustomerEmotion, opt => opt.Ignore())
                .ForMember(dest => dest.EmotionProbability, opt => opt.Ignore());

            CreateMap<CreateCustomerRequest, CreateCustomerCommand>()
                .ForMember(dest => dest.LastestCustomerEmotion, opt => opt.Ignore())
                .ForMember(dest => dest.EmotionProbability, opt => opt.Ignore());
        }
    }
}