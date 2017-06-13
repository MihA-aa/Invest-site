using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using BLL.DTO;
using PL.Models;

namespace PL.App_Start
{
    public class AutoMapperWebConfiguration
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
            CreateMap<ViewTemplateDTO, ViewTemplateModel>().ReverseMap();
            CreateMap<PortfolioDTO, PortfolioModel>().ReverseMap();
            CreateMap<ViewDTO, ViewModel>().ReverseMap();
            CreateMap<PositionDTO, PositionModel>()
                .ForMember("CurrencySymbol", opt => opt.MapFrom(src => src.Id));
            CreateMap<PositionModel, PositionDTO>();
            CreateMap<CustomerDTO, CustomerModel>().ReverseMap();
            CreateMap<ViewTemplateColumnDTO, ViewTemplateColumnModel>()
                .ForMember("DT_RowId", opt => opt.MapFrom(src => src.Id));
            CreateMap<ViewTemplateColumnModel, ViewTemplateColumnDTO>();
            CreateMap<ProfileDTO, ProfileModel>().ReverseMap();
        }
    }
}