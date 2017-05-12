using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using BLL.DTO;
using BLL.Services;
using DAL.ApplicationManager;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace UnitTests.Tests
{
    [TestClass]
    public class UserTests
    {
        private Mock<IUnitOfWork> UnitOfWork;
        private UserService userService;
        private Mock<ApplicationUserManager> UserManager;
        private Mock<ApplicationRoleManager> RoleManager;
        private ValidateService validateService;
        private Mock<IProfileRepository> profileRepository;
        # region Initialize
        List<User> ListUsers;
        List<Profile> ListProfiles;
        List<Role> ListRoles;

        Role user = new Role { Name = "user" };
        Role admin = new Role { Name = "admin" };
        User firstUser = new User
        {
            UserName = "firstUser",
            Email = "firstUser@gmail.com",
            PasswordHash = "Password"
        };
        User secondUser = new User
        {
            UserName = "secondUser",
            Email = "secondUser@gmail.com",
            PasswordHash = "Password123"
        };
        Profile firstProfile = new Profile
        {
            Id = "1",
            Name = "firstProfile"
        };
        Profile secondProfile = new Profile
        {
            Id = "2",
            Name = "secondProfile"
        };
        #endregion

        [TestInitialize]
        public void Initialize()
        {
            ListUsers = new List<User> { firstUser, secondUser };
            ListProfiles = new List<Profile> { firstProfile, secondProfile };
            ListRoles = new List<Role> { user, admin };

            UnitOfWork = new Mock<IUnitOfWork>();
            UserManager = new Mock<ApplicationUserManager>();
            RoleManager = new Mock<ApplicationRoleManager>();
            profileRepository = new Mock<IProfileRepository>();
        }

        [TestMethod]
        public void CanGetProfile()
        {
            profileRepository.Setup(c => c.Get(It.IsAny<string>()))
                .Returns((string i) => ListProfiles.FirstOrDefault(c => c.Id == i));
            UnitOfWork.Setup(m => m.Profiles).Returns(profileRepository.Object);
            userService = new UserService(UnitOfWork.Object, validateService);

            ProfileDTO profile1 = userService.GetProfile("1");
            ProfileDTO profile2 = userService.GetProfile("2");

            Assert.AreEqual(profile1.Name, "firstProfile");
            Assert.AreEqual(profile2.Name, "secondProfile");
        }
        
    }
}
