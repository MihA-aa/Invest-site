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
    public class ColumnService: BaseService, IColumnService
    {
        public ColumnService(IUnitOfWork uow, IValidateService vd, IMapper map) : base(uow, vd, map) { }
        public IEnumerable<ColumnDTO> GetColumns()
        {
            return IMapper.Map<IEnumerable<Column>, List<ColumnDTO>>(db.Columns.GetAll());
        }
    }
}
