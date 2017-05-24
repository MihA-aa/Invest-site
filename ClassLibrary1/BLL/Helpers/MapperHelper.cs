using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.DTO;
using DAL.Entities;

namespace BLL.Helpers
{
    public static class MapperHelper
    {
        public static Position ConvertPositionDtoToPosition(PositionDTO positionDto)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<PositionDTO, Position>());
            return Mapper.Map<PositionDTO, Position>(positionDto);
        }
        public static PositionDTO ConvertPositionToPositionDto(Position position)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Position, PositionDTO>());
            return Mapper.Map<Position, PositionDTO>(position);
        }

        public static IEnumerable<PositionDTO> ConvertListPositionToPositionDto(IEnumerable<Position> positions)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Position, PositionDTO>());
            return Mapper.Map<IEnumerable<Position>, List<PositionDTO>>(positions);
        }
    }
}
