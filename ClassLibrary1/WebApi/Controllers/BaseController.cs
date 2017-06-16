using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using AutoMapper;

namespace WebApi.Controllers
{
    public class BaseController: ApiController
    {
        private IMapper _mapper = null;
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