﻿using AutoMapper;
using SmartGrocery.Model.Product;
using SmartGrocery.Model.Transaction;
using SmartGrocery.UseCase.Product;

namespace SmartGrocery.UseCase.Transactions
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<CreateTransactionCommand, Transaction>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Customer, opt => opt.Ignore())
                .ForMember(dest => dest.ProductSnapshot, opt => opt.Ignore());

            CreateMap<UpdatedProductSnapshot, ProductSnapshot>()
                .ForMember(dest => dest.NumberOfSoldProduct, opt => opt.MapFrom(src => src.NumberOfSoldProduct))
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.ProductId, opt => opt.Ignore())
                .ForMember(dest => dest.TransactionId, opt => opt.Ignore())
                .ForMember(dest => dest.RemainingProduct, opt => opt.Ignore())
                .ForMember(dest => dest.Product, opt => opt.Ignore())
                .ForMember(dest => dest.Transaction, opt => opt.Ignore())
                .ForMember(dest => dest.Status, opt => opt.Ignore())
                .ForMember(dest => dest.RemainingProduct, opt => opt.Ignore());
        }
    }
}