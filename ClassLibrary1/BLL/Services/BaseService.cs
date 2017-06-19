using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Interfaces;
using DAL.Interfaces;

namespace BLL.Services
{
    public class BaseService
    {
        protected IUnitOfWork db { get; }
        protected IValidateService validateService { get; }
        protected IMapper IMapper { get; }
        public BaseService(IUnitOfWork uow, IValidateService vd, IMapper map)
        {
            db = uow;
            validateService = vd;
            IMapper = map;
        }

    }
}
