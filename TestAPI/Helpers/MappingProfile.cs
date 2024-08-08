using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TestAPI.Data.Dtos.Shops;
using TestAPI.Data.Dtos.Products;
using TestAPI.Models;
using TestAPI.Data.Dtos.User;

namespace TestAPI.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile(){
            CreateMap<CreateShopDto, Shop>();
            CreateMap<Shop, CreateShopDto>();
            CreateMap<UpdateShopDto, Shop>();
            CreateMap<Shop, UpdateShopDto>();

            CreateMap<Product, CreateProductDto>();
            CreateMap<CreateProductDto, Product>();
            CreateMap<Product, UpdateProductDto>();
            CreateMap<UpdateProductDto, Product>();

            CreateMap<UserDto, User>()
            .ForMember(dest => dest.Roles, opt => opt.Ignore());
        }
    }
    
}