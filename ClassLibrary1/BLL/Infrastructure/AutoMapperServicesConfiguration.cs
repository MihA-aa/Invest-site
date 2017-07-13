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
            .ForMember("ColumnName", opt => opt.MapFrom(src => src.Column.Name))
            .ForMember("FormatName", opt => opt.MapFrom(src => src.ColumnFormat.Name));
            CreateMap<ViewTemplateColumnDTO, ViewTemplateColumn>();
            CreateMap<ColumnFormat, ColumnFormatDTO>();
            CreateMap<ViewForTable, ViewDTO>().ReverseMap();
            CreateMap<DAL.Entities.Profile, ProfileDTO>().ReverseMap();
            CreateMap<TradeSybolView, TradeSybolViewDTO>();
            CreateMap<SymbolView, SymbolViewDTO>();
            CreateMap<Position, PositionDTO>().ReverseMap();
            CreateMap<Portfolio, PortfolioDTO>().ReverseMap();
            CreateMap<Customer, CustomerDTO>().ReverseMap();
            CreateMap<Record, RecordDTO>().ReverseMap();
            CreateMap<Column, ColumnDTO>();
        }
    }
}
