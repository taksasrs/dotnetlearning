using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TestAPI.Data.Dtos;
using TestAPI.Models;

namespace TestAPI.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile(){
            CreateMap<CreateShopDto, Shop>();
            CreateMap<Shop, CreateShopDto>();
        }
    }
    
}