using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using BLL.DTO.Enums;
using BLL.Interfaces;
using BLL.Services;
using DAL.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace UnitTests.Tests
{
    [TestClass]
    public class CalculationTests
    {
        private Mock<CalculationService> mockCalculationService;
        private CalculationService calculationService;
        private readonly decimal dividends = 8.667m;
        [TestInitialize]
        public void Initialize()
        {
            calculationService = new CalculationService();
            mockCalculationService = new Mock<CalculationService>();
        }
        
        [TestMethod]
        public void CanGetAbsoluteGainForOpenLongPositions()
        {
            var result = calculationService.GetAbsoluteGain(121.12m,
                null, 100.02m, 120, dividends, TradeTypesDTO.Long);
            Assert.IsTrue(result == 3572.04m);
        }
        [TestMethod]
        public void CanGetAbsoluteGainForCloseLongPositions()
        {
            var result = calculationService.GetAbsoluteGain(null,
                121.12m, 100.02m, 120, dividends, TradeTypesDTO.Long);
            Assert.IsTrue(result == 3572.04m);
        }
        [TestMethod]
        public void CanGetAbsoluteGainForOpenShortPositions()
        {
            var result = calculationService.GetAbsoluteGain(121.12m,
                null, 100.02m, 120, dividends, TradeTypesDTO.Short);
            Assert.IsTrue(result == -3572.04m);
        }
        [TestMethod]
        public void CanGetAbsoluteGainForCloseShortPositions()
        {
            var result = calculationService.GetAbsoluteGain(null,
                121.12m, 100.02m, 120, dividends, TradeTypesDTO.Short);
            Assert.IsTrue(result == -3572.04m);
        }

        [TestMethod]
        public void CanGetGainWithOpenWeightAndPriceNotZero()
        {
            mockCalculationService.Setup(m => m
            .GetAbsoluteGain(It.IsAny<decimal?>(), It.IsAny<decimal?>(), It.IsAny<decimal>(),
            It.IsAny<int>(), It.IsAny<decimal>(), It.IsAny<TradeTypesDTO>()))
            .Returns(3572.04m);

            var result = mockCalculationService.Object.GetGain(121.12m,
                null, 100.02m, 120, dividends, TradeTypesDTO.Long);

            Assert.IsTrue(result == 0.2976104779044191161767646471m);
        }

        [TestMethod]
        public void CanGetGainWithOpenWeightZero()
        {
            var result = mockCalculationService.Object.GetGain(121.12m,
                null, 100.02m, 0, dividends, TradeTypesDTO.Long);

            Assert.IsTrue(result == 0);
        }

        [TestMethod]
        public void CanGetGainWithOpenPriceZero()
        {
            var result = mockCalculationService.Object.GetGain(121.12m,
                null, 0, 120, dividends, TradeTypesDTO.Long);

            Assert.IsTrue(result == 0);
        }
        [TestMethod]
        public void CanGetGainWithOpenWeightPriceZero()
        {
            var result = mockCalculationService.Object.GetGain(121.12m,
                null, 0, 0, dividends, TradeTypesDTO.Long);

            Assert.IsTrue(result == 0);
        }

        [TestMethod]
        public void CanGetDividends()
        {
            var result = calculationService.GetDividends(dividends, 100);

            Assert.IsTrue(result == 866.7m);
        }

        [TestMethod]
        public void CanGetPortfolioValue()
        {
            var result = calculationService.GetPortfolioValue(new List<decimal>{100.0m, 200.0m, 300.0m });

            Assert.IsTrue(result == 600.0m);
        }
    }
}
//(121.12 + 8.667 - 100.02) * 120
//(100.02 - 8.667 - 121.12)* 120
//3572.04/(100.02*120)
