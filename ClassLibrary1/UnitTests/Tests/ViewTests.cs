//using System;
//using System.Collections.Generic;
//using System.Linq;
//using AutoMapper;
//using BLL.DTO;
//using BLL.Infrastructure;
//using BLL.Interfaces;
//using BLL.Services;
//using DAL.Entities;
//using DAL.Enums;
//using DAL.Interfaces;
//using Entity = DAL.Entities;
//using BLL.Helpers;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Moq;
//using UnitTests.Attributes;

//namespace UnitTests.Tests
//{
//    [TestClass]
//    public class ViewTests
//    {
//        private Mock<IUnitOfWork> UnitOfWork;
//        private Mock<IViewRepository> viewRepository;
//        private Mock<IViewTemplateRepository> viewTemplateRepository;
//        private Mock<IPortfolioRepository> portfolioRepository;
//        private Mock<ICustomerService> customerService;
//        private Mock<IProfileRepository> profileRepository;
//        private ViewService viewService;
//        private ValidateService validateService;
//        private IMapper map;
//        List<View> ListViews;
//        List<ViewTemplate> ListViewTemplates;
//        List<Portfolio> ListPortfoios;

//        #region ViewTemplate Inizialize
//        ViewTemplate viewTemplate1 = new ViewTemplate
//        {
//            Id = 1,
//            Name = "Preview all",
//            Positions = TemplatePositions.All,
//            ShowPortfolioStats = true,
//            SortOrder = Sorting.ASC
//        };

//        ViewTemplate viewTemplate2 = new ViewTemplate
//        {
//            Id = 2,
//            Name = "Default",
//            Positions = TemplatePositions.OpenOnly,
//            ShowPortfolioStats = false,
//            SortOrder = Sorting.DESC
//        };
//        #endregion

//        #region Portfolio Inizialize
//        Portfolio portfolio1 = new Portfolio
//        {
//            Id = 1,
//            Name = "HDFC Bank",
//            DisplayIndex = 4,
//            LastUpdateDate = new DateTime(2017, 3, 12),
//            Visibility = true,
//            Quantity = 3,
//            PercentWins = 93.23m,
//            BiggestWinner = 534.32m,
//            BiggestLoser = 123.46m,
//            AvgGain = 316.65m,
//            MonthAvgGain = 341.436m,
//            PortfolioValue = 5532.42m,
//        };
//        Portfolio portfolio2 = new Portfolio
//        {
//            Id = 2,
//            Name = "IndusInd Bank",
//            DisplayIndex = 5,
//            LastUpdateDate = new DateTime(2017, 3, 12),
//            Visibility = true,
//            Quantity = 3,
//            PercentWins = 93.23m,
//            BiggestWinner = 534.32m,
//            BiggestLoser = 123.46m,
//            AvgGain = 316.65m,
//            MonthAvgGain = 341.436m,
//            PortfolioValue = 5532.42m,
//        };
//        #endregion

//        [TestInitialize]
//        public void Initialize()
//        {
//            #region View Inizialize

//            View previewAllView = new View
//            {
//                Id = 1,
//                Name = "Preview All View",
//                ShowName = true,
//                DateFormat = DateFormats.MonthDayYear,
//                MoneyPrecision = 2,
//                PercentyPrecision = 4,
//                ViewTemplate = viewTemplate1,
//                ViewTemplateId = 1
//            };

//            View defaultView = new View
//            {
//                Id = 2,
//                Name = "Default View",
//                ShowName = false,
//                DateFormat = DateFormats.DayMonthNameYear,
//                MoneyPrecision = 1,
//                PercentyPrecision = 2,
//                ViewTemplate = viewTemplate2,
//                ViewTemplateId = 2
//            };
//            #endregion
//            ListViewTemplates = new List<ViewTemplate> { viewTemplate1, viewTemplate2 };
//            ListViews = new List<View> { previewAllView, defaultView };
//            ListPortfoios = new List<Portfolio> {portfolio1, portfolio2};
//            UnitOfWork = new Mock<IUnitOfWork>();
//            viewRepository = new Mock<IViewRepository>();
//            validateService = new ValidateService();
//            customerService = new Mock<ICustomerService>();
//            viewTemplateRepository = new Mock<IViewTemplateRepository>();
//            portfolioRepository = new Mock<IPortfolioRepository>();
//            profileRepository = new Mock<IProfileRepository>();
//            map = new AutoMapperConfiguration().Configure().CreateMapper();
//        }

//        [TestMethod]
//        public void CanGetAllViews()
//        {
//            viewRepository.Setup(m => m.GetAll()).Returns(ListViews);
//            UnitOfWork.Setup(m => m.Views).Returns(viewRepository.Object);
//            viewService = new ViewService(UnitOfWork.Object, validateService, map, customerService.Object);
            
//            var views = viewService.GetViews().ToList();
            
//            Assert.IsTrue(views.Count == 2);
//            Assert.AreEqual(views[0].Name, "Preview All View");
//            Assert.AreEqual(views[1].Name, "Default View");
//        }

//        [TestMethod]
//        public void CanGetViewById()
//        {
//            viewRepository.Setup(c => c.Get(It.IsAny<int>()))
//                .Returns((int i) => ListViews.FirstOrDefault(c => c.Id == i));
//            UnitOfWork.Setup(m => m.Views).Returns(viewRepository.Object);
//            viewService = new ViewService(UnitOfWork.Object, validateService, map, customerService.Object);

//            var view1 = viewService.GetView(1);
//            var view2 = viewService.GetView(2);

//            Assert.AreEqual(view1.Name, "Preview All View");
//            Assert.AreEqual(view2.Name, "Default View");
//        }

//        [TestMethod]
//        public void CanGeViewsForUser()
//        {
//            profileRepository.Setup(c => c.Get(It.IsAny<string>()))
//                .Returns(new Entity.Profile { Customer = new Customer { Views = new List<View> { new View { Id = 99, Name = "UserView" } } } });
//            UnitOfWork.Setup(m => m.ViewTemplates).Returns(viewTemplateRepository.Object);
//            UnitOfWork.Setup(m => m.Profiles).Returns(profileRepository.Object);
//            viewService = new ViewService(UnitOfWork.Object, validateService, map, customerService.Object);

//            var views = viewService.GetViewsForUser("").ToList();

//            Assert.IsTrue(views.Count == 1);
//            Assert.AreEqual(views[0].Name, "UserView");
//        }

//        [TestMethod]
//        [MyExpectedException(typeof(ValidationException),
//        "Profile not found")]
//        public void CanNotGeViewTemplatesForNonexistentUser()
//        {
//            profileRepository.Setup(c => c.Get(It.IsAny<string>())).Returns((Entity.Profile)null);
//            UnitOfWork.Setup(m => m.ViewTemplates).Returns(viewTemplateRepository.Object);
//            UnitOfWork.Setup(m => m.Profiles).Returns(profileRepository.Object);
//            viewService = new ViewService(UnitOfWork.Object, validateService, map, customerService.Object);

//            viewService.GetViewsForUser("");
//        }

//        [TestMethod]
//        [MyExpectedException(typeof(ValidationException),
//        "Not set id of view")]
//        public void CanNotGetViewByNullId()
//        {
//            UnitOfWork.Setup(m => m.Views).Returns(viewRepository.Object);
//            viewService = new ViewService(UnitOfWork.Object, validateService, map, customerService.Object);

//            viewService.GetView(null);
//        }

//        [TestMethod]
//        [MyExpectedException(typeof(ValidationException),
//        "View not found")]
//        public void CanNotGetNonexistentViewByViewId()
//        {
//            viewRepository.Setup(c => c.Get(It.IsAny<int>()))
//                .Returns((int i) => ListViews.FirstOrDefault(c => c.Id == i));
//            UnitOfWork.Setup(m => m.Views).Returns(viewRepository.Object);
//            viewService = new ViewService(UnitOfWork.Object, validateService, map, customerService.Object);

//            viewService.GetView(5);
//        }

//        [TestMethod]
//        public void CanDeleteView()
//        {
//            viewRepository.Setup(c => c.IsExist(It.IsAny<int>()))
//                .Returns((int i) => ListViews.Any(c => c.Id == i));
//            viewRepository.Setup(m => m.Delete(It.IsAny<int>()))
//                .Callback<int>(i => ListViews.RemoveAll(c => c.Id == i));
//            UnitOfWork.Setup(m => m.Views).Returns(viewRepository.Object);
//            viewService = new ViewService(UnitOfWork.Object, validateService, map, customerService.Object);

//            viewService.DeleteView(1);

//            Assert.IsTrue(ListViews.Count() == 1);
//        }

//        [TestMethod]
//        [MyExpectedException(typeof(ValidationException),
//        "Not set id of view")]
//        public void CanNotDeleteViewByNullId()
//        {
//            UnitOfWork.Setup(m => m.Views).Returns(viewRepository.Object);
//            viewService = new ViewService(UnitOfWork.Object, validateService, map, customerService.Object);

//            viewService.DeleteView(null);
//        }

//        [TestMethod]
//        [MyExpectedException(typeof(ValidationException),
//        "View not found")]
//        public void CanNotDeleteNonexistView()
//        {
//            viewRepository.Setup(c => c.Get(It.IsAny<int>()))
//                .Returns((int i) => ListViews.FirstOrDefault(c => c.Id == i));
//            UnitOfWork.Setup(m => m.Views).Returns(viewRepository.Object);
//            viewService = new ViewService(UnitOfWork.Object, validateService, map, customerService.Object);

//            viewService.DeleteView(5);
//        }

//        [TestMethod]
//        public void CanCreateViewInCreateOrUpdate()
//        {
//            viewRepository.Setup(m => m.Create(It.IsAny<View>()))
//                .Callback<View>(ListViews.Add);
//            viewTemplateRepository.Setup(c => c.Get(It.IsAny<int>()))
//                .Returns((int i) => ListViewTemplates.FirstOrDefault(c => c.Id == i));
//            customerService.Setup(m => m.GetCustomerByProfileId(It.IsAny<string>()))
//                .Returns(new Customer { Id = 23123, Name = "Misha" });
//            UnitOfWork.Setup(m => m.Views).Returns(viewRepository.Object);
//            UnitOfWork.Setup(m => m.ViewTemplates).Returns(viewTemplateRepository.Object);
//            viewService = new ViewService(UnitOfWork.Object, validateService, map, customerService.Object);

//            viewService.CreateOrUpdateView(new ViewDTO { ViewTemplateId = 1 }, "");

//            Assert.IsTrue(ListViews.Count() == 3);
//        }

//        [TestMethod]
//        public void CanUpdateViewInCreateOrUpdate()
//        {
//            viewRepository.Setup(c => c.IsExist(It.IsAny<int>()))
//                .Returns((int i) => ListViews.Any(c => c.Id == i));
//            viewRepository.Setup(m => m.Update(It.IsAny<View>())).Callback<View>(p =>
//            {
//                int index = ListViews.IndexOf(ListViews.FirstOrDefault(c => c.Id == p.Id));
//                ListViews[index] = p;
//            });
//            viewTemplateRepository.Setup(c => c.Get(It.IsAny<int>()))
//                .Returns((int i) => ListViewTemplates.FirstOrDefault(c => c.Id == i));
//            customerService.Setup(m => m.GetCustomerByProfileId(It.IsAny<string>()))
//                .Returns(new Customer { Id = 23123, Name = "Misha" });
//            UnitOfWork.Setup(m => m.Views).Returns(viewRepository.Object);
//            UnitOfWork.Setup(m => m.ViewTemplates).Returns(viewTemplateRepository.Object);
//            viewService = new ViewService(UnitOfWork.Object, validateService, map, customerService.Object);

//            viewService.CreateOrUpdateView(new ViewDTO { Id = 1, Name = "Update Name", ViewTemplateId = 1 }, "");

//            Assert.IsTrue(ListViews.FirstOrDefault(c => c.Id == 1).Name == "Update Name");
//        }

//        [TestMethod]
//        [MyExpectedException(typeof(ValidationException),
//        "View is null reference")]
//        public void CanNotCreateNullReferenceViewInCreateOrUpdate()
//        {
//            UnitOfWork.Setup(m => m.Views).Returns(viewRepository.Object);
//            viewService = new ViewService(UnitOfWork.Object, validateService, map, customerService.Object);

//            viewService.CreateOrUpdateView(null, "1");
//        }

//        [TestMethod]
//        public void CanCreateView()
//        {
//            viewRepository.Setup(m => m.Create(It.IsAny<View>()))
//                .Callback<View>(ListViews.Add);
//            viewTemplateRepository.Setup(c => c.Get(It.IsAny<int>()))
//                .Returns((int i) => ListViewTemplates.FirstOrDefault(c => c.Id == i));
//            customerService.Setup(m => m.GetCustomerByProfileId(It.IsAny<string>()))
//                .Returns(new Customer { Id = 23123, Name = "Misha" });
//            UnitOfWork.Setup(m => m.Views).Returns(viewRepository.Object);
//            UnitOfWork.Setup(m => m.ViewTemplates).Returns(viewTemplateRepository.Object);
//            viewService = new ViewService(UnitOfWork.Object, validateService, map, customerService.Object);

//            viewService.CreateView(new ViewDTO {ViewTemplateId = 1}, "");

//            Assert.IsTrue(ListViews.Count() == 3);
//        }

//        [TestMethod]
//        [MyExpectedException(typeof(ValidationException),
//        "View is null reference")]
//        public void CanNotCreateNullReferenceView()
//        {
//            UnitOfWork.Setup(m => m.Views).Returns(viewRepository.Object);
//            viewService = new ViewService(UnitOfWork.Object, validateService, map, customerService.Object);

//            viewService.CreateView(null, "1");
//        }

//        [TestMethod]
//        public void CanUpdateView()
//        {
//            viewRepository.Setup(c => c.IsExist(It.IsAny<int>()))
//                .Returns((int i) => ListViews.Any(c => c.Id == i));
//            viewRepository.Setup(m => m.Update(It.IsAny<View>())).Callback<View>(p =>
//            {
//                int index = ListViews.IndexOf(ListViews.FirstOrDefault(c => c.Id == p.Id));
//                ListViews[index] = p;
//            });
//            viewTemplateRepository.Setup(c => c.Get(It.IsAny<int>()))
//                .Returns((int i) => ListViewTemplates.FirstOrDefault(c => c.Id == i));
//            customerService.Setup(m => m.GetCustomerByProfileId(It.IsAny<string>()))
//                .Returns(new Customer { Id = 23123, Name = "Misha" });
//            UnitOfWork.Setup(m => m.Views).Returns(viewRepository.Object);
//            UnitOfWork.Setup(m => m.ViewTemplates).Returns(viewTemplateRepository.Object);
//            viewService = new ViewService(UnitOfWork.Object, validateService, map, customerService.Object);

//            viewService.UpdateView(new ViewDTO { Id = 1, Name = "Update Name", ViewTemplateId = 1 });

//            Assert.IsTrue(ListViews.FirstOrDefault(c => c.Id == 1).Name == "Update Name");
//        }

//        [TestMethod]
//        [MyExpectedException(typeof(ValidationException),
//        "View is null reference")]
//        public void CanNotUpdateNullReferencePosition()
//        {
//            UnitOfWork.Setup(m => m.Views).Returns(viewRepository.Object);
//            viewService = new ViewService(UnitOfWork.Object, validateService, map, customerService.Object);

//            viewService.UpdateView(null);
//        }

//        [TestMethod]
//        [MyExpectedException(typeof(ValidationException),
//        "View not found")]
//        public void CanNotUpdateNonexistentPosition()
//        {
//            viewRepository.Setup(c => c.IsExist(It.IsAny<int>()))
//                .Returns((int i) => ListViews.Any(c => c.Id == i));
//            UnitOfWork.Setup(m => m.Views).Returns(viewRepository.Object);
//            viewService = new ViewService(UnitOfWork.Object, validateService, map, customerService.Object);

//            viewService.UpdateView(new ViewDTO { Id = 6, Name = "Update Name", ViewTemplateId = 1 });
//        }

//        public void CanAddViewTemplateToView()
//        {
//            viewTemplateRepository.Setup(c => c.Get(It.IsAny<int>()))
//                .Returns((int i) => ListViewTemplates.FirstOrDefault(c => c.Id == i));
//            UnitOfWork.Setup(m => m.Views).Returns(viewRepository.Object);
//            UnitOfWork.Setup(m => m.ViewTemplates).Returns(viewTemplateRepository.Object);
//            viewService = new ViewService(UnitOfWork.Object, validateService, map, customerService.Object);

//            viewService.AddViewTemplateToView(new View{ ViewTemplateId = 1 },1);

//            Assert.IsTrue(ListViews.FirstOrDefault(c => c.Id == 1).ViewTemplate.Id == 1);
//        }

//        [TestMethod]
//        [MyExpectedException(typeof(ValidationException),
//        "View is null reference")]
//        public void CanNotAddNullReferenceViewToViewTemplate()
//        {
//            UnitOfWork.Setup(m => m.Views).Returns(viewRepository.Object);
//            viewService = new ViewService(UnitOfWork.Object, validateService, map, customerService.Object);

//            viewService.AddViewTemplateToView(null, 1);
//        }

//        [TestMethod]
//        [MyExpectedException(typeof(ValidationException),
//        "Not set id of ViewTemplate")]
//        public void CanNotAddViewWithNotSetIdOfViewTemplate()
//        {
//            UnitOfWork.Setup(m => m.Views).Returns(viewRepository.Object);
//            viewService = new ViewService(UnitOfWork.Object, validateService, map, customerService.Object);

//            viewService.AddViewTemplateToView(new View(), null);
//        }

//        [TestMethod]
//        [MyExpectedException(typeof(ValidationException),
//        "ViewTemplate not found")]
//        public void CanNotAddPositionInNonexistentPortfolio()
//        {
//            viewTemplateRepository.Setup(c => c.IsExist(It.IsAny<int>()))
//                .Returns((int i) => ListViewTemplates.Any(c => c.Id == i));
//            UnitOfWork.Setup(m => m.ViewTemplates).Returns(viewTemplateRepository.Object);
//            UnitOfWork.Setup(m => m.Views).Returns(viewRepository.Object);
//            viewService = new ViewService(UnitOfWork.Object, validateService, map, customerService.Object);

//            viewService.AddViewTemplateToView(new View(), 11);
//        }

//        public void CanAddPortfolioToView()
//        {
//            viewRepository.Setup(m => m.Create(It.IsAny<View>()))
//                           .Callback<View>(ListViews.Add);
//            portfolioRepository.Setup(c => c.Get(It.IsAny<int>()))
//                .Returns((int i) => ListPortfoios.FirstOrDefault(c => c.Id == i));
//            UnitOfWork.Setup(m => m.Views).Returns(viewRepository.Object);
//            UnitOfWork.Setup(m => m.Portfolios).Returns(portfolioRepository.Object);
//            viewService = new ViewService(UnitOfWork.Object, validateService, map, customerService.Object);

//            viewService.AddPortfolioToView(new View { ViewTemplateId = 1 }, 1);

//            Assert.IsTrue(ListViews.FirstOrDefault(c => c.Id == 1).Portfolio.Id == 1);
//        }


//        [TestMethod]
//        [MyExpectedException(typeof(ValidationException),
//        "View is null reference")]
//        public void CanNotAddNullReferenceViewToPortfolio()
//        {
//            UnitOfWork.Setup(m => m.Views).Returns(viewRepository.Object);
//            viewService = new ViewService(UnitOfWork.Object, validateService, map, customerService.Object);

//            viewService.AddPortfolioToView(null, 1);
//        }

//        [TestMethod]
//        [MyExpectedException(typeof(ValidationException),
//        "Not set id of Portfolio")]
//        public void CanNotAddViewWithNotSetIdOfPortfolio()
//        {
//            UnitOfWork.Setup(m => m.Views).Returns(viewRepository.Object);
//            viewService = new ViewService(UnitOfWork.Object, validateService, map, customerService.Object);

//            viewService.AddPortfolioToView(new View(), null);
//        }

//        [TestMethod]
//        [MyExpectedException(typeof(ValidationException),
//        "Portfolio not found")]
//        public void CanNotAddViewInNonexistentPortfolio()
//        {
//            portfolioRepository.Setup(c => c.IsExist(It.IsAny<int>()))
//                .Returns((int i) => ListPortfoios.Any(c => c.Id == i));
//            UnitOfWork.Setup(m => m.Portfolios).Returns(portfolioRepository.Object);
//            UnitOfWork.Setup(m => m.Views).Returns(viewRepository.Object);
//            viewService = new ViewService(UnitOfWork.Object, validateService, map, customerService.Object);

//            viewService.AddPortfolioToView(new View(), 11);
//        }

//    }
//}
