//using System;
//using System.Collections.Generic;
//using System.Linq;
//using AutoMapper;
//using BLL.DTO;
//using BLL.DTO.Enums;
//using BLL.Helpers;
//using BLL.Services;
//using DAL.Entities;
//using DAL.Enums;
//using DAL.Interfaces;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Moq;
//using BLL.Infrastructure;
//using BLL.Interfaces;
//using DAL.Entities.Views;
//using UnitTests.Attributes;

//namespace UnitTests.Tests
//{
//    [TestClass]
//    public class PositionTests
//    {
//        private Mock<IUnitOfWork> UnitOfWork;
//        private PositionService positionService;
//        private ValidateService validateService;
//        private CalculationService calculationService;
//        private Mock<ITradeSybolService> tradeSybolService;
//        private Mock<IPositionRepository> positionRepository;
//        private Mock<IPortfolioRepository> portfolioRepository;
//        private Mock<ISymbolDividendRepository> symbolDividendRepository;
//        private IMapper map;
//        List<Position> ListPositions;
//        List<Portfolio> ListPortfolios;

//        #region positions initialize
//        Position position1 = new Position
//        {
//            Id = 1,
//            SymbolId = 3,
//            SymbolType = Symbols.Option,
//            SymbolName = "PLSE",
//            Name = "Pulse Biosciences CS",
//            OpenDate = new DateTime(2015, 7, 20),
//            OpenPrice = 128.32m,
//            OpenWeight = 40,
//            TradeType = TradeTypes.Long,
//            TradeStatus = TradeStatuses.Open,
//            Dividends = 57.3m,
//            CloseDate = new DateTime(2016, 1, 12),
//            ClosePrice = 218.32m,
//            CurrentPrice = 99.53m,
//            Gain = 87.12m,
//            AbsoluteGain = 110.34m,
//            MaxGain = 154.34m
//        };
//        Position position2 = new Position
//        {
//            Id = 2,
//            SymbolId = 2,
//            SymbolType = Symbols.Stock,
//            SymbolName = "WIWTY",
//            Name = "Witwatersrand Gold Rsrcs Ltd",
//            OpenDate = new DateTime(2009, 2, 24),
//            OpenPrice = 4.00m,
//            OpenWeight = 125,
//            TradeType = TradeTypes.Long,
//            TradeStatus = TradeStatuses.Open,
//            Dividends = 0.00m,
//            CloseDate = new DateTime(2012, 1, 12),
//            ClosePrice = 5.60m,
//            CurrentPrice = 3.64m,
//            Gain = 40.0m,
//            AbsoluteGain = 1.60m,
//            MaxGain = 1.60m
//        };
//        Position position3 = new Position
//        {
//            Id = 3,
//            SymbolId = 1,
//            SymbolType = Symbols.Option,
//            SymbolName = "AAT",
//            Name = "AAT Corporation Limited",
//            OpenDate = new DateTime(2017, 4, 28),
//            OpenPrice = 43.20m,
//            OpenWeight = 113,
//            TradeType = TradeTypes.Long,
//            TradeStatus = TradeStatuses.Wait,
//            Dividends = 17.34m,
//            CloseDate = new DateTime(2017, 5, 2),
//            ClosePrice = 54.24m,
//            CurrentPrice = 27.98m,
//            Gain = 11.56m,
//            AbsoluteGain = 9.45m,
//            MaxGain = 14.34m
//        };
//        PositionDTO newPosition = new PositionDTO
//        {
//            Id = 5,
//            SymbolId = 3,
//            SymbolType = SymbolsDTO.Option,
//            SymbolName = "PLSE",
//            Name = "Pulse Biosciences CS",
//            OpenDate = new DateTime(2015, 7, 20),
//            OpenPrice = 128.32m,
//            OpenWeight = 40,
//            TradeType = TradeTypesDTO.Long,
//            TradeStatus = TradeStatusesDTO.Open,
//            Dividends = 57.3m,
//            CloseDate = new DateTime(2016, 1, 12),
//            ClosePrice = 218.32m,
//            CurrentPrice = 99.53m,
//            Gain = 87.12m,
//            AbsoluteGain = 110.34m,
//            MaxGain = 154.34m
//        };
//        #endregion

//        [TestInitialize]
//        public void Initialize()
//        {
//            #region portfolio inizialize
//            Portfolio portfolio1 = new Portfolio
//            {
//                Id = 1,
//                Name = "Strategic Investment Open Portfolio",
//                Notes = "A portfolio is a grouping of financial assets such as stocks,",
//                DisplayIndex = 1,
//                LastUpdateDate = new DateTime(2017, 4, 28),
//                Visibility = false,
//                Quantity = 2,
//                PercentWins = 73.23m,
//                BiggestWinner = 234.32m,
//                BiggestLoser = 12.65m,
//                AvgGain = 186.65m,
//                MonthAvgGain = 99.436m,
//                PortfolioValue = 1532.42m,
//                Positions = new List<Position> { position1, position2 }
//            };

//            Portfolio portfolio2 = new Portfolio
//            {
//                Id = 2,
//                Name = "Strategic Investment Income Portfolio",
//                Notes = "A portfolio is a grouping of financial assets such as stocks,",
//                DisplayIndex = 2,
//                LastUpdateDate = new DateTime(2017, 3, 12),
//                Visibility = true,
//                Quantity = 3,
//                PercentWins = 93.23m,
//                BiggestWinner = 534.32m,
//                BiggestLoser = 123.46m,
//                AvgGain = 316.65m,
//                MonthAvgGain = 341.436m,
//                PortfolioValue = 5532.42m,
//                Positions = null
//            };
//            #endregion
//            ListPositions = new List<Position> { position1, position2, position3 };
//            ListPortfolios = new List<Portfolio> { portfolio1, portfolio2 };
//            UnitOfWork = new Mock<IUnitOfWork>();
//            positionRepository = new Mock<IPositionRepository>();
//            portfolioRepository = new Mock<IPortfolioRepository>();
//            symbolDividendRepository = new Mock<ISymbolDividendRepository>();
//            validateService = new ValidateService();
//            tradeSybolService = new Mock<ITradeSybolService>();
//            calculationService = new CalculationService();
//            map = new AutoMapperConfiguration().Configure().CreateMapper();
//        }

//        [TestMethod]
//        public void CanGetAllPositions()
//        {
//            positionRepository.Setup(m => m.GetAll()).Returns(ListPositions);
//            UnitOfWork.Setup(m => m.Positions).Returns(positionRepository.Object);
//            positionService = new PositionService(UnitOfWork.Object, validateService, tradeSybolService.Object, calculationService, map);

//            IEnumerable<PositionDTO> result = positionService.GetPositions();

//            List<PositionDTO> positions = result.ToList();
//            Assert.IsTrue(positions.Count == 3);
//            Assert.AreEqual(positions[0].Name, "Pulse Biosciences CS");
//            Assert.AreEqual(positions[1].Name, "Witwatersrand Gold Rsrcs Ltd");
//            Assert.AreEqual(positions[2].Name, "AAT Corporation Limited");
//        }

//        [TestMethod]
//        public void CanGetPositionById()
//        {
//            positionRepository.Setup(c => c.Get(It.IsAny<int>()))
//                .Returns((int i) => ListPositions.FirstOrDefault(c => c.Id == i));
//            UnitOfWork.Setup(m => m.Positions).Returns(positionRepository.Object);
//            positionService = new PositionService(UnitOfWork.Object, validateService, tradeSybolService.Object, calculationService, map);

//            PositionDTO position1 = positionService.GetPosition(1);
//            PositionDTO position2 = positionService.GetPosition(2);
//            PositionDTO position3 = positionService.GetPosition(3);

//            Assert.AreEqual(position1.Name, "Pulse Biosciences CS");
//            Assert.AreEqual(position2.Name, "Witwatersrand Gold Rsrcs Ltd");
//            Assert.AreEqual(position3.Name, "AAT Corporation Limited");
//        }

//        [TestMethod]
//        [MyExpectedException(typeof(ValidationException),
//         "Not set id of Position")]
//        public void CanNotGetPositionByNullId()
//        {
//            UnitOfWork.Setup(m => m.Positions).Returns(positionRepository.Object);
//            positionService = new PositionService(UnitOfWork.Object, validateService, tradeSybolService.Object, calculationService, map);

//            positionService.GetPosition(null);
//        }

//        [TestMethod]
//        [MyExpectedException(typeof(ValidationException),
//         "Position not found")]
//        public void CanNotGetNonexistentPositionByPositionId()
//        {
//            positionRepository.Setup(c => c.Get(It.IsAny<int>()))
//                .Returns((int i) => ListPositions.FirstOrDefault(c => c.Id == i));
//            UnitOfWork.Setup(m => m.Positions).Returns(positionRepository.Object);
//            positionService = new PositionService(UnitOfWork.Object, validateService, tradeSybolService.Object, calculationService, map);

//            positionService.GetPosition(5);
//        }

//        [TestMethod]
//        public void CanDeletePosition()
//        {
//            positionRepository.Setup(c => c.IsExist(It.IsAny<int>()))
//                .Returns((int i) => ListPositions.Any(c => c.Id == i));
//            positionRepository.Setup(m => m.Delete(It.IsAny<int>()))
//                .Callback<int>(i => ListPositions.RemoveAll(c => c.Id == i));
//            UnitOfWork.Setup(m => m.Positions).Returns(positionRepository.Object);
//            positionService = new PositionService(UnitOfWork.Object, validateService, tradeSybolService.Object, calculationService, map);

//            positionService.DeletePosition(1);

//            Assert.IsTrue(ListPositions.Count() == 2);
//        }

//        [TestMethod]
//        [MyExpectedException(typeof(ValidationException),
//         "Not set id of Position")]
//        public void CanNotDeletePositionByNullId()
//        {
//            UnitOfWork.Setup(m => m.Positions).Returns(positionRepository.Object);
//            positionService = new PositionService(UnitOfWork.Object, validateService, tradeSybolService.Object, calculationService, map);

//            positionService.DeletePosition(null);
//        }

//        [TestMethod]
//        [MyExpectedException(typeof(ValidationException),
//         "Position not found")]
//        public void CanNotDeleteNonexistPosition()
//        {
//            positionRepository.Setup(c => c.Get(It.IsAny<int>()))
//                .Returns((int i) => ListPositions.FirstOrDefault(c => c.Id == i));
//            UnitOfWork.Setup(m => m.Positions).Returns(positionRepository.Object);
//            positionService = new PositionService(UnitOfWork.Object, validateService, tradeSybolService.Object, calculationService, map);

//            positionService.DeletePosition(5);
//        }


//        [TestMethod]
//        public void CanCreatePosition()
//        {
//            portfolioRepository.Setup(c => c.Get(It.IsAny<int>()))
//                .Returns((int i) => ListPortfolios.FirstOrDefault(c => c.Id == i));
//            positionRepository.Setup(c => c.IsExist(It.IsAny<int>()))
//                .Returns((int i) => ListPositions.Any(c => c.Id == i));
//            portfolioRepository.Setup(m => m.IsExist(It.IsAny<int>()))
//                .Returns((int id) => ListPortfolios.Any(p => p.Id == id));
//            symbolDividendRepository.Setup(c => c.Get(It.IsAny<int>()))
//                .Returns(new SymbolDividend { SymbolID = 39817, DividendAmount = 7.1191m });
//            tradeSybolService.Setup(c => c.GetPriceForDate(It.IsAny<DateTime>(), It.IsAny<int>()))
//                .Returns(0.66m);
//            tradeSybolService.Setup(c => c.GetMaxGainForSymbolBetweenDate(It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<int>(), It.IsAny<TradeTypesDTO>()))
//                .Returns(new TradeInforamation { Price = 2.491m });
//            positionRepository.Setup(m => m.Create(It.IsAny<Position>()))
//                .Callback<Position>(ListPositions.Add);
//            UnitOfWork.Setup(m => m.Portfolios).Returns(portfolioRepository.Object);
//            UnitOfWork.Setup(m => m.Positions).Returns(positionRepository.Object);
//            UnitOfWork.Setup(m => m.SymbolDividends).Returns(symbolDividendRepository.Object);
//            positionService = new PositionService(UnitOfWork.Object, validateService, tradeSybolService.Object, calculationService, map);

//            positionService.CreatePosition(new PositionDTO {OpenWeight = 12, CloseDate = new DateTime(2015, 7, 20) }, 1);

//            Assert.IsTrue(ListPositions.Count() == 4);
//        }

//        [TestMethod]
//        [MyExpectedException(typeof(ValidationException),
//           "Position is null reference")]
//        public void CanNotCreateNullReferencePosition()
//        {
//            UnitOfWork.Setup(m => m.Positions).Returns(positionRepository.Object);
//            positionService = new PositionService(UnitOfWork.Object, validateService, tradeSybolService.Object, calculationService, map);

//            positionService.CreatePosition(null, 1);
//        }

//        [TestMethod]
//        [MyExpectedException(typeof(ValidationException),
//            "The Weight value must be greater than 0 and less than or equal to 10,000.")]
//        public void CanNotCreatePositionWithNotWalidWeight()
//        {
//            UnitOfWork.Setup(m => m.Positions).Returns(positionRepository.Object);
//            positionService = new PositionService(UnitOfWork.Object, validateService, tradeSybolService.Object, calculationService, map);

//            positionService.CreatePosition(new PositionDTO { OpenWeight = -1 }, 1);
//        }

//        [TestMethod]
//        public void CanCreatePositionInCreateOrUpdate()
//        {
//            portfolioRepository.Setup(c => c.Get(It.IsAny<int>()))
//                .Returns((int i) => ListPortfolios.FirstOrDefault(c => c.Id == i));
//            positionRepository.Setup(c => c.IsExist(It.IsAny<int>()))
//                .Returns((int i) => ListPositions.Any(c => c.Id == i));
//            portfolioRepository.Setup(m => m.IsExist(It.IsAny<int>()))
//                .Returns((int id) => ListPortfolios.Any(p => p.Id == id));
//            symbolDividendRepository.Setup(c => c.Get(It.IsAny<int>()))
//                .Returns(new SymbolDividend { SymbolID = 39817, DividendAmount = 7.1191m });
//            tradeSybolService.Setup(c => c.GetPriceForDate(It.IsAny<DateTime>(), It.IsAny<int>()))
//                .Returns(0.66m);
//            tradeSybolService.Setup(c => c.GetMaxGainForSymbolBetweenDate(It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<int>(), It.IsAny<TradeTypesDTO>()))
//                .Returns(new TradeInforamation { Price = 2.491m });
//            positionRepository.Setup(m => m.Create(It.IsAny<Position>()))
//                .Callback<Position>(ListPositions.Add);
//            UnitOfWork.Setup(m => m.Portfolios).Returns(portfolioRepository.Object);
//            UnitOfWork.Setup(m => m.Positions).Returns(positionRepository.Object);
//            UnitOfWork.Setup(m => m.SymbolDividends).Returns(symbolDividendRepository.Object);
//            positionService = new PositionService(UnitOfWork.Object, validateService, tradeSybolService.Object, calculationService, map);

//            positionService.CreateOrUpdatePosition(new PositionDTO { OpenWeight = 12, CloseDate = new DateTime(2015, 7, 20) }, 1);

//            Assert.IsTrue(ListPositions.Count() == 4);
//        }

//        [TestMethod]
//        public void CanUpdatePositionInCreateOrUpdate()
//        {
//            portfolioRepository.Setup(m => m.GetAll()).Returns(ListPortfolios);
//            positionRepository.Setup(c => c.IsExist(It.IsAny<int>()))
//                .Returns((int i) => ListPositions.Any(c => c.Id == i));
//            positionRepository.Setup(m => m.Update(It.IsAny<Position>())).Callback<Position>(p =>
//            {
//                int index = ListPositions.IndexOf(ListPositions.FirstOrDefault(c => c.Id == p.Id));
//                ListPositions[index] = p;
//            });
//            symbolDividendRepository.Setup(c => c.Get(It.IsAny<int>()))
//                .Returns(new SymbolDividend { SymbolID = 39817, DividendAmount = 7.1191m });
//            tradeSybolService.Setup(c => c.GetPriceForDate(It.IsAny<DateTime>(), It.IsAny<int>()))
//                .Returns(0.66m);
//            tradeSybolService.Setup(c => c.GetMaxGainForSymbolBetweenDate(It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<int>(), It.IsAny<TradeTypesDTO>()))
//                .Returns(new TradeInforamation { Price = 2.491m });
//            UnitOfWork.Setup(m => m.Positions).Returns(positionRepository.Object);
//            UnitOfWork.Setup(m => m.Portfolios).Returns(portfolioRepository.Object);
//            UnitOfWork.Setup(m => m.SymbolDividends).Returns(symbolDividendRepository.Object);
//            positionService = new PositionService(UnitOfWork.Object, validateService, tradeSybolService.Object, calculationService, map);

//            #region
//            var updatePosition = new PositionDTO
//            {
//                Id = 1,
//                Name = "New update position",
//                OpenWeight = 123,
//                CloseDate = new DateTime(1, 1, 1, 0, 0, 0)
//            };
//            #endregion
//            positionService.CreateOrUpdatePosition(updatePosition, 1);

//            Assert.IsTrue(ListPositions.FirstOrDefault(c => c.Id == 1).Name == "New update position");
//        }

//        [TestMethod]
//        [MyExpectedException(typeof(ValidationException),
//           "Position is null reference")]
//        public void CanNotCreateNullReferencePositionInCreateOrUpdate()
//        {
//            UnitOfWork.Setup(m => m.Positions).Returns(positionRepository.Object);
//            positionService = new PositionService(UnitOfWork.Object, validateService, tradeSybolService.Object, calculationService, map);

//            positionService.CreateOrUpdatePosition(null, 1);
//        }


//        [TestMethod]
//        public void CanUpdatePosition()
//        {
//            portfolioRepository.Setup(m => m.GetAll()).Returns(ListPortfolios);
//            positionRepository.Setup(c => c.IsExist(It.IsAny<int>()))
//                .Returns((int i) => ListPositions.Any(c => c.Id == i));
//            positionRepository.Setup(m => m.Update(It.IsAny<Position>())).Callback<Position>(p =>
//            {
//                int index = ListPositions.IndexOf(ListPositions.FirstOrDefault(c => c.Id == p.Id));
//                ListPositions[index] = p;
//            });
//            symbolDividendRepository.Setup(c => c.Get(It.IsAny<int>()))
//                .Returns(new SymbolDividend {SymbolID = 39817, DividendAmount = 7.1191m });
//            tradeSybolService.Setup(c => c.GetPriceForDate(It.IsAny<DateTime>(), It.IsAny<int>()))
//                .Returns( 0.66m );
//            tradeSybolService.Setup(c => c.GetMaxGainForSymbolBetweenDate(It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<int>(), It.IsAny<TradeTypesDTO>()))
//                .Returns(new TradeInforamation { Price = 2.491m });
//            UnitOfWork.Setup(m => m.Positions).Returns(positionRepository.Object);
//            UnitOfWork.Setup(m => m.Portfolios).Returns(portfolioRepository.Object);
//            UnitOfWork.Setup(m => m.SymbolDividends).Returns(symbolDividendRepository.Object);
//            positionService = new PositionService(UnitOfWork.Object, validateService, tradeSybolService.Object, calculationService, map);

//            #region
//            var updatePosition = new PositionDTO
//            {
//                Id = 1,
//                Name = "New update position",
//                OpenWeight = 123,
//                CloseDate =  new DateTime(1, 1, 1, 0, 0, 0)
//            };
//            #endregion
//            positionService.UpdatePosition(updatePosition);

//            Assert.IsTrue(ListPositions.FirstOrDefault(c => c.Id == 1).Name == "New update position");
//        }

//        [TestMethod]
//        [MyExpectedException(typeof(ValidationException),
//         "Position is null reference")]
//        public void CanNotUpdateNullReferencePosition()
//        {
//            UnitOfWork.Setup(m => m.Positions).Returns(positionRepository.Object);
//            positionService = new PositionService(UnitOfWork.Object, validateService, tradeSybolService.Object, calculationService, map);

//            positionService.UpdatePosition((PositionDTO)null);
//        }

//        [TestMethod]
//        [MyExpectedException(typeof(ValidationException),
//        "Position not found")]
//        public void CanNotUpdateNonexistentPosition()
//        {
//            positionRepository.Setup(c => c.IsExist(It.IsAny<int>()))
//                .Returns((int i) => ListPositions.Any(c => c.Id == i));
//            UnitOfWork.Setup(m => m.Positions).Returns(positionRepository.Object);
//            positionService = new PositionService(UnitOfWork.Object, validateService, tradeSybolService.Object, calculationService, map);

//            #region
//            var updatePosition = new PositionDTO
//            {
//                Id = 19,
//                Name = "New update position",
//                OpenWeight = 123
//            };
//            #endregion
//            positionService.UpdatePosition(updatePosition);
//        }

//        public void CanAddPositionToPortfolio()
//        {
//            Portfolio portfolio = new Portfolio();
//            portfolioRepository.Setup(c => c.Get(It.IsAny<int>()))
//                .Returns((int i) => ListPortfolios.FirstOrDefault(c => c.Id == i));
//            portfolioRepository.Setup(c => c.AddPositionToPortfolio(It.IsAny<Position>(), It.IsAny<int>()))
//                .Callback((Position p, int i) =>
//            {
//                portfolio = ListPortfolios.FirstOrDefault(c => c.Id == i);
//                portfolio.Positions.Add(p);
//            });
//            UnitOfWork.Setup(m => m.Portfolios).Returns(portfolioRepository.Object);
//            positionService = new PositionService(UnitOfWork.Object, validateService, tradeSybolService.Object, calculationService, map);

//            positionService.AddPositionToPortfolio(new Position(), 1);

//            Assert.IsTrue(portfolio.Positions.Count() == 3);
//        }

//        [TestMethod]
//        [MyExpectedException(typeof(ValidationException),
//         "Position is null reference")]
//        public void CanNotAddNullReferencePositionToPortfolio()
//        {
//            UnitOfWork.Setup(m => m.Positions).Returns(positionRepository.Object);
//            positionService = new PositionService(UnitOfWork.Object, validateService, tradeSybolService.Object, calculationService, map);

//            positionService.AddPositionToPortfolio(null, 1);
//        }

//        [TestMethod]
//        [MyExpectedException(typeof(ValidationException),
//         "Not set id of Portfolio")]
//        public void CanNotAddPositionWithNotSetIdOfPortfolio()
//        {
//            UnitOfWork.Setup(m => m.Positions).Returns(positionRepository.Object);
//            positionService = new PositionService(UnitOfWork.Object, validateService, tradeSybolService.Object, calculationService, map);

//            positionService.AddPositionToPortfolio(new Position(), null);
//        }

//        [TestMethod]
//        [MyExpectedException(typeof(ValidationException),
//        "Portfolio not found")]
//        public void CanNotAddPositionInNonexistentPortfolio()
//        {
//            portfolioRepository.Setup(c => c.IsExist(It.IsAny<int>()))
//                .Returns((int i) => ListPortfolios.Any(c => c.Id == i));
//            UnitOfWork.Setup(m => m.Portfolios).Returns(portfolioRepository.Object);
//            UnitOfWork.Setup(m => m.Positions).Returns(positionRepository.Object);
//            positionService = new PositionService(UnitOfWork.Object, validateService, tradeSybolService.Object, calculationService, map);

//            positionService.AddPositionToPortfolio(new Position(), 11);
//        }

//        [TestMethod]
//        public void CanNotGetNotValidCoseDate()
//        {
//            symbolDividendRepository.Setup(c => c.Get(It.IsAny<int>()))
//                .Returns(new SymbolDividend { SymbolID = 39817, DividendAmount = 7.1191m });
//            tradeSybolService.Setup(c => c.GetPriceForDate(It.IsAny<DateTime>(), It.IsAny<int>()))
//                .Returns(0.66m);
//            tradeSybolService.Setup(c => c.GetMaxGainForSymbolBetweenDate(It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<int>(), It.IsAny<TradeTypesDTO>()))
//                .Returns(new TradeInforamation {Price = 2.491m });
//            UnitOfWork.Setup(m => m.Positions).Returns(positionRepository.Object);
//            UnitOfWork.Setup(m => m.SymbolDividends).Returns(symbolDividendRepository.Object);
//            positionService = new PositionService(UnitOfWork.Object, validateService, tradeSybolService.Object, calculationService, map);

//            var result = positionService.CalculateAllParams(new PositionDTO { Id = 5,
//                OpenWeight = 12, CloseDate = new DateTime(1, 1, 1, 0, 0, 0) });

//            Assert.IsTrue(result.CloseDate == null);
//        }
//    }
//}
