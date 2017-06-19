using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using AutoMapper;
using log4net;

namespace WebApi.Controllers
{
    public class BaseController: ApiController
    {
        private IMapper _mapper = null;
        protected ILog logger;
        public BaseController()
        {
            logger = LogManager.GetLogger(Type.GetType("WebApi.Controllers." + this.GetType().Name));
        }
        protected IMapper Mapper
        {
            get
            {
                if (_mapper == null) _mapper = WebApiApplication.MapperConfiguration.CreateMapper();
                return _mapper;
            }
        }
    }
}