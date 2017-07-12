//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Security.Principal;
//using System.Web;
//using System.Web.Mvc;
//using AutoMapper;
//using BLL.DTO;
//using BLL.Interfaces;
//using BLL.Services;
//using DAL.Entities;
//using log4net;
//using Microsoft.AspNet.Identity;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Moq;
//using PL.App_Start;
//using PL.Controllers;
//using PL.Models;

//namespace UnitTests.Tests
//{
//    [TestClass]
//    public class PortfolioControllerTests
//    {
//        private Mock<IPortfolioService> portfolioService;
//        private PortfolioController portfolioController;
//        List<PortfolioDTO> ListPortfolios;
//        private Mock<PortfolioController> mock;

//        #region portfolio inizialize
//        PortfolioDTO portfolio1 = new PortfolioDTO
//        {
//            Id = 1,
//            Name = "Strategic Investment Open Portfolio",
//            Notes = "A portfolio is a grouping of financial assets such as stocks,",
//            DisplayIndex = 1,
//            LastUpdateDate = new DateTime(2017, 4, 28),
//            Visibility = false,
//            Quantity = 2,
//            PercentWins = 73.23m,
//            BiggestWinner = 234.32m,
//            BiggestLoser = 12.65m,
//            AvgGain = 186.65m,
//            MonthAvgGain = 99.436m,
//            PortfolioValue = 1532.42m
//        };

//        PortfolioDTO portfolio2 = new PortfolioDTO
//        {
//            Id = 2,
//            Name = "Strategic Investment Income Portfolio",
//            Notes = "A portfolio is a grouping of financial assets such as stocks,",
//            DisplayIndex = 2,
//            LastUpdateDate = new DateTime(2017, 3, 12),
//            Visibility = true,
//            Quantity = 3,
//            PercentWins = 93.23m,
//            BiggestWinner = 534.32m,
//            BiggestLoser = 123.46m,
//            AvgGain = 316.65m,
//            MonthAvgGain = 341.436m,
//            PortfolioValue = 5532.42m
//        };
//        #endregion

//        [AssemblyInitialize]
//        public static void Configure(TestContext tc)
//        {
//            log4net.Config.XmlConfigurator.Configure();
//        }

//        [TestInitialize]
//        public void Initialize()
//        {
//            ListPortfolios = new List<PortfolioDTO> {portfolio1, portfolio2};
//            portfolioService = new Mock<IPortfolioService>();
//    }

//        [TestMethod]
//        public void CreatePortfolio_Return_GeneralPartialView()
//        {
//            portfolioController = new PortfolioController(portfolioService.Object);

//            PartialViewResult result = portfolioController.CreatePortfolio();
            
//            Assert.IsNotNull(result);
//            Assert.AreEqual("_General", result.ViewName);
//        }

//        [TestMethod]
//        public void CreateUpdatePortfolioReturnView()
//        {
//            portfolioService.Setup(c => c.CreateOrUpdatePortfolio(It.IsAny<PortfolioDTO>(), It.IsAny<string>()))
//                .Returns((PortfolioDTO i, string id) => 1);
//            mock = new Mock<PortfolioController>(portfolioService.Object) { CallBase = true };
//            mock.SetupGet(p => p.Mapper).Returns(AutoMapperWebConfiguration.GetConfiguration().CreateMapper());
//            portfolioController = mock.Object;
//            var controllerContext = new Mock<ControllerContext>();
//            var principal = new Mock<IPrincipal>();
//            principal.Setup(p => p.IsInRole("Administrator")).Returns(true);
//            principal.SetupGet(x => x.Identity.Name).Returns("userName");
//            controllerContext.SetupGet(x => x.HttpContext.User).Returns(principal.Object);
//            portfolioController.ControllerContext = controllerContext.Object;

//            var result = portfolioController.CreateUpdatePortfolio(new PortfolioModel {Id = 1,Name = "Update Name"}) as RedirectToRouteResult;

//            Assert.IsNotNull(result);
//            Assert.AreEqual("Index", result.RouteValues["action"]);
//            Assert.AreEqual(1, portfolioController.TempData["PortfolioId"]);
//        }

//        [TestMethod]
//        public void DeletePortfolio_ReturnJson()
//        {
//            portfolioService.Setup(c => c.DeletePortfolio(It.IsAny<int?>()))
//                .Callback<int?>(i => ListPortfolios.RemoveAll(c => c.Id == i.Value));
//            portfolioController = new PortfolioController(portfolioService.Object);

//            var result = portfolioController.DeletePortfolio(1);

//            portfolioService.Verify(m => m.DeletePortfolio(1));
//            Assert.IsTrue(ListPortfolios.Count() == 1);
//            Assert.AreEqual("Response from Delete", result.Data);
//        }
//    }
//}
