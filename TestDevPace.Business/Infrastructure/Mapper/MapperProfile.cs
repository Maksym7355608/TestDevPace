using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestDevPace.Business.Models;
using TestDevPace.Data.Entities;

namespace TestDevPace.Business.Infrastructure.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Customer, CustomerModel>().ReverseMap();
        }
    }
}
