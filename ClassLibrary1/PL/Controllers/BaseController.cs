﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using BLL.Interfaces;
using BLL.Services;
using log4net;

namespace PL.Controllers
{
    public class BaseController : Controller
    {
        private IMapper _mapper = null;

        protected ILog logger;
        public BaseController()
        {
            try
            {
                logger = LogManager.GetLogger(Type.GetType("PL.Controllers." + this.GetType().Name));
            }
            catch (Exception)
            {
                logger = LogManager.GetLogger("Unit test");
            }
        }
        public  virtual IMapper Mapper
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