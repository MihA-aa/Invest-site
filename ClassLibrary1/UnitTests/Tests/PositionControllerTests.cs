using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BLL.DTO;
using BLL.DTO.Enums;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PL.App_Start;
using PL.Controllers;
using PL.Models;

namespace UnitTests.Tests
{
    [TestClass]
    public class PositionControllerTests
    {
        private Mock<IPositionService> positionService;
        private PositionController positionController;
        List<PositionDTO> ListPositions;
        private Mock<PositionController> mock;

        #region positions initialize
        PositionDTO position1 = new PositionDTO
        {
            Id = 1,
            SymbolId = 3,
            SymbolType = SymbolsDTO.Option,
            SymbolName = "PLSE",
            Name = "Pulse Biosciences CS",
            OpenDate = new DateTime(2015, 7, 20),
            OpenPrice = 128.32m,
            OpenWeight = 40,
            TradeType = TradeTypesDTO.Long,
            TradeStatus = TradeStatusesDTO.Open,
            Dividends = 57.3m,
            CloseDate = new DateTime(2016, 1, 12),
            ClosePrice = 218.32m,
            CurrentPrice = 99.53m,
            Gain = 87.12m,
            AbsoluteGain = 110.34m,
            MaxGain = 154.34m
        };
        PositionModel positionm1 = new PositionModel
        {
            Id = 1,
            SymbolId = 3,
            SymbolType = SymbolsDTO.Option,
            SymbolName = "PLSE",
            Name = "Pulse Biosciences CS",
            OpenDate = new DateTime(2015, 7, 20),
            OpenPrice = 128.32m,
            OpenWeight = 40,
            TradeType = TradeTypesDTO.Long,
            TradeStatus = TradeStatusesDTO.Open,
            Dividends = 57.3m,
            CloseDate = new DateTime(2016, 1, 12),
            ClosePrice = 218.32m,
            CurrentPrice = 99.53m,
            Gain = 87.12m,
            AbsoluteGain = 110.34m,
            MaxGain = 154.34m
        };
        PositionDTO position2 = new PositionDTO
        {
            Id = 2,
            SymbolId = 2,
            SymbolType = SymbolsDTO.Stock,
            SymbolName = "WIWTY",
            Name = "Witwatersrand Gold Rsrcs Ltd",
            OpenDate = new DateTime(2009, 2, 24),
            OpenPrice = 4.00m,
            OpenWeight = 125,
            TradeType = TradeTypesDTO.Long,
            TradeStatus = TradeStatusesDTO.Open,
            Dividends = 0.00m,
            CloseDate = new DateTime(2012, 1, 12),
            ClosePrice = 5.60m,
            CurrentPrice = 3.64m,
            Gain = 40.0m,
            AbsoluteGain = 1.60m,
            MaxGain = 1.60m
        };
        PositionDTO position3 = new PositionDTO
        {
            Id = 3,
            SymbolId = 1,
            SymbolType = SymbolsDTO.Option,
            SymbolName = "AAT",
            Name = "AAT Corporation Limited",
            OpenDate = new DateTime(2017, 4, 28),
            OpenPrice = 43.20m,
            OpenWeight = 113,
            TradeType = TradeTypesDTO.Long,
            TradeStatus = TradeStatusesDTO.Wait,
            Dividends = 17.34m,
            CloseDate = new DateTime(2017, 5, 2),
            ClosePrice = 54.24m,
            CurrentPrice = 27.98m,
            Gain = 11.56m,
            AbsoluteGain = 9.45m,
            MaxGain = 14.34m
        };
        #endregion

        [TestInitialize]
        public void Initialize()
        {
            ListPositions = new List<PositionDTO> { position1, position2, position3 };
            positionService = new Mock<IPositionService>();
        }

        [TestMethod]
        public void CreatePortfolio_ReturnPartialViewWithData()
        {
            positionService.Setup(c => c.GetPosition(It.IsAny<int>()))
                .Returns((int i) => ListPositions.FirstOrDefault(c => c.Id == i));
            mock = new Mock<PositionController>(positionService.Object) { CallBase = true };
            mock.SetupGet(p => p.Mapper).Returns(AutoMapperWebConfiguration.GetConfiguration().CreateMapper());
            positionController = mock.Object;

            var result = positionController.Save(1) as PartialViewResult;

            Assert.IsNotNull(result);
            var resultPosition = (PositionModel)result.Model;
            Assert.IsTrue(resultPosition.Id == positionm1.Id);
            Assert.IsTrue(resultPosition.Name == positionm1.Name);
        }

        [TestMethod]
        public void CreatePortfolio_ReturnPartialView()
        {
            positionController = new PositionController(positionService.Object);

            var result = positionController.Save(0) as PartialViewResult;

            Assert.IsNotNull(result);
            Assert.IsNull(result.Model);
        }

        [TestMethod]
        public void CreatePortfolio_ModelStateNotValid()
        {
            positionController = new PositionController(positionService.Object);

            positionController.ModelState.AddModelError("test", "test");
            positionController.Save(new PositionModel { Name = null }, 1);

            Assert.IsTrue(positionController.ViewData.ModelState.Count == 1, "test");
        }

        [TestMethod]
        public void CreatePortfolio_Success()
        {
            positionService.Setup(m => m.CreateOrUpdatePosition(It.IsAny<PositionDTO>(), It.IsAny<int?>()))
                .Callback((PositionDTO position, int? i) => ListPositions.Add(position));
            mock = new Mock<PositionController>(positionService.Object) { CallBase = true };
            mock.SetupGet(p => p.Mapper).Returns(AutoMapperWebConfiguration.GetConfiguration().CreateMapper());
            positionController = mock.Object;

            JsonResult result = (JsonResult)positionController.Save(new PositionModel { Id = 7 }, 1);

            Assert.IsTrue(ListPositions.Count() == 4);
        }


        [TestMethod]
        public void CangetDeletePosition_ReturnPartialView()
        {
            positionService.Setup(c => c.GetPosition(It.IsAny<int>()))
                .Returns((int i) => ListPositions.FirstOrDefault(c => c.Id == i));
            positionController = new PositionController(positionService.Object);

            var result = positionController.Delete(1) as PartialViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(1, positionController.ViewBag.Id);
        }


        [TestMethod]
        public void CangetDeletePosition_ReturnJson()
        {
            positionService.Setup(c => c.DeletePosition(It.IsAny<int?>()))
                .Callback<int?>(i => ListPositions.RemoveAll(c => c.Id == i.Value));
            positionController = new PositionController(positionService.Object);

            var result = positionController.DeletePosition(1);

            positionService.Verify(m => m.DeletePosition(1));
            Assert.IsTrue(ListPositions.Count() == 2);
        }
    }
}
