using AutoMapper;
using CustomerBase = SmartGrocery.Model.Customer.Customer;

namespace SmartGrocery.UseCase.Customer
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateCustomerCommand, CustomerBase>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.Transactions, opt => opt.Ignore())
                .ForMember(x => x.Points, opt => opt.Ignore());

            CreateMap<CustomerBase, CustomerDetailsDto>()
                .ForMember(dest => dest.TransactionDtos, opt => opt.MapFrom(src => src.Transactions));
        }
    }
}