using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BLL.DTO;
using BLL.Infrastructure;
using BLL.Interfaces;
using BLL.Services;
using DAL.Entities;
using DAL.Enums;
using BLL.Helpers;
using DAL.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using UnitTests.Attributes;

namespace UnitTests.Tests
{
    [TestClass]
    public class ViewTemplateTests
    {
        private Mock<IUnitOfWork> UnitOfWork;
        private Mock<IViewTemplateRepository> viewTemplateRepository;
        private Mock<ICustomerService> customerService;
        private ViewTemplateService viewTemplateService;
        private IMapper map;
        List<ViewTemplate> ListViewTemplates;
        #region ViewTemplate Inizialize
        ViewTemplate viewTemplate1 = new ViewTemplate
        {
            Id = 1,
            Name = "Preview all",
            Positions = TemplatePositions.All,
            ShowPortfolioStats = true,
            SortOrder = Sorting.ASC
        };

        ViewTemplate viewTemplate2 = new ViewTemplate
        {
            Id = 2,
            Name = "Default",
            Positions = TemplatePositions.OpenOnly,
            ShowPortfolioStats = false,
            SortOrder = Sorting.DESC
        };
        #endregion

        [TestInitialize]
        public void Initialize()
        {
            ListViewTemplates = new List<ViewTemplate> { viewTemplate1, viewTemplate2 };
            UnitOfWork = new Mock<IUnitOfWork>();
            customerService = new Mock<ICustomerService>();
            viewTemplateRepository = new Mock<IViewTemplateRepository>();
            map = new AutoMapperConfiguration().Configure().CreateMapper();
        }

        [TestMethod]
        public void CanGetAllViewTemplates()
        {
            viewTemplateRepository.Setup(m => m.GetAll()).Returns(ListViewTemplates);
            UnitOfWork.Setup(m => m.ViewTemplates).Returns(viewTemplateRepository.Object);
            viewTemplateService = new ViewTemplateService(UnitOfWork.Object, map, customerService.Object);

            var viewTemplates = viewTemplateService.GetViewTemplates().ToList();

            Assert.IsTrue(viewTemplates.Count == 2);
            Assert.AreEqual(viewTemplates[0].Name, "Preview all");
            Assert.AreEqual(viewTemplates[1].Name, "Default");
        }

        [TestMethod]
        public void CanGetViewTemplateById()
        {
            viewTemplateRepository.Setup(c => c.Get(It.IsAny<int>()))
                .Returns((int i) => ListViewTemplates.FirstOrDefault(c => c.Id == i));
            UnitOfWork.Setup(m => m.ViewTemplates).Returns(viewTemplateRepository.Object);
            viewTemplateService = new ViewTemplateService(UnitOfWork.Object, map, customerService.Object);

            var view1 = viewTemplateService.GetViewTemplate(1);
            var view2 = viewTemplateService.GetViewTemplate(2);

            Assert.AreEqual(view1.Name, "Preview all");
            Assert.AreEqual(view2.Name, "Default");
        }

        [TestMethod]
        [MyExpectedException(typeof(ValidationException),
        "Not set id of ViewTemplate")]
        public void CanNotGetViewTemplateByNullId()
        {
            UnitOfWork.Setup(m => m.ViewTemplates).Returns(viewTemplateRepository.Object);
            viewTemplateService = new ViewTemplateService(UnitOfWork.Object, map, customerService.Object);

            viewTemplateService.GetViewTemplate(null);
        }

        [TestMethod]
        [MyExpectedException(typeof(ValidationException),
        "ViewTemplate not found")]
        public void CanNotGetNonexistentViewTemplateByViewId()
        {
            viewTemplateRepository.Setup(c => c.Get(It.IsAny<int>()))
                .Returns((int i) => ListViewTemplates.FirstOrDefault(c => c.Id == i));
            UnitOfWork.Setup(m => m.ViewTemplates).Returns(viewTemplateRepository.Object);
            viewTemplateService = new ViewTemplateService(UnitOfWork.Object, map, customerService.Object);

            viewTemplateService.GetViewTemplate(5);
        }

        [TestMethod]
        public void CanDeleteViewTemplate()
        {
            viewTemplateRepository.Setup(c => c.IsExist(It.IsAny<int>()))
                .Returns((int i) => ListViewTemplates.Any(c => c.Id == i));
            viewTemplateRepository.Setup(m => m.Delete(It.IsAny<int>()))
                .Callback<int>(i => ListViewTemplates.RemoveAll(c => c.Id == i));
            UnitOfWork.Setup(m => m.ViewTemplates).Returns(viewTemplateRepository.Object);
            viewTemplateService = new ViewTemplateService(UnitOfWork.Object, map, customerService.Object);

            viewTemplateService.DeleteViewTemplate(1);

            Assert.IsTrue(ListViewTemplates.Count() == 1);
        }

        [TestMethod]
        [MyExpectedException(typeof(ValidationException),
        "Not set id of ViewTemplate")]
        public void CanNotDeleteViewTemplateByNullId()
        {
            UnitOfWork.Setup(m => m.ViewTemplates).Returns(viewTemplateRepository.Object);
            viewTemplateService = new ViewTemplateService(UnitOfWork.Object, map, customerService.Object);

            viewTemplateService.DeleteViewTemplate(null);
        }

        [TestMethod]
        [MyExpectedException(typeof(ValidationException),
        "ViewTemplate not found")]
        public void CanNotDeleteNonexistViewTemplate()
        {
            viewTemplateRepository.Setup(c => c.Get(It.IsAny<int>()))
                .Returns((int i) => ListViewTemplates.FirstOrDefault(c => c.Id == i));
            UnitOfWork.Setup(m => m.ViewTemplates).Returns(viewTemplateRepository.Object);
            viewTemplateService = new ViewTemplateService(UnitOfWork.Object, map, customerService.Object);

            viewTemplateService.DeleteViewTemplate(5);
        }

        [TestMethod]
        public void CanCreateViewTemplate()
        {
            viewTemplateRepository.Setup(m => m.Create(It.IsAny<ViewTemplate>()))
                .Callback<ViewTemplate>(ListViewTemplates.Add);
            viewTemplateRepository.Setup(c => c.Get(It.IsAny<int>()))
                .Returns((int i) => ListViewTemplates.FirstOrDefault(c => c.Id == i));
            customerService.Setup(m => m.GetCustomerByProfileId(It.IsAny<string>()))
                .Returns(new Customer { Id = 23123, Name = "Misha" });
            UnitOfWork.Setup(m => m.ViewTemplates).Returns(viewTemplateRepository.Object);
            viewTemplateService = new ViewTemplateService(UnitOfWork.Object, map, customerService.Object);

            viewTemplateService.CreateViewTemplate(new ViewTemplateDTO { }, "");

            Assert.IsTrue(ListViewTemplates.Count() == 3);
        }

        [TestMethod]
        [MyExpectedException(typeof(ValidationException),
        "ViewTemplate is null reference")]
        public void CanNotCreateNullReferenceView()
        {
            UnitOfWork.Setup(m => m.ViewTemplates).Returns(viewTemplateRepository.Object);
            viewTemplateService = new ViewTemplateService(UnitOfWork.Object, map, customerService.Object);

            viewTemplateService.CreateViewTemplate(null, "1");
        }

    }
}
