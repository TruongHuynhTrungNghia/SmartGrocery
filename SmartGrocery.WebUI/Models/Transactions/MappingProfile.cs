using AutoMapper;
using SmartGrocery.WebApi.Contracts.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartGrocery.WebUI.Models.Transactions
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TransactionDetails, TransactionDetailsViewModel>();
            CreateMap<TransactionDetailsViewModel, CreateTransactionRequest>()
                .ForMember(dest => dest.CustomerId, opt => opt.Ignore())
                .ForMember(dest => dest.ProductSnapshotContracts, opt => opt.MapFrom(src => src.ProductSnapshots));
        }
    }
}