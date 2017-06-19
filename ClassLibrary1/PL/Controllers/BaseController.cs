using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using log4net;

namespace PL.Controllers
{
    public class BaseController : Controller
    {
        private IMapper _mapper = null;

        protected ILog logger;
        public BaseController()
        {
            logger = LogManager.GetLogger(Type.GetType("PL.Controllers." + this.GetType().Name));
        }
        protected IMapper Mapper
        {
            get
            {
                if (_mapper == null)
                    _mapper = MvcApplication.MapperConfiguration.CreateMapper();
                return _mapper;
            }
        }
    }
}