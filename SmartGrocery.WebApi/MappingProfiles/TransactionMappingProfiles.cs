using AutoMapper;
using SmartGrocery.UseCase.Product;
using SmartGrocery.UseCase.Transactions;
using SmartGrocery.WebApi.Contracts.BaseProduct;
using SmartGrocery.WebApi.Contracts.Transaction;

namespace SmartGrocery.WebApi.MappingProfiles
{
    public class TransactionMappingProfiles : Profile
    {
        public TransactionMappingProfiles()
        {
            CreateMap<CreateTransactionRequest, CreateTransactionCommand>()
                .ForMember(dest => dest.ProductSnapshotDto, opt => opt.MapFrom(src => src.ProductSnapshotContracts));

            CreateMap<EditTransactionRequest, UpdateTransactionCommand>()

                .ForMember(dest => dest.ProductSnapshotDto, opt => opt.MapFrom(src => src.ProductSnapshotContracts));

            CreateMap<ProductSnapshotDto, ProductSnapshotContract>();
        }
    }
}