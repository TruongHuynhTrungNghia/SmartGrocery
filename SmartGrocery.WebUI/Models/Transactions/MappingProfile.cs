using AutoMapper;
using SmartGrocery.WebApi.Contracts.Transaction;

namespace SmartGrocery.WebUI.Models.Transactions
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TransactionDetails, TransactionDetailsViewModel>();
            CreateMap<TransactionDetailsViewModel, CreateTransactionRequest>()
                .ForMember(dest => dest.ProductSnapshotContracts, opt => opt.MapFrom(src => src.ProductSnapshots));

            CreateMap<TransactionDetailsViewModel, EditTransactionRequest>()
                .ForMember(dest => dest.ProductSnapshotContracts, opt => opt.MapFrom(src => src.ProductSnapshots));
        }
    }
}