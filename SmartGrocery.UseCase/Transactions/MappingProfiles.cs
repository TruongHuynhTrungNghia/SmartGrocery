using AutoMapper;
using SmartGrocery.Model.Product;
using SmartGrocery.Model.Transaction;
using SmartGrocery.UseCase.Product;
using System;

namespace SmartGrocery.UseCase.Transactions
{
    public class MappingProfiles : Profile
    {
        //TODO: Replace it with user email after implementing auth
        private const string Admin = "Admin";

        public MappingProfiles()
        {
            CreateMap<CreateTransactionCommand, Transaction>()
                .ForMember(dest => dest.LastUpdatedAt, opt => opt.UseValue(DateTime.Now))
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Customer, opt => opt.Ignore())
                .ForMember(dest => dest.ProductSnapshot, opt => opt.Ignore())
                .ForMember(dest => dest.CustomerId, opt => opt.Ignore());

            CreateMap<UpdatedProductSnapshot, ProductSnapshot>()
                .ForMember(dest => dest.NumberOfSoldProduct, opt => opt.MapFrom(src => src.Amount))
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.ProductId, opt => opt.Ignore())
                .ForMember(dest => dest.TransactionId, opt => opt.Ignore())
                .ForMember(dest => dest.RemainingProduct, opt => opt.Ignore())
                .ForMember(dest => dest.Product, opt => opt.Ignore())
                .ForMember(dest => dest.Transaction, opt => opt.Ignore())
                .ForMember(dest => dest.Status, opt => opt.Ignore())
                .ForMember(dest => dest.RemainingProduct, opt => opt.Ignore());

            CreateMap<Transaction, TransactionDto>()
                .ForMember(dest => dest.TransactionId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.ProductSnapshotDtos, opt => opt.MapFrom(src => src.ProductSnapshot))
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.CustomerFullName));
        }
    }
}