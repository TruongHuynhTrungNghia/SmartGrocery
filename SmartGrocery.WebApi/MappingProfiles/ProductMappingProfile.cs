﻿using AutoMapper;
using SmartGrocery.UseCase.Product;
using SmartGrocery.WebApi.Contracts.BaseProduct;

namespace SmartGrocery.WebApi.MappingProfiles
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<EditProductCommnand, EditProductRequest>();
        }
    }
}