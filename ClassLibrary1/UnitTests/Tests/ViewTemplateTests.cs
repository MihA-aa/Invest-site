//using System;
//using System.Text;
//using System.Collections.Generic;
//using System.Linq;
//using AutoMapper;
//using BLL.DTO;
//using BLL.Infrastructure;
//using BLL.Interfaces;
//using BLL.Services;
//using DAL.Entities;
//using Entity = DAL.Entities;
//using DAL.Enums;
//using BLL.Helpers;
//using DAL.Interfaces;
//using DALEF.Repositories;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Moq;
//using UnitTests.Attributes;

//namespace UnitTests.Tests
//{
//    [TestClass]
//    public class ViewTemplateTests
//    {
//        private Mock<IUnitOfWork> UnitOfWork;
//        private Mock<IViewTemplateRepository> viewTemplateRepository;
//        private Mock<IViewTemplateColumnRepository> viewTemplateColumnRepository;
//        private Mock<IProfileRepository> profileRepository;
//        private Mock<ICustomerService> customerService;
//        private Mock<IValidateService> validateService;
//        private ViewTemplateService viewTemplateService;
//        private IMapper map;
//        List<ViewTemplate> ListViewTemplates;
//        List<ViewTemplateColumn> ListViewTemplateColumns;

//        #region ViewTemplateColumn Inizialize
//        ViewTemplateColumn viewTemplateColumn1 = new ViewTemplateColumn
//        {
//            Id = 1,
//            Name = "Name",
//            ViewTemplateId = 1,
//            DisplayIndex = 1,
//            ColumnId = 1,
//            ColumnFormatId = 3
//        };
//        ViewTemplateColumn viewTemplateColumn2 = new ViewTemplateColumn
//        {
//            Id = 2,
//            Name = "Symbol",
//            ViewTemplateId = 1,
//            DisplayIndex = 2,
//            ColumnId = 2,
//            ColumnFormatId = 1
//        };
//        #endregion
//        ViewTemplate viewTemplate1 = new ViewTemplate
//        {
//            Id = 1,
//            Name = "Preview all",
//            Positions = TemplatePositions.All,
//            ShowPortfolioStats = true,
//            SortOrder = Sorting.ASC
//        };

//        [TestInitialize]
//        public void Initialize()
//        {
//            #region ViewTemplate Inizialize
//            ViewTemplate viewTemplate2 = new ViewTemplate
//            {
//                Id = 2,
//                Name = "Default",
//                Positions = TemplatePositions.OpenOnly,
//                ShowPortfolioStats = false,
//                SortOrder = Sorting.DESC,
//                Columns = new List<ViewTemplateColumn> { viewTemplateColumn1, viewTemplateColumn2 }
//            };
//            #endregion

//            ListViewTemplateColumns = new List<ViewTemplateColumn> { viewTemplateColumn1, viewTemplateColumn2 };
//            ListViewTemplates = new List<ViewTemplate> { viewTemplate1, viewTemplate2 };
//            UnitOfWork = new Mock<IUnitOfWork>();
//            customerService = new Mock<ICustomerService>();
//            viewTemplateRepository = new Mock<IViewTemplateRepository>();
//            viewTemplateColumnRepository = new Mock<IViewTemplateColumnRepository>();
//            profileRepository = new Mock<IProfileRepository>();
//            validateService = new Mock<IValidateService>();
//            map = new AutoMapperConfiguration().Configure().CreateMapper();
//        }

//        [TestMethod]
//        public void CanGetAllViewTemplates()
//        {
//            viewTemplateRepository.Setup(m => m.GetAll()).Returns(ListViewTemplates);
//            UnitOfWork.Setup(m => m.ViewTemplates).Returns(viewTemplateRepository.Object);
//            viewTemplateService = new ViewTemplateService(UnitOfWork.Object, validateService.Object, map, customerService.Object);

//            var viewTemplates = viewTemplateService.GetViewTemplates().ToList();

//            Assert.IsTrue(viewTemplates.Count == 2);
//            Assert.AreEqual(viewTemplates[0].Name, "Preview all");
//            Assert.AreEqual(viewTemplates[1].Name, "Default");
//        }

//        [TestMethod]
//        public void CanGeViewTemplatesForUser()
//        {
//            profileRepository.Setup(c => c.Get(It.IsAny<string>()))
//                .Returns(new Entity.Profile { Customer = new Customer { ViewTemplates = new List<ViewTemplate> { new ViewTemplate { Id = 99, Name = "UserViewTemplate" } } } });
//            UnitOfWork.Setup(m => m.ViewTemplates).Returns(viewTemplateRepository.Object);
//            UnitOfWork.Setup(m => m.Profiles).Returns(profileRepository.Object);
//            viewTemplateService = new ViewTemplateService(UnitOfWork.Object, validateService.Object, map, customerService.Object);

//            var viewTemplates = viewTemplateService.GetViewTemplatesForUser("").ToList();

//            Assert.IsTrue(viewTemplates.Count == 1);
//            Assert.AreEqual(viewTemplates[0].Name, "UserViewTemplate");
//        }

//        [TestMethod]
//        [MyExpectedException(typeof(ValidationException),
//        "Profile not found")]
//        public void CanNotGeViewTemplatesForNonexistentUser()
//        {
//            profileRepository.Setup(c => c.Get(It.IsAny<string>())).Returns((Entity.Profile) null);
//            UnitOfWork.Setup(m => m.ViewTemplates).Returns(viewTemplateRepository.Object);
//            UnitOfWork.Setup(m => m.Profiles).Returns(profileRepository.Object);
//            viewTemplateService = new ViewTemplateService(UnitOfWork.Object, validateService.Object, map, customerService.Object);

//            viewTemplateService.GetViewTemplatesForUser("");
//        }

//        [TestMethod]
//        public void CanGetNameOfViewTemplateById()
//        {
//            viewTemplateRepository.Setup(c => c.Get(It.IsAny<int>()))
//                .Returns((int i) => ListViewTemplates.FirstOrDefault(c => c.Id == i));
//            UnitOfWork.Setup(m => m.ViewTemplates).Returns(viewTemplateRepository.Object);
//            viewTemplateService = new ViewTemplateService(UnitOfWork.Object, validateService.Object, map, customerService.Object);

//            var view1Name = viewTemplateService.GetNameByTemplateId(1);
//            var view2Name = viewTemplateService.GetNameByTemplateId(2);

//            Assert.AreEqual(view1Name, "Preview all");
//            Assert.AreEqual(view2Name, "Default");
//        }

//        [TestMethod]
//        [MyExpectedException(typeof(ValidationException),
//        "Not set id of ViewTemplate")]
//        public void CanNotGetNameOfViewTemplateByNullId()
//        {
//            UnitOfWork.Setup(m => m.ViewTemplates).Returns(viewTemplateRepository.Object);
//            viewTemplateService = new ViewTemplateService(UnitOfWork.Object, validateService.Object, map, customerService.Object);

//            viewTemplateService.GetNameByTemplateId(null);
//        }

//        [TestMethod]
//        [MyExpectedException(typeof(ValidationException),
//        "ViewTemplate not found")]
//        public void CanNotGetNonexistentViewTemplateNameByViewId()
//        {
//            viewTemplateRepository.Setup(c => c.Get(It.IsAny<int>()))
//                .Returns((int i) => ListViewTemplates.FirstOrDefault(c => c.Id == i));
//            UnitOfWork.Setup(m => m.ViewTemplates).Returns(viewTemplateRepository.Object);
//            viewTemplateService = new ViewTemplateService(UnitOfWork.Object, validateService.Object, map, customerService.Object);

//            viewTemplateService.GetNameByTemplateId(5);
//        }

//        [TestMethod]
//        public void CanGetViewTemplateColumnsById()
//        {
//            viewTemplateRepository.Setup(c => c.Get(It.IsAny<int>()))
//                .Returns((int i) => ListViewTemplates.FirstOrDefault(c => c.Id == i));
//            UnitOfWork.Setup(m => m.ViewTemplates).Returns(viewTemplateRepository.Object);
//            viewTemplateService = new ViewTemplateService(UnitOfWork.Object, validateService.Object, map, customerService.Object);

//            var columns1 = viewTemplateService.GetViewTemplateColumns(1);
//            var columns2 = viewTemplateService.GetViewTemplateColumns(2);

//            Assert.IsTrue(!columns1.Any());
//            Assert.IsTrue(columns2.Count() == 2);
//        }

//        [TestMethod]
//        [MyExpectedException(typeof(ValidationException),
//        "Not set id of ViewTemplate")]
//        public void CanNotGetViewTemplateColumnsByNullId()
//        {
//            UnitOfWork.Setup(m => m.ViewTemplates).Returns(viewTemplateRepository.Object);
//            viewTemplateService = new ViewTemplateService(UnitOfWork.Object, validateService.Object, map, customerService.Object);

//            viewTemplateService.GetViewTemplateColumns(null);
//        }
        
//        [TestMethod]
//        [MyExpectedException(typeof(ValidationException),
//        "ViewTemplate not found")]
//        public void CanNotGetColumnNonexistentViewTemplateByViewTemplateId()
//        {
//            viewTemplateRepository.Setup(c => c.Get(It.IsAny<int>()))
//                .Returns((int i) => ListViewTemplates.FirstOrDefault(c => c.Id == i));
//            UnitOfWork.Setup(m => m.ViewTemplates).Returns(viewTemplateRepository.Object);
//            viewTemplateService = new ViewTemplateService(UnitOfWork.Object, validateService.Object, map, customerService.Object);

//            viewTemplateService.GetViewTemplateColumns(5);
//        }

//        [TestMethod]
//        public void CanGetViewTemplateById()
//        {
//            viewTemplateRepository.Setup(c => c.Get(It.IsAny<int>()))
//                .Returns((int i) => ListViewTemplates.FirstOrDefault(c => c.Id == i));
//            UnitOfWork.Setup(m => m.ViewTemplates).Returns(viewTemplateRepository.Object);
//            viewTemplateService = new ViewTemplateService(UnitOfWork.Object, validateService.Object, map, customerService.Object);

//            var view1 = viewTemplateService.GetViewTemplate(1);
//            var view2 = viewTemplateService.GetViewTemplate(2);

//            Assert.AreEqual(view1.Name, "Preview all");
//            Assert.AreEqual(view2.Name, "Default");
//        }
        
//        [TestMethod]
//        [MyExpectedException(typeof(ValidationException),
//        "Not set id of ViewTemplate")]
//        public void CanNotGetViewTemplateByNullId()
//        {
//            UnitOfWork.Setup(m => m.ViewTemplates).Returns(viewTemplateRepository.Object);
//            viewTemplateService = new ViewTemplateService(UnitOfWork.Object, validateService.Object, map, customerService.Object);

//            viewTemplateService.GetViewTemplate(null);
//        }

//        [TestMethod]
//        [MyExpectedException(typeof(ValidationException),
//        "ViewTemplate not found")]
//        public void CanNotGetNonexistentViewTemplateByViewId()
//        {
//            viewTemplateRepository.Setup(c => c.Get(It.IsAny<int>()))
//                .Returns((int i) => ListViewTemplates.FirstOrDefault(c => c.Id == i));
//            UnitOfWork.Setup(m => m.ViewTemplates).Returns(viewTemplateRepository.Object);
//            viewTemplateService = new ViewTemplateService(UnitOfWork.Object, validateService.Object, map, customerService.Object);

//            viewTemplateService.GetViewTemplate(5);
//        }

//        [TestMethod]
//        public void CanDeleteViewTemplate()
//        {
//            viewTemplateRepository.Setup(c => c.IsExist(It.IsAny<int>()))
//                .Returns((int i) => ListViewTemplates.Any(c => c.Id == i));
//            viewTemplateRepository.Setup(m => m.Delete(It.IsAny<int>()))
//                .Callback<int>(i => ListViewTemplates.RemoveAll(c => c.Id == i));
//            UnitOfWork.Setup(m => m.ViewTemplates).Returns(viewTemplateRepository.Object);
//            viewTemplateService = new ViewTemplateService(UnitOfWork.Object, validateService.Object, map, customerService.Object);

//            viewTemplateService.DeleteViewTemplate(1);

//            Assert.IsTrue(ListViewTemplates.Count() == 1);
//        }

//        [TestMethod]
//        [MyExpectedException(typeof(ValidationException),
//        "Not set id of ViewTemplate")]
//        public void CanNotDeleteViewTemplateByNullId()
//        {
//            UnitOfWork.Setup(m => m.ViewTemplates).Returns(viewTemplateRepository.Object);
//            viewTemplateService = new ViewTemplateService(UnitOfWork.Object, validateService.Object, map, customerService.Object);

//            viewTemplateService.DeleteViewTemplate(null);
//        }

//        [TestMethod]
//        [MyExpectedException(typeof(ValidationException),
//        "ViewTemplate not found")]
//        public void CanNotDeleteNonexistViewTemplate()
//        {
//            viewTemplateRepository.Setup(c => c.Get(It.IsAny<int>()))
//                .Returns((int i) => ListViewTemplates.FirstOrDefault(c => c.Id == i));
//            UnitOfWork.Setup(m => m.ViewTemplates).Returns(viewTemplateRepository.Object);
//            viewTemplateService = new ViewTemplateService(UnitOfWork.Object, validateService.Object, map, customerService.Object);

//            viewTemplateService.DeleteViewTemplate(5);
//        }

//        [TestMethod]
//        public void CanCreateViewTemplateInCreateOrUpdate()
//        {
//            viewTemplateRepository.Setup(m => m.Create(It.IsAny<ViewTemplate>()))
//                .Callback<ViewTemplate>(ListViewTemplates.Add);
//            viewTemplateRepository.Setup(c => c.Get(It.IsAny<int>()))
//                .Returns((int i) => ListViewTemplates.FirstOrDefault(c => c.Id == i));
//            customerService.Setup(m => m.GetCustomerByProfileId(It.IsAny<string>()))
//                .Returns(new Customer { Id = 23123, Name = "Misha" });
//            UnitOfWork.Setup(m => m.ViewTemplates).Returns(viewTemplateRepository.Object);
//            viewTemplateService = new ViewTemplateService(UnitOfWork.Object, validateService.Object, map, customerService.Object);

//            viewTemplateService.CreateOrUpdateViewTemplate(new ViewTemplateDTO { Id=4}, "");

//            Assert.IsTrue(ListViewTemplates.Count() == 3);
//        }

//        [TestMethod]
//        public void CanUpdateViewTemplateInCreateOrUpdate()
//        {
//            viewTemplateRepository.Setup(c => c.IsExist(It.IsAny<int>()))
//                .Returns((int i) => ListViewTemplates.Any(c => c.Id == i));
//            viewTemplateRepository.Setup(m => m.Update(It.IsAny<ViewTemplate>())).Callback<ViewTemplate>(p =>
//            {
//                int index = ListViewTemplates.IndexOf(ListViewTemplates.FirstOrDefault(c => c.Id == p.Id));
//                ListViewTemplates[index] = p;
//            });
//            viewTemplateColumnRepository.Setup(c => c.Get(It.IsAny<int>()))
//                .Returns((int i) => ListViewTemplateColumns.FirstOrDefault(c => c.Id == i));
//            customerService.Setup(m => m.GetCustomerByProfileId(It.IsAny<string>()))
//                .Returns(new Customer { Id = 23123, Name = "Misha" });
//            UnitOfWork.Setup(m => m.ViewTemplates).Returns(viewTemplateRepository.Object);
//            UnitOfWork.Setup(m => m.ViewTemplateColumns).Returns(viewTemplateColumnRepository.Object);
//            viewTemplateService = new ViewTemplateService(UnitOfWork.Object, validateService.Object, map, customerService.Object);

//            viewTemplateService.CreateOrUpdateViewTemplate(new ViewTemplateDTO { Id = 1, Name = "Update Name", SortColumnId = 1 }, "");

//            Assert.IsTrue(ListViewTemplates.FirstOrDefault(c => c.Id == 1).Name == "Update Name");
//        }

//        [TestMethod]
//        [MyExpectedException(typeof(ValidationException),
//        "ViewTemplate is null reference")]
//        public void CanNotCreateOrUpdateNullReferenceViewTemplate()
//        {
//            UnitOfWork.Setup(m => m.ViewTemplates).Returns(viewTemplateRepository.Object);
//            viewTemplateService = new ViewTemplateService(UnitOfWork.Object, validateService.Object, map, customerService.Object);

//            viewTemplateService.CreateOrUpdateViewTemplate(null, "1");
//        }

//        [TestMethod]
//        public void CanCreateViewTemplate()
//        {
//            viewTemplateRepository.Setup(m => m.Create(It.IsAny<ViewTemplate>()))
//                .Callback<ViewTemplate>(ListViewTemplates.Add);
//            viewTemplateRepository.Setup(c => c.Get(It.IsAny<int>()))
//                .Returns((int i) => ListViewTemplates.FirstOrDefault(c => c.Id == i));
//            customerService.Setup(m => m.GetCustomerByProfileId(It.IsAny<string>()))
//                .Returns(new Customer { Id = 23123, Name = "Misha" });
//            UnitOfWork.Setup(m => m.ViewTemplates).Returns(viewTemplateRepository.Object);
//            viewTemplateService = new ViewTemplateService(UnitOfWork.Object, validateService.Object, map, customerService.Object);

//            viewTemplateService.CreateViewTemplate(new ViewTemplateDTO { }, "");

//            Assert.IsTrue(ListViewTemplates.Count() == 3);
//        }

//        [TestMethod]
//        [MyExpectedException(typeof(ValidationException),
//        "ViewTemplate is null reference")]
//        public void CanNotCreateNullReferenceViewTemplate()
//        {
//            UnitOfWork.Setup(m => m.ViewTemplates).Returns(viewTemplateRepository.Object);
//            viewTemplateService = new ViewTemplateService(UnitOfWork.Object, validateService.Object, map, customerService.Object);

//            viewTemplateService.CreateViewTemplate(null, "1");
//        }

//        [TestMethod]
//        public void CanUpdateViewTemplate()
//        {
//            viewTemplateRepository.Setup(c => c.IsExist(It.IsAny<int>()))
//                .Returns((int i) => ListViewTemplates.Any(c => c.Id == i));
//            viewTemplateRepository.Setup(m => m.Update(It.IsAny<ViewTemplate>())).Callback<ViewTemplate>(p =>
//            {
//                int index = ListViewTemplates.IndexOf(ListViewTemplates.FirstOrDefault(c => c.Id == p.Id));
//                ListViewTemplates[index] = p;
//            });
//            viewTemplateColumnRepository.Setup(c => c.Get(It.IsAny<int>()))
//                .Returns((int i) => ListViewTemplateColumns.FirstOrDefault(c => c.Id == i));
//            customerService.Setup(m => m.GetCustomerByProfileId(It.IsAny<string>()))
//                .Returns(new Customer { Id = 23123, Name = "Misha" });
//            UnitOfWork.Setup(m => m.ViewTemplates).Returns(viewTemplateRepository.Object);
//            UnitOfWork.Setup(m => m.ViewTemplateColumns).Returns(viewTemplateColumnRepository.Object);
//            viewTemplateService = new ViewTemplateService(UnitOfWork.Object, validateService.Object, map, customerService.Object);

//            viewTemplateService.UpdateViewTemplate(new ViewTemplateDTO { Id = 1, Name = "Update Name", SortColumnId = 1});

//            Assert.IsTrue(ListViewTemplates.FirstOrDefault(c => c.Id == 1).Name == "Update Name");
//        }

//        [TestMethod]
//        [MyExpectedException(typeof(ValidationException),
//        "ViewTemplate is null reference")]
//        public void CanNotUpdateNullReferencePosition()
//        {
//            UnitOfWork.Setup(m => m.ViewTemplates).Returns(viewTemplateRepository.Object);
//            viewTemplateService = new ViewTemplateService(UnitOfWork.Object, validateService.Object, map, customerService.Object);

//            viewTemplateService.UpdateViewTemplate(null);
//        }


//        [TestMethod]
//        [MyExpectedException(typeof(ValidationException),
//        "ViewTemplate not found")]
//        public void CanNotUpdateNonexistentPosition()
//        {
//            viewTemplateRepository.Setup(c => c.IsExist(It.IsAny<int>()))
//                .Returns((int i) => ListViewTemplates.Any(c => c.Id == i));
//            UnitOfWork.Setup(m => m.ViewTemplates).Returns(viewTemplateRepository.Object);
//            viewTemplateService = new ViewTemplateService(UnitOfWork.Object, validateService.Object, map, customerService.Object);

//            viewTemplateService.UpdateViewTemplate(new ViewTemplateDTO { Id = 6, Name = "Update Name"});
//        }

//        [TestMethod]
//        public void CanAddSortColumnToTemplate()
//        {
//            viewTemplateColumnRepository.Setup(c => c.Get(It.IsAny<int>()))
//                .Returns((int i) => ListViewTemplateColumns.FirstOrDefault(c => c.Id == i));
//            UnitOfWork.Setup(m => m.ViewTemplateColumns).Returns(viewTemplateColumnRepository.Object);
//            UnitOfWork.Setup(m => m.ViewTemplates).Returns(viewTemplateRepository.Object);
//            viewTemplateService = new ViewTemplateService(UnitOfWork.Object, validateService.Object, map, customerService.Object);

//            viewTemplateService.AddSortColumnToTemplate(viewTemplate1, 1);

//            Assert.IsTrue(ListViewTemplates.FirstOrDefault(c => c.Id == 1).SortColumn.Id == 1);
//        }

//        [TestMethod]
//        [MyExpectedException(typeof(ValidationException),
//        "ViewTemplate is null reference")]
//        public void CanNotAddNullReferenceViewTemplateToColumn()
//        {
//            UnitOfWork.Setup(m => m.ViewTemplates).Returns(viewTemplateRepository.Object);
//            viewTemplateService = new ViewTemplateService(UnitOfWork.Object, validateService.Object, map, customerService.Object);

//            viewTemplateService.AddSortColumnToTemplate(null, 1);
//        }

//        [TestMethod]
//        public void CanNotAddViewTemplateWithNotSetIdOfColumn()
//        {
//            UnitOfWork.Setup(m => m.ViewTemplates).Returns(viewTemplateRepository.Object);
//            viewTemplateService = new ViewTemplateService(UnitOfWork.Object, validateService.Object, map, customerService.Object);

//            viewTemplateService.AddSortColumnToTemplate(new ViewTemplate(), null);
//        }

//        [TestMethod]
//        [MyExpectedException(typeof(ValidationException),
//        "ViewTemplateColumn not found")]
//        public void CanNotAddViewInNonexistentPortfolio()
//        {
//            viewTemplateColumnRepository.Setup(c => c.IsExist(It.IsAny<int>()))
//                .Returns((int i) => ListViewTemplateColumns.Any(c => c.Id == i));
//            UnitOfWork.Setup(m => m.ViewTemplateColumns).Returns(viewTemplateColumnRepository.Object);
//            UnitOfWork.Setup(m => m.ViewTemplates).Returns(viewTemplateRepository.Object);
//            viewTemplateService = new ViewTemplateService(UnitOfWork.Object, validateService.Object, map, customerService.Object);

//            viewTemplateService.AddSortColumnToTemplate(new ViewTemplate(), 11);
//        }
//    }
//}
