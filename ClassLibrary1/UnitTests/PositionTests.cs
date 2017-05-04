using System;
using System.Collections.Generic;
using System.Linq;
using BLL.DTO;
using BLL.Services;
using DAL.Entities;
using DAL.Enums;
using DAL.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace UnitTests
{
    [TestClass]
    public class PositionTests
    {
        private Mock<IUnitOfWork> UnitOfWork;
        private PortfolioService portfolioService;
        private PositionService positionService;
        private Mock<IRepository<Position>> positionRepository;
        private Mock<IRepository<Portfolio>> portfolioRepository;
        List<Position> ListPositions;
        List<Portfolio> ListPortfolios;

        [TestInitialize]
        public void Initialize()
        {
            #region positions initialize
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
                Name = "Witwatersrand Gold Rsrcs Ltd",
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
                Name = "AAT Corporation Limited",
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

            #region portfolio inizialize
            Portfolio portfolio1 = new Portfolio
            {
                Id = 1,
                Name = "Strategic Investment Open Portfolio",
                Notes = "A portfolio is a grouping of financial assets such as stocks,",
                DisplayIndex = 1,
                LastUpdateDate = new DateTime(2017, 4, 28),
                Visibility = false,
                Quantity = 2,
                PercentWins = 73.23m,
                BiggestWinner = 234.32m,
                BiggestLoser = 12.65m,
                AvgGain = 186.65m,
                MonthAvgGain = 99.436m,
                PortfolioValue = 1532.42m,
                Positions = new List<Position> { position1, position2 }
            };

            Portfolio portfolio2 = new Portfolio
            {
                Id = 2,
                Name = "Strategic Investment Income Portfolio",
                Notes = "A portfolio is a grouping of financial assets such as stocks,",
                DisplayIndex = 2,
                LastUpdateDate = new DateTime(2017, 3, 12),
                Visibility = true,
                Quantity = 3,
                PercentWins = 93.23m,
                BiggestWinner = 534.32m,
                BiggestLoser = 123.46m,
                AvgGain = 316.65m,
                MonthAvgGain = 341.436m,
                PortfolioValue = 5532.42m,
                Positions = new List<Position> { position3 }
            };

            ListPortfolios = new List<Portfolio> { portfolio1, portfolio2 };
            #endregion 

            UnitOfWork = new Mock<IUnitOfWork>();
            positionRepository = new Mock<IRepository<Position>>();
            portfolioRepository = new Mock<IRepository<Portfolio>>();

        }

        [TestMethod]
        public void CanGetAllPositions()
        {
            positionRepository.Setup(m => m.GetAll()).Returns(ListPositions);
            UnitOfWork.Setup(m => m.Positions).Returns(positionRepository.Object);
            positionService = new PositionService(UnitOfWork.Object);

            IEnumerable<PositionDTO> result = positionService.GetPositions();

            List<PositionDTO> positions = result.ToList();
            Assert.IsTrue(positions.Count == 3);
            Assert.AreEqual(positions[0].Name, "Pulse Biosciences CS");
            Assert.AreEqual(positions[1].Name, "Witwatersrand Gold Rsrcs Ltd");
            Assert.AreEqual(positions[2].Name, "AAT Corporation Limited");
        }

        [TestMethod]
        public void CanGetPositionById()
        {
            positionRepository.Setup(c => c.Get(It.IsAny<int>())).Returns((int i) => ListPositions.Single(c => c.Id == i));
            UnitOfWork.Setup(m => m.Positions).Returns(positionRepository.Object);
            positionService = new PositionService(UnitOfWork.Object);

            PositionDTO position1 = positionService.GetPosition(1);
            PositionDTO position2 = positionService.GetPosition(2);
            PositionDTO position3 = positionService.GetPosition(3);

            Assert.AreEqual(position1.Name, "Pulse Biosciences CS");
            Assert.AreEqual(position2.Name, "Witwatersrand Gold Rsrcs Ltd");
            Assert.AreEqual(position3.Name, "AAT Corporation Limited");
        }

        [TestMethod]
        public void CanCreatePosition()
        {
            positionRepository.Setup(m => m.GetAll()).Returns(ListPositions);
            positionRepository.Setup(m => m.Create(It.IsAny<Position>()));
            UnitOfWork.Setup(m => m.Positions).Returns(positionRepository.Object);
            positionService = new PositionService(UnitOfWork.Object);

            #region
            PositionDTO Newposition = new PositionDTO
            {
                Id = 5,
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
            #endregion

            positionService.CreatePosition(Newposition);

            IEnumerable<Position> positions = positionRepository.Object.GetAll();

            Assert.IsTrue(positions.Count() == 4);
        }

    }
}
