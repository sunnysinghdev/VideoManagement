using Domain.Models;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using WebAPI.Controllers;

namespace WebAPI.Test
{
    public class VideoControllerTest
    {
        readonly Mock<IVideoService> _mockVideoService;
        readonly Mock<IFileService> _mockFileService;
        readonly Mock<ILogger<VideosController>> _mockLogger;

        public VideoControllerTest()
        {
            _mockVideoService = new Mock<IVideoService>();
            _mockFileService = new Mock<IFileService>();
            _mockLogger = new Mock<ILogger<VideosController>>();
        }

        [Fact]
        public void TestCreateVideo_Success()
        {
            // Arrange
            var controller = new VideosController(_mockVideoService.Object, _mockFileService.Object, _mockLogger.Object);
            var video = new CreateVideo
            {
                FileName = "1.mp4",
            };
            // Act
            var result = controller.Post(video);
            // Assert

            Assert.NotNull(result);
            Assert.IsType<NoContentResult>(result);

        }
        [Fact]
        public void TestCreateVideo_Exception()
        {
            // Arrange
            var controller = new VideosController(_mockVideoService.Object, _mockFileService.Object, _mockLogger.Object);
            var video = new Video();

            _mockVideoService.Setup(_=> _.Create(video)).Throws(new Exception("Invalid file"));
            // Act
            var result = controller.Post(new CreateVideo());
            // Assert

            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);

        }
    }
}