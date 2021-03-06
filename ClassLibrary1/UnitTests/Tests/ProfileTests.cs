﻿using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using BLL.DTO;
using BLL.Services;
using DAL.ApplicationManager;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using UnitTests.Attributes;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Helpers;
using BLL.Infrastructure;
using BLL.Interfaces;
using Microsoft.AspNet.Identity;
using Profile = DAL.Entities.Profile;

namespace UnitTests.Tests
{
    [TestClass]
    public class ProfileTests
    {
        private Mock<IUnitOfWork> UnitOfWork;
        private UserService userService;
        private Mock<IUserService> mockUserService;
        private ProfileService profileService;
        private Mock<ApplicationUserManager> UserManager;
        private ValidateService validateService;
        private Mock<IProfileRepository> profileRepository;
        private IMapper map;
        # region Initialize
        List<UserEntity> ListUsers;
        List<Profile> ListProfiles;
        List<Role> ListRoles;

        Role user = new Role { Name = "user" };
        Role admin = new Role { Name = "admin" };
        UserEntity firstUser = new UserEntity
        {
            //Id = "1",
            UserName = "firstUser",
            Email = "firstUser@gmail.com",
            PasswordHash = "Password"
        };
        UserDTO firstUserDTO = new UserDTO
        {
            Id = "1",
            Login = "firstUser",
            Password = "Password"
        };
        UserEntity secondUser = new UserEntity
        {
            //Id = "2",
            UserName = "secondUser",
            Email = "secondUser@gmail.com",
            PasswordHash = "Password123"
        };
        Profile firstProfile = new Profile
        {
            Id = "1",
            Login = "firstProfile"
        };
        Profile secondProfile = new Profile
        {
            Id = "2",
            Login = "secondProfile"
        };
        #endregion

        [TestInitialize]
        public void Initialize()
        {
            ListUsers = new List<UserEntity> { firstUser, secondUser };
            ListProfiles = new List<Profile> { firstProfile, secondProfile };
            ListRoles = new List<Role> { user, admin };

            var userStore = new Mock<IUserStore<UserEntity>>();
            UnitOfWork = new Mock<IUnitOfWork>();
            UserManager = new Mock<ApplicationUserManager>(userStore.Object);
            profileRepository = new Mock<IProfileRepository>();
            mockUserService = new Mock<IUserService>();
            validateService = new ValidateService();
            map = new AutoMapperConfiguration().Configure().CreateMapper();
        }

        [TestMethod]
        public void CanGetProfile()
        {
            profileRepository.Setup(c => c.Get(It.IsAny<string>()))
                .Returns((string i) => ListProfiles.FirstOrDefault(c => c.Id == i));
            UnitOfWork.Setup(m => m.Profiles).Returns(profileRepository.Object);
            profileService = new ProfileService(UnitOfWork.Object, map, validateService, mockUserService.Object);

            ProfileDTO profile1 = profileService.GetProfile("1");
            ProfileDTO profile2 = profileService.GetProfile("2");

            Assert.AreEqual(profile1.Login, "firstProfile");
            Assert.AreEqual(profile2.Login, "secondProfile");
        }

        [TestMethod]
        [MyExpectedException(typeof(ValidationException),
         "Not set id of Profile")]
        public void CanNotGetProfileByNullId()
        {
            UnitOfWork.Setup(m => m.Profiles).Returns(profileRepository.Object);
            profileService = new ProfileService(UnitOfWork.Object, map, validateService, mockUserService.Object);

            profileService.GetProfile(null);
        }

        [TestMethod]
        [MyExpectedException(typeof(ValidationException),
         "Profile not found")]
        public void CanNotGetNonexistentProfileById()
        {
            profileRepository.Setup(c => c.Get(It.IsAny<string>()))
                .Returns((string i) => ListProfiles.FirstOrDefault(c => c.Id == i));
            UnitOfWork.Setup(m => m.Profiles).Returns(profileRepository.Object);
            profileService = new ProfileService(UnitOfWork.Object, map, validateService, mockUserService.Object);

            profileService.GetProfile("5");
        }

        [TestMethod]
        public async Task CanAuthenticate()
        {
            UserManager.Setup(c => c.FindAsync(It.IsAny<string>(), It.IsAny<string>()))
                .Returns((string userName, string password) =>
                {
                    return Task.FromResult(ListUsers.Where(u => u.UserName == userName)
                    .FirstOrDefault(u => u.PasswordHash == password));
                });
            UserManager.Setup(c => c.CreateIdentityAsync(It.IsAny<UserEntity>(), It.IsAny<string>()))
                .Returns((UserEntity userName, string password) => Task.FromResult(new ClaimsIdentity()));
            UnitOfWork.Setup(m => m.UserManager).Returns(UserManager.Object);
            userService = new UserService(UnitOfWork.Object, validateService, map);

            var result = await userService.AuthenticateAsync(firstUserDTO);

            Assert.IsNotNull(result);
        }
        [TestMethod]
        public async Task CanNotAuthenticateNonexistUser()
        {
            UserManager.Setup(c => c.FindAsync(It.IsAny<string>(), It.IsAny<string>()))
                .Returns((string userName, string password) =>
                {
                    return Task.FromResult(ListUsers.Where(u => u.UserName == userName)
                    .FirstOrDefault(u => u.PasswordHash == password));
                });
            UserManager.Setup(c => c.CreateIdentityAsync(It.IsAny<UserEntity>(), It.IsAny<string>()))
                .Returns((UserEntity userName, string password) => Task.FromResult(new ClaimsIdentity()));
            UnitOfWork.Setup(m => m.UserManager).Returns(UserManager.Object);
            userService = new UserService(UnitOfWork.Object, validateService, map);

            var result = await userService.AuthenticateAsync(new UserDTO { Login = "log", Password = "Pass" });

            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task CanCreateUser()
        {
            UserManager.Setup(c => c.FindByNameAsync(It.IsAny<string>()))
               .Returns((string Login) =>
               {
                   return Task.FromResult(ListUsers.FirstOrDefault(u => u.UserName == Login));
               });
            UserManager.Setup(c => c.CreateAsync(It.IsAny<UserEntity>(), It.IsAny<string>()))
               .Returns((UserEntity userName, string password) => Task.FromResult(new IdentityResult()));
            profileRepository.Setup(m => m.Create(It.IsAny<Profile>()))
                .Callback<Profile>(ListProfiles.Add);
            UnitOfWork.Setup(m => m.UserManager).Returns(UserManager.Object);
            UnitOfWork.Setup(m => m.Profiles).Returns(profileRepository.Object);
            userService = new UserService(UnitOfWork.Object, validateService, map);

            await userService.CreateAsync(new UserDTO { Login = "SomeUser1", Password = "PaWQEsdssWor-d12" });

            Assert.IsTrue(ListProfiles.Count() == 3);
            Assert.IsTrue(ListProfiles.ToList()[2].Login == "SomeUser1");
        }

        [TestMethod]
        [MyExpectedException(typeof(ValidationException),
            "Password must be at least 8 characters and contain uppercase letters," +
            " lowercase letters, numbers, special characters.|")]
        public async Task CanNotCreateNoValidUser()
        {
            UnitOfWork.Setup(m => m.UserManager).Returns(UserManager.Object);
            UnitOfWork.Setup(m => m.Profiles).Returns(profileRepository.Object);
            userService = new UserService(UnitOfWork.Object, validateService, map);

            await userService.CreateAsync(new UserDTO { Login = "So1", Password = "P" });
        }

        [TestMethod]
        [MyExpectedException(typeof(ValidationException),
    "Login must be at least 3 characters and no more than 16 characters. " +
    "Can contain any letter, number, underscore and hyphen.|")]
        public async Task CanNotCreateUserWithNoValidlogin()
        {
            UnitOfWork.Setup(m => m.UserManager).Returns(UserManager.Object);
            UnitOfWork.Setup(m => m.Profiles).Returns(profileRepository.Object);
            userService = new UserService(UnitOfWork.Object, validateService, map);

            await userService.CreateAsync(new UserDTO { Login = "S", Password = "PaWQEsdssWor-d12" });
        }

        [TestMethod]
        [MyExpectedException(typeof(ValidationException),
            "User with such login already exist")]
        public async Task CanNotCreateAlredyExistUser()
        {

            UserManager.Setup(c => c.FindByNameAsync(It.IsAny<string>()))
               .Returns((string Login) =>
               {
                   return Task.FromResult(ListUsers.FirstOrDefault(u => u.UserName == Login));
               });
            UnitOfWork.Setup(m => m.UserManager).Returns(UserManager.Object);
            UnitOfWork.Setup(m => m.Profiles).Returns(profileRepository.Object);
            userService = new UserService(UnitOfWork.Object, validateService, map);

            await userService.CreateAsync(new UserDTO { Login = "firstUser", Password = "Fio@$23rstUser@gmail.com" });

        }
    }
}
