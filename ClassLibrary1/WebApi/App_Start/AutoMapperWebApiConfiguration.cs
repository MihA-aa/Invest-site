using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using BLL.DTO;
using WebApi.Models;

namespace WebApi.App_Start
{
    public class AutoMapperWebApiConfiguration
    {
        public static MapperConfiguration GetConfiguration()
        {
            return new MapperConfiguration(_ =>
            {
                _.AddProfile(new MapperProfile());
            });
        }
    }

    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<PortfolioDTO, PortfolioModel>().ReverseMap();
            CreateMap<PositionDTO, PositionModel>().ReverseMap();
        }
    }
}
