using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BLL.Infrastructure;
using BLL.Services;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace UnitTests.Tests
{
    [TestClass]
    public class ViewTemplateColumnTests
    {
        private Mock<IUnitOfWork> UnitOfWork;
        private Mock<IViewTemplateRepository> viewTemplateRepository;
        private Mock<IViewTemplateColumnRepository> viewTemplateColumnRepository;
        private ViewTemplateColumnService viewTemplateColumnService;
        private ValidateService validateService;
        private IMapper map;
        List<ViewTemplateColumn> ListViewTemplateColumns;

        #region ViewTemplateColumn Inizialize
        ViewTemplateColumn viewTemplateColumn1 = new ViewTemplateColumn
        {
            Id = 1,
            Name = "Name",
            ViewTemplateId = 1,
            DisplayIndex = 1,
            ColumnId = 1,
            ColumnFormatId = 3
        };
        ViewTemplateColumn viewTemplateColumn2 = new ViewTemplateColumn
        {
            Id = 2,
            Name = "Symbol",
            ViewTemplateId = 1,
            DisplayIndex = 2,
            ColumnId = 2,
            ColumnFormatId = 1
        };
        #endregion

        [TestInitialize]
        public void Initialize()
        {

            ListViewTemplateColumns = new List<ViewTemplateColumn> { viewTemplateColumn1, viewTemplateColumn2 };
            UnitOfWork = new Mock<IUnitOfWork>();
            viewTemplateRepository = new Mock<IViewTemplateRepository>();
            viewTemplateColumnRepository = new Mock<IViewTemplateColumnRepository>();
            validateService = new ValidateService();
            map = new AutoMapperConfiguration().Configure().CreateMapper();
        }


        [TestMethod]
        public void CanGetViewTemplateById()
        {
            viewTemplateColumnRepository.Setup(c => c.Get(It.IsAny<int>()))
                .Returns((int i) => ListViewTemplateColumns.FirstOrDefault(c => c.Id == i));
            UnitOfWork.Setup(m => m.ViewTemplateColumns).Returns(viewTemplateColumnRepository.Object);
            viewTemplateColumnService = new ViewTemplateColumnService(UnitOfWork.Object,validateService , map);

            var column1 = viewTemplateColumnService.GetViewTemplateColumn(1);
            var column2 = viewTemplateColumnService.GetViewTemplateColumn(2);

            Assert.AreEqual(column1.Name, "Name");
            Assert.AreEqual(column2.Name, "Symbol");
        }
    }
}
