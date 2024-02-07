using AutoMapper;
using BlogWebApi.Controllers;
using BlogWebApiTests.MockDatas;
using DataAccessLayer.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;
using System.Web.Http.Results;

namespace TestProject1
{
    [TestClass]
    public class UnitTest1
    {
        private readonly Mock<IUserRepository> mockUserRepository = new Mock<IUserRepository>();
        private readonly Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();
        private readonly Mock<IMapper> mockMapper = new Mock<IMapper>();
        private readonly UserController controller;

        public UnitTest1()
        {
            controller = new UserController(mockUnitOfWork.Object, mockUserRepository.Object, mockMapper.Object);
        }
        [TestMethod]
        public async Task TestMethod1Async()
        {
            mockUserRepository.Setup(repo => repo.GetAllUsers())
                      .ReturnsAsync(MockUserDatas.GetEmptyUsersMockDatas());
            var datas = (NotFoundResult)await controller.GetAllUsers();
            //Assert
            //datas.StatusCode.Should().Be(404);
        }
    }
}
