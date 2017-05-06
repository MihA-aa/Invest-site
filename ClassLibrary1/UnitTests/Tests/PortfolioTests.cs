﻿using System;
using System.Collections.Generic;
using System.Linq;
using BLL.DTO;
using DAL.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BLL.Services;
using Moq;
using DAL.Enums;
using DAL.Interfaces;
using BLL.Infrastructure;
using UnitTests.Attributes;

namespace UnitTests.Tests
{
    [TestClass]
    public class PortfolioTests
    {
        private Mock<IUnitOfWork> UnitOfWork;
        private PortfolioService portfolioService;
        private Mock<IPortfolioRepository> portfolioRepository;
        List<Portfolio> ListPortfolios;
        private ValidateService validateService;
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
        PositionDTO newPosition = new PositionDTO
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

        [TestInitialize]
        public void Initialize()
        {
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
            #endregion
            
            ListPortfolios = new List<Portfolio> { portfolio1, portfolio2 };
            UnitOfWork = new Mock<IUnitOfWork>();
            portfolioRepository = new Mock<IPortfolioRepository>();
            validateService = new ValidateService();

        }
        [TestMethod]
        public void CanGetAllPortfolios()
        {
            portfolioRepository.Setup(m => m.GetAll()).Returns(ListPortfolios);
            UnitOfWork.Setup(m => m.Portfolios).Returns(portfolioRepository.Object);
            portfolioService = new PortfolioService(UnitOfWork.Object, validateService);

            IEnumerable<PortfolioDTO> result = portfolioService.GetPortfolios();

            List<PortfolioDTO> positions = result.ToList();
            Assert.IsTrue(positions.Count == 2);
            Assert.AreEqual(positions[0].Name, "Strategic Investment Open Portfolio");
            Assert.AreEqual(positions[1].Name, "Strategic Investment Income Portfolio");
        }

        [TestMethod]
        public void CanGetPortfolioById()
        {
            portfolioRepository.Setup(c => c.Get(It.IsAny<int>()))
                .Returns((int i) => ListPortfolios.FirstOrDefault(c => c.Id == i));
            UnitOfWork.Setup(m => m.Portfolios).Returns(portfolioRepository.Object);
            portfolioService = new PortfolioService(UnitOfWork.Object, validateService);

            PortfolioDTO portfolio1 = portfolioService.GetPortfolio(1);
            PortfolioDTO portfolio2 = portfolioService.GetPortfolio(2);

            Assert.AreEqual(portfolio1.Name, "Strategic Investment Open Portfolio");
            Assert.AreEqual(portfolio2.Name, "Strategic Investment Income Portfolio");
        }

        [TestMethod]
        [MyExpectedException(typeof(ValidationException),
         "Not set id of portfolio")]
        public void CanNotGetPortfolioByNullId()
        {
            UnitOfWork.Setup(m => m.Portfolios).Returns(portfolioRepository.Object);
            portfolioService = new PortfolioService(UnitOfWork.Object, validateService);

            portfolioService.GetPortfolio(null);
        }

        [TestMethod]
        [MyExpectedException(typeof(ValidationException),
         "Portfolio not found")]
        public void CanNotGetNonexistentPortfolioByPortfolioId()
        {
            portfolioRepository.Setup(c => c.Get(It.IsAny<int>()))
                .Returns((int i) => ListPortfolios.FirstOrDefault(c => c.Id == i));
            UnitOfWork.Setup(m => m.Portfolios).Returns(portfolioRepository.Object);
            portfolioService = new PortfolioService(UnitOfWork.Object, validateService);

            portfolioService.GetPortfolio(5);
        }

        [TestMethod]
        public void CanGetPortfolioPositionsByPortfolioId()
        {
            portfolioRepository.Setup(c => c.Get(It.IsAny<int>()))
                .Returns((int i) => ListPortfolios.FirstOrDefault(c => c.Id == i));
            UnitOfWork.Setup(m => m.Portfolios).Returns(portfolioRepository.Object);
            portfolioService = new PortfolioService(UnitOfWork.Object, validateService);

            IEnumerable<PositionDTO> positions1 = portfolioService.GetPortfolioPositions(1);
            IEnumerable<PositionDTO> positions2 = portfolioService.GetPortfolioPositions(2);

            Assert.IsTrue(positions1.Count() == 2);
            Assert.IsTrue(positions2.Count() == 1);
            Assert.AreEqual(positions1.ToList()[0].Name, "Pulse Biosciences CS");
            Assert.AreEqual(positions1.ToList()[1].Name, "Witwatersrand Gold Rsrcs Ltd");
            Assert.AreEqual(positions2.ToList()[0].Name, "AAT Corporation Limited");
        }

        [TestMethod]
        [MyExpectedException(typeof(ValidationException),
         "Not set id of portfolio")]
        public void CanNotGetPortfolioPositionsByNullId()
        {
            UnitOfWork.Setup(m => m.Portfolios).Returns(portfolioRepository.Object);
            portfolioService = new PortfolioService(UnitOfWork.Object, validateService);

            portfolioService.GetPortfolioPositions(null);
        }

        [TestMethod]
        [MyExpectedException(typeof(ValidationException),
         "Portfolio not found")]
        public void CanNotGetPositionsNonexistentPortfolio()
        {
            portfolioRepository.Setup(c => c.Get(It.IsAny<int>()))
                .Returns((int i) => ListPortfolios.FirstOrDefault(c => c.Id == i));
            UnitOfWork.Setup(m => m.Portfolios).Returns(portfolioRepository.Object);
            portfolioService = new PortfolioService(UnitOfWork.Object, validateService);

            portfolioService.GetPortfolioPositions(5);
        }

        [TestMethod]
        public void CanCreatePortfolio()
        {
            portfolioRepository.Setup(m => m.Create(It.IsAny<Portfolio>()))
                .Callback<Portfolio>(ListPortfolios.Add); ;
            UnitOfWork.Setup(m => m.Portfolios).Returns(portfolioRepository.Object);
            portfolioService = new PortfolioService(UnitOfWork.Object, validateService);
            
            portfolioService.CreatePortfolio(new PortfolioDTO());

            Assert.IsTrue(ListPortfolios.Count() == 3);
        }

        [TestMethod]
        [MyExpectedException(typeof(ValidationException),
            "Percent Wins of portfolio cannot be less than zero")]
        public void CanNotCreatePortfolioWithPercenWinsLessThanZero()
        {
            UnitOfWork.Setup(m => m.Portfolios).Returns(portfolioRepository.Object);
            portfolioService = new PortfolioService(UnitOfWork.Object, validateService);

            portfolioService.CreatePortfolio(new PortfolioDTO { PercentWins = -1 });
        }

        [TestMethod]
        public void CanAddPositionToPortfolio()
        {
            Portfolio portfolio = new Portfolio();
            portfolioRepository.Setup(c => c.Get(It.IsAny<int>()))
                .Returns((int i) => ListPortfolios.FirstOrDefault(c => c.Id == i));
            portfolioRepository.Setup(c =>c.AddPositionToPortfolio(It.IsAny<Position>(), It.IsAny<int>()))
                .Callback((Position p, int i) =>
            {
                portfolio = ListPortfolios.FirstOrDefault(c => c.Id == i);
                portfolio.Positions.Add(p);
            });
            UnitOfWork.Setup(m => m.Portfolios).Returns(portfolioRepository.Object);
            portfolioService = new PortfolioService(UnitOfWork.Object, validateService);

            portfolioService.AddPositionToPortfolio(newPosition, 1);
            IEnumerable<Position> positions1 = portfolio.Positions;
            
            Assert.IsTrue(positions1.Count() == 3);
        }
        
        [TestMethod]
        [MyExpectedException(typeof(ValidationException),
         "Position is null reference")]
        public void CanNotAddNonexistentPositionToPortfolio()
        {
            UnitOfWork.Setup(m => m.Portfolios).Returns(portfolioRepository.Object);
            portfolioService = new PortfolioService(UnitOfWork.Object, validateService);

            portfolioService.AddPositionToPortfolio(null, 1);
        }

        [TestMethod]
        [MyExpectedException(typeof(ValidationException),
         "Not set id of portfolio")]
        public void CanNotAddPositionWithNullPortfolioId()
        {
            UnitOfWork.Setup(m => m.Portfolios).Returns(portfolioRepository.Object);
            portfolioService = new PortfolioService(UnitOfWork.Object, validateService);

            portfolioService.AddPositionToPortfolio(newPosition, null);
        }

        [TestMethod]
        [MyExpectedException(typeof(ValidationException),
         "Portfolio not found")]
        public void CanNotAddPositionInNonexistentPortfolio()
        {
            portfolioRepository.Setup(c => c.Get(It.IsAny<int>()))
                .Returns((int i) => ListPortfolios.FirstOrDefault(c => c.Id == i));
            UnitOfWork.Setup(m => m.Portfolios).Returns(portfolioRepository.Object);
            portfolioService = new PortfolioService(UnitOfWork.Object, validateService);

            portfolioService.AddPositionToPortfolio(newPosition, 5);
        }

        [TestMethod]
        public void CanDeletePortfolio()
        {
            portfolioRepository.Setup(c => c.Get(It.IsAny<int>()))
                .Returns((int i) => ListPortfolios.FirstOrDefault(c => c.Id == i));
            portfolioRepository.Setup(m => m.Delete(It.IsAny<int>()))
                .Callback<int>(i => ListPortfolios.RemoveAll(c => c.Id == i));
            UnitOfWork.Setup(m => m.Portfolios).Returns(portfolioRepository.Object);
            portfolioService = new PortfolioService(UnitOfWork.Object, validateService);

            portfolioService.DeletePortfolio(1);

            Assert.IsTrue(ListPortfolios.Count() == 1);
        }

        [TestMethod]
        [MyExpectedException(typeof(ValidationException),
         "Not set id of portfolio")]
        public void CanNotDeletePortfolioByNullId()
        {
            UnitOfWork.Setup(m => m.Portfolios).Returns(portfolioRepository.Object);
            portfolioService = new PortfolioService(UnitOfWork.Object, validateService);

            portfolioService.DeletePortfolio(null);
        }

        [TestMethod]
        [MyExpectedException(typeof(ValidationException),
         "Portfolio not found")]
        public void CanNotDeleteNonexistPortfolio()
        {
            portfolioRepository.Setup(c => c.Get(It.IsAny<int>()))
                .Returns((int i) => ListPortfolios.FirstOrDefault(c => c.Id == i));
            UnitOfWork.Setup(m => m.Portfolios).Returns(portfolioRepository.Object);
            portfolioService = new PortfolioService(UnitOfWork.Object, validateService);

            portfolioService.DeletePortfolio(5);
        }

        [TestMethod]
        public void CanUpdatePortfolio()
        {
            portfolioRepository.Setup(c => c.Get(It.IsAny<int>()))
                .Returns((int i) => ListPortfolios.FirstOrDefault(c => c.Id == i));
            portfolioRepository.Setup(m => m.Update(It.IsAny<Portfolio>()))
                .Callback<Portfolio>(p =>
            {
                int index = ListPortfolios.IndexOf(ListPortfolios.FirstOrDefault(c => c.Id == p.Id));
                ListPortfolios[index] = p;
            });
            UnitOfWork.Setup(m => m.Portfolios).Returns(portfolioRepository.Object);
            portfolioService = new PortfolioService(UnitOfWork.Object, validateService);

            #region
            PortfolioDTO updatePortfolio = new PortfolioDTO
            {
                Id = 1,
                Name = "Update Portfolio",
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
                PortfolioValue = 1532.42m
            };
            #endregion
            portfolioService.UpdatePortfolio(updatePortfolio);

            Assert.IsTrue(ListPortfolios.FirstOrDefault(c => c.Id == 1).Name == "Update Portfolio");
        }

        [TestMethod]
        [MyExpectedException(typeof(ValidationException),
         "Percent Wins of portfolio cannot be less than zero")]
        public void CanNotUpdatePortfolioWithPercenWinsLessThanZero()
        {
            portfolioRepository.Setup(c => c.Get(It.IsAny<int>()))
                .Returns((int i) => ListPortfolios.FirstOrDefault(c => c.Id == i));
            UnitOfWork.Setup(m => m.Portfolios).Returns(portfolioRepository.Object);
            portfolioService = new PortfolioService(UnitOfWork.Object, validateService);

            portfolioService.UpdatePortfolio(new PortfolioDTO { PercentWins = -1 });
        }

        [TestMethod]
        [MyExpectedException(typeof(ValidationException),
         "Portfolio is null reference")]
        public void CanNotUpdateNullReferencePortfolio()
        {
            UnitOfWork.Setup(m => m.Portfolios).Returns(portfolioRepository.Object);
            portfolioService = new PortfolioService(UnitOfWork.Object, validateService);

            portfolioService.UpdatePortfolio(null);
        }

        [TestMethod]
        [MyExpectedException(typeof(ValidationException),
         "Portfolio not found")]
        public void CanNotUpdateNonexistPortfolio()
        {
            portfolioRepository.Setup(c => c.Get(It.IsAny<int>()))
                .Returns((int i) => ListPortfolios.FirstOrDefault(c => c.Id == i));
            UnitOfWork.Setup(m => m.Portfolios).Returns(portfolioRepository.Object);
            portfolioService = new PortfolioService(UnitOfWork.Object, validateService);
            
            portfolioService.UpdatePortfolio(new PortfolioDTO { Id = 5 });
        }
    }
}