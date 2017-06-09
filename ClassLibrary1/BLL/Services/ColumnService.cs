using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;

namespace BLL.Services
{
    public class ColumnService: IColumnService
    {
        IUnitOfWork db { get; }
        IMapper IMapper { get; }
        public ColumnService(IUnitOfWork uow, IMapper map)
        {
            db = uow;
            IMapper = map;
        }
        public IEnumerable<ColumnDTO> GetColumns()
        {
            return IMapper.Map<IEnumerable<Column>, List<ColumnDTO>>(db.Columns.GetAll());
        }
    }
}
