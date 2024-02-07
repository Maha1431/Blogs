using AutoMapper;
using BlogWebApi.Controllers;
using BlogWebApiTests.MockDatas;
using DataAccessLayer.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace BlogWebApiTests.Controllers
{
    public class BlogControllerTests
    {
        private readonly Mock<IBlogRepository> mockBlogRepository = new Mock<IBlogRepository>();
        private readonly Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();
        private readonly Mock<IMapper> mockMapper = new Mock<IMapper>();
        private readonly BlogController controller;

        public BlogControllerTests()
        {
           controller = new BlogController(mockUnitOfWork.Object, mockBlogRepository.Object, mockMapper.Object);
        }

        [Fact]
        public async Task GetAllBlogs_ShouldReturnBadRequest_WhenBlogNotFound()
        {
            //Act
            mockBlogRepository.Setup(repo => repo.GetAllBlogs())
                        .ReturnsAsync(MockBlogDatas.GetEmptyBlogMock());
            var datas = (NotFoundResult)await controller.GetAllBlogs();
            //Assert
            datas.StatusCode.Should().Be(404);
        }

        [Fact]
        public async Task GetAllUsers_ShouldReturnOkResponse_WhenDatasFound()
        {
            mockBlogRepository.Setup(repo => repo.GetAllBlogs())
                .ReturnsAsync(MockBlogDatas.GetAllBlogsMock());
            //Act
            var result = await controller.GetAllBlogs();
            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task GetBlogById_ShouldReturnOkResponse_WhenBlogFound()
        {
            int Id = 1;
            mockBlogRepository.Setup(repo => repo.GetBlog(Id))
                             .ReturnsAsync(MockBlogDatas.GetBlogByIdMock());

            var result = await controller.GetBlogByIdAsync(Id);

            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task GetUserById_ShouldReturnNotFoundResult_WhenUserNotFound()
        {

            int Id = 100;
            mockBlogRepository.Setup(repo => repo.GetBlog(Id))
                .ReturnsAsync(MockBlogDatas.GetEmptyBlogByIdMock());
            var result = await controller.GetBlogByIdAsync(Id);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeleteBlog_ShouldReturnOkResponse_WhenBlogFound()
        {
            int Id = 1;
            mockBlogRepository.Setup(repo => repo.DeleteBlog(Id)).ReturnsAsync(true);

            var data = await controller.DeleteBlog(Id);
            Assert.IsType<OkResult>(data);
        }

        //working
        [Fact]
        public async Task DeleteBlog_ShouldReturnNotFoundResult_WhenBlogNotFound()
        {
            int Id = 100;
            mockBlogRepository.Setup(repo => repo.DeleteBlog(Id)).ReturnsAsync(false);
            var result = await controller.DeleteBlog(Id);
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
