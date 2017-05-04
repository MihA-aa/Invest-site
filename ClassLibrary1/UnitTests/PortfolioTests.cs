using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BLL.Interfaces;
using BLL.Services;
using Moq;
using DAL.Enums;
using DAL.Interfaces;
using DALEF.Repositories;

namespace UnitTests
{
    [TestClass]
    public class PortfolioTests
    {
        private Mock<IUnitOfWork> UnitOfWork;
        private PortfolioService portfolioService;
        private Mock<IRepository<Position>> positionRepository;
        List<Position> ListPositions;
        List<Portfolio> ListPortfolios;

        [TestInitialize]
        public void Initialize()
        {
            #region
            Position position1 = new Position
            {
                Id = 1,
                SymbolId = 3,
                SymbolType = Symbols.Option,
                SymbolName = "PLSE",
                Name = "Pulse Biosciences CS",
                OpenDate = new DateTime(2015, 7, 20),
                OpenPrice = 128.32m,
                OpenWeight = 40,
                TradeType = TradeTypes.Long,
                TradeStatus = TradeStatuses.Open,
                Dividends = 57.3m,
                CloseDate = new DateTime(2016, 1, 12),
                ClosePrice = 218.32m,
                CurrentPrice = 99.53m,
                Gain = 87.12m,
                AbsoluteGain = 110.34m,
                MaxGain = 154.34m
            };
            Position position2 = new Position
            {
                Id = 2,
                SymbolId = 2,
                SymbolType = Symbols.Stock,
                SymbolName = "WIWTY",
                Name = "Witwatersrand Gold Rsrcs Ltd ",
                OpenDate = new DateTime(2009, 2, 24),
                OpenPrice = 4.00m,
                OpenWeight = 125,
                TradeType = TradeTypes.Long,
                TradeStatus = TradeStatuses.Open,
                Dividends = 0.00m,
                CloseDate = new DateTime(2012, 1, 12),
                ClosePrice = 5.60m,
                CurrentPrice = 3.64m,
                Gain = 40.0m,
                AbsoluteGain = 1.60m,
                MaxGain = 1.60m
            };
            Position position3 = new Position
            {
                Id = 3,
                SymbolId = 1,
                SymbolType = Symbols.Option,
                SymbolName = "AAT",
                Name = "AAT",
                OpenDate = new DateTime(2017, 4, 28),
                OpenPrice = 43.20m,
                OpenWeight = 113,
                TradeType = TradeTypes.Long,
                TradeStatus = TradeStatuses.Wait,
                Dividends = 17.34m,
                CloseDate = new DateTime(2017, 5, 2),
                ClosePrice = 54.24m,
                CurrentPrice = 27.98m,
                Gain = 11.56m,
                AbsoluteGain = 9.45m,
                MaxGain = 14.34m
            };
            ListPositions = new List<Position>()
            {
                position1, position2, position3
            };
            #endregion

            UnitOfWork = new Mock<IUnitOfWork>();
            positionRepository = new Mock<IRepository<Position>>();

        }


        [TestMethod]
        public void TestMethod1()
        {

            positionRepository.Setup(m => m.GetAll()).Returns(ListPositions);
            UnitOfWork.Setup(m => m.Positions).Returns(positionRepository.Object);
            portfolioService = new PortfolioService(UnitOfWork.Object);

            var result = portfolioService.GetPositions();

            Assert.AreEqual(result.Count(), 3);
        }
    }
}
