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
using BLL.Infrastructure;
using UnitTests.Attributes;

namespace UnitTests.Tests
{
    [TestClass]
    public class PositionTests
    {
        private Mock<IUnitOfWork> UnitOfWork;
        private PositionService positionService;
        private ValidateService validateService;
        private Mock<IRepository<Position>> positionRepository;
        List<Position> ListPositions;

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
            ListPositions = new List<Position> { position1, position2, position3 };
            UnitOfWork = new Mock<IUnitOfWork>();
            positionRepository = new Mock<IRepository<Position>>();
            validateService = new ValidateService();
        }

        [TestMethod]
        public void CanGetAllPositions()
        {
            positionRepository.Setup(m => m.GetAll()).Returns(ListPositions);
            UnitOfWork.Setup(m => m.Positions).Returns(positionRepository.Object);
            positionService = new PositionService(UnitOfWork.Object, validateService);

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
            positionRepository.Setup(c => c.Get(It.IsAny<int>())).Returns((int i) => ListPositions.FirstOrDefault(c => c.Id == i));
            UnitOfWork.Setup(m => m.Positions).Returns(positionRepository.Object);
            positionService = new PositionService(UnitOfWork.Object, validateService);

            PositionDTO position1 = positionService.GetPosition(1);
            PositionDTO position2 = positionService.GetPosition(2);
            PositionDTO position3 = positionService.GetPosition(3);

            Assert.AreEqual(position1.Name, "Pulse Biosciences CS");
            Assert.AreEqual(position2.Name, "Witwatersrand Gold Rsrcs Ltd");
            Assert.AreEqual(position3.Name, "AAT Corporation Limited");
        }

        [TestMethod]
        [MyExpectedException(typeof(ValidationException),
         "Not set id of position")]
        public void CanNotGetPositionByNullId()
        {
            UnitOfWork.Setup(m => m.Positions).Returns(positionRepository.Object);
            positionService = new PositionService(UnitOfWork.Object, validateService);

            positionService.GetPosition(null);
        }

        [TestMethod]
        [MyExpectedException(typeof(ValidationException),
         "Position not found")]
        public void CanNotGetNonexistentPositionByPositionId()
        {
            positionRepository.Setup(c => c.Get(It.IsAny<int>()))
                .Returns((int i) => ListPositions.FirstOrDefault(c => c.Id == i));
            UnitOfWork.Setup(m => m.Positions).Returns(positionRepository.Object);
            positionService = new PositionService(UnitOfWork.Object, validateService);

            positionService.GetPosition(5);
        }
        [TestMethod]
        public void CanCreatePosition()
        {
            positionRepository.Setup(m => m.Create(It.IsAny<Position>())).Callback<Position>(ListPositions.Add); ;
            UnitOfWork.Setup(m => m.Positions).Returns(positionRepository.Object);
            positionService = new PositionService(UnitOfWork.Object, validateService);
            
            positionService.CreatePosition(newPosition);

            Assert.IsTrue(ListPositions.Count() == 4);
        }

        [TestMethod]
        [MyExpectedException(typeof(ValidationException),
            "Open Weight of position cannot be less than zero")]
        public void CanNotCreatePositionWithOpenWeightLessThanZero()
        {
            UnitOfWork.Setup(m => m.Positions).Returns(positionRepository.Object);
            positionService = new PositionService(UnitOfWork.Object, validateService);
            
            positionService.CreatePosition(new PositionDTO { OpenWeight = -1 });
        }
        [TestMethod]
        public void CanDeletePosition()
        {
            positionRepository.Setup(c => c.Get(It.IsAny<int>()))
                .Returns((int i) => ListPositions.FirstOrDefault(c => c.Id == i));
            positionRepository.Setup(m => m.Delete(It.IsAny<int>()))
                .Callback<int>(i => ListPositions.RemoveAll(c => c.Id == i));
            UnitOfWork.Setup(m => m.Positions).Returns(positionRepository.Object);
            positionService = new PositionService(UnitOfWork.Object, validateService);

            positionService.DeletePosition(1);

            Assert.IsTrue(ListPositions.Count() == 2);
        }

        [TestMethod]
        [MyExpectedException(typeof(ValidationException),
         "Not set id of position")]
        public void CanNotDeletePositionByNullId()
        {
            UnitOfWork.Setup(m => m.Positions).Returns(positionRepository.Object);
            positionService = new PositionService(UnitOfWork.Object, validateService);

            positionService.DeletePosition(null);
        }

        [TestMethod]
        [MyExpectedException(typeof(ValidationException),
         "Position not found")]
        public void CanNotDeleteNonexistPosition()
        {
            positionRepository.Setup(c => c.Get(It.IsAny<int>()))
                .Returns((int i) => ListPositions.FirstOrDefault(c => c.Id == i));
            UnitOfWork.Setup(m => m.Positions).Returns(positionRepository.Object);
            positionService = new PositionService(UnitOfWork.Object, validateService);

            positionService.DeletePosition(5);
        }

        [TestMethod]
        public void CanUpdatePosition()
        {
            positionRepository.Setup(c => c.Get(It.IsAny<int>()))
                .Returns((int i) => ListPositions.FirstOrDefault(c => c.Id == i));
            positionRepository.Setup(m => m.Update(It.IsAny<Position>())).Callback<Position>(p =>
            {
                int index = ListPositions.IndexOf(ListPositions.FirstOrDefault(c => c.Id == p.Id));
                ListPositions[index] = p;
            });
           UnitOfWork.Setup(m => m.Positions).Returns(positionRepository.Object);
           positionService = new PositionService(UnitOfWork.Object, validateService);

            #region
            PositionDTO updatePosition = new PositionDTO
            {
                Id = 1,
                SymbolId = 3,
                SymbolType = Symbols.Option,
                SymbolName = "PLSE",
                Name = "New update position",
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
            positionService.UpdatePosition(updatePosition);

            Assert.IsTrue(ListPositions.FirstOrDefault(c => c.Id == 1).Name == "New update position");
        }

        [TestMethod]
        [MyExpectedException(typeof(ValidationException),
         "Open Weight of position cannot be less than zero")]
        public void CanNotUpdateePositionWithOpenWeightLessThanZero()
        {
            UnitOfWork.Setup(m => m.Positions).Returns(positionRepository.Object);
            positionService = new PositionService(UnitOfWork.Object, validateService);

            positionService.UpdatePosition(new PositionDTO { OpenWeight = -1 });
        }

        [TestMethod]
        [MyExpectedException(typeof(ValidationException),
         "Position is null reference")]
        public void CanNotUpdateNullReferencePosition()
        {
            UnitOfWork.Setup(m => m.Positions).Returns(positionRepository.Object);
            positionService = new PositionService(UnitOfWork.Object, validateService);

            positionService.UpdatePosition(null);
        }

        [TestMethod]
        [MyExpectedException(typeof(ValidationException),
         "Position not found")]
        public void CanNotUpdateeNonexistPosition()
        {
            positionRepository.Setup(c => c.Get(It.IsAny<int>()))
                .Returns((int i) => ListPositions.FirstOrDefault(c => c.Id == i));
            UnitOfWork.Setup(m => m.Positions).Returns(positionRepository.Object);
            positionService = new PositionService(UnitOfWork.Object, validateService);

            positionService.UpdatePosition(new PositionDTO { Id = 5 });
        }
    }
}
