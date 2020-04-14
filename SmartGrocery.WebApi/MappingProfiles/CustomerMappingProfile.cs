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
        }
    }
}