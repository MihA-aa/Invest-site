using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.DTO;
using DAL.Entities;
using DAL.Entities.Views;

namespace BLL.Infrastructure
{
    public class AutoMapperConfiguration
    {
        public MapperConfiguration Configure()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ClientMappingProfile>();
            });
            return config;
        }
    }

    public class ClientMappingProfile : AutoMapper.Profile
    {
        public ClientMappingProfile()
        {
            CreateMap<ViewTemplate, ViewTemplateDTO>().ReverseMap();
            CreateMap<ViewTemplateColumn, ViewTemplateColumnDTO>()
            .ForMember("ColumnName", opt => opt.MapFrom(src => src.Column.Name));
            CreateMap<ViewTemplateColumn, ViewTemplateColumnDTO>().ReverseMap();
            CreateMap<ColumnFormat, ColumnFormatDTO>();
            CreateMap<View, ViewDTO>().ReverseMap();
            CreateMap<DAL.Entities.Profile, ProfileDTO>();
            CreateMap<TradeSybolView, TradeSybolViewDTO>();
            CreateMap<SymbolView, SymbolViewDTO>();
            CreateMap<Position, PositionDTO>().ReverseMap();
            CreateMap<Portfolio, PortfolioDTO>().ReverseMap();
        }
    }
}
