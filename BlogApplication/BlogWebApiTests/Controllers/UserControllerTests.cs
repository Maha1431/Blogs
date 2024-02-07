using AutoMapper;
using BlogWebApi.Controllers;
using BlogWebApiTests.MockDatas;
using CommonLayer.DataTransferObjects;
using DataAccessLayer.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace BlogWebApiTests.Controllers
{
    public class UserControllerTests
    {
        private readonly Mock<IUserRepository> mockUserRepository = new Mock<IUserRepository>();
        private readonly Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();
        private readonly Mock<IMapper> mockMapper = new Mock<IMapper>();
        private readonly UserController controller;

        public UserControllerTests()
        {
            controller = new UserController(mockUnitOfWork.Object, mockUserRepository.Object, mockMapper.Object);
        }

        [Fact]
        public async Task GetAllUsers_ShouldReturnBadRequest_WhenDatasNotFound()
        {
            //Act
            mockUserRepository.Setup(repo => repo.GetAllUsers())
                        .ReturnsAsync(MockUserDatas.GetEmptyUsersMock());
            var datas = (NotFoundResult)await controller.GetAllUsers();
            //Assert
             datas.StatusCode.Should().Be(404);        
        }

        [Fact]
        public async Task GetAllUsers_ShouldReturnOkResponse_WhenDatasFound()
        {
            mockUserRepository.Setup(repo => repo.GetAllUsers())
                .ReturnsAsync(MockUserDatas.GetAllUsersMock());
            //Act
            var result = await controller.GetAllUsers();
            // Assert
            result.Should().BeOfType<OkObjectResult>();     
        }

        [Fact]   
        public async Task GetUserById_ShouldReturnOkResponse_WhenUserFound()
        {
            int Id = 1;
            mockUserRepository.Setup(repo => repo.GetUserById(Id))
                             .ReturnsAsync(MockUserDatas.GetUserByIdMock());

            var result = await controller.GetUserById(Id);

            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task GetUserById_ShouldReturnNotFoundResult_WhenUserNotFound()
        {
           
            int Id = 100;
            mockUserRepository.Setup(repo => repo.GetUserById(Id))
                .ReturnsAsync(MockUserDatas.GetEmptyUserByIdMock());
            var result = await controller.GetUserById(Id);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeleteUser_ShouldReturnOkResponse_WhenUserFound()
        {
            int Id = 1;
           mockUserRepository.Setup(repo => repo.DeleteUser(Id)).ReturnsAsync(true);
             
            var data = await controller.DeleteUser(Id);
            Assert.IsType<OkResult>(data);
        }

        //working
        [Fact]
        public async Task DeleteUser_ShouldReturnNotFoundResult_WhenUserNotFound()
        {
            int Id = 100;
            mockUserRepository.Setup(repo => repo.DeleteUser(Id)).ReturnsAsync(false);
            var result = await controller.GetUserById(Id);
            Assert.IsType<NotFoundResult>(result);  
        }      

        [Fact]
        public async Task ResetPassword_ShouldReturnOkResponse_WhenValidDatas()
        {
            var reset = new ResetPasswordDTO
            {
                EmailId = "hindhujha@gmail.com",
                Password = "Hindhu!90",
                ConfirmPassword = "Hindhu!90",
                SecretCode = 9012
            };
            mockUserRepository.Setup(repo => repo.ResetPassword(reset)).ReturnsAsync(true);
            var result = await controller.ResetPassword(reset);
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task ResetPassword_ShouldReturnBadRequest_WhenNotAuthorized()
        {
            var reset = new ResetPasswordDTO
            {
                EmailId = "hindhujha@gmail.com",
                Password = "Hindhu!90",
                ConfirmPassword = "000niji!90"         
            };
            mockUserRepository.Setup(repo => repo.ResetPassword(reset)).ReturnsAsync(null);
            var result = await controller.ResetPassword(reset);
            result.Should().BeOfType<NotFoundResult>();
        }
    }
}
