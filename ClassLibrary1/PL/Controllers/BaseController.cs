using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;

namespace PL.Controllers
{
    public class BaseController : Controller
    {
        private IMapper _mapper = null;
        protected IMapper Mapper
        {
            get
            {
                if (_mapper == null) _mapper = MvcApplication.MapperConfiguration.CreateMapper();
                return _mapper;
            }
        }
    }
}