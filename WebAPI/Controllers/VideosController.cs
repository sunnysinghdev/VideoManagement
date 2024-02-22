using Asp.Versioning;
using Domain.Models;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;
using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [ApiVersion(1.0)]
    [Route("api/v{version:apiVersion}/videos")]
    [ApiController]
    public class VideosController : ControllerBase
    {
        readonly IVideoService _videoService;
        readonly IFileService _fileService;
        private readonly ILogger<VideosController> _logger;

        public VideosController(IVideoService videoService, IFileService fileService, ILogger<VideosController> logger)
        {
            _videoService = videoService;
            _fileService = fileService;
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Video> Get()
        {
            return _videoService.GetAll();
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try 
            {
                return Ok(_videoService.Get(id));
            }
            catch (Exception ex)
            {
                _logger.LogWarning("Video not found.");
                return NotFound(ex);
            }
        }

        [HttpPost]
        public IActionResult Post(IFormFile file, [FromQuery] string title, [FromQuery] string description)
        {
            try
            {
                var videoRequest = new CreateVideo
                {
                    Title = title,
                    Description = description,
                    File = file,
                };
                _fileService.Upload(videoRequest.File);
                var video = new Video
                {
                    FileName = videoRequest.File.FileName,
                    Url = "Upload/" + videoRequest.File.FileName,
                    Size = videoRequest.File.Length
                };
                int id = _videoService.Create(video);

                var info = new VideoInfo
                {
                   VideoId = id,
                   Title = videoRequest.Title,
                   Description = videoRequest.Description,
                   IsDownloaded = false
                };
                _videoService.CreateVideoInfo(info);

                _logger.LogInformation("Video created successfully.");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex);
            }
        }
        [HttpGet("download/{id}")]
        public IActionResult Download(int id)
        {
            try
            {
                var video = _videoService.Get(id);
                var fileBytes = _fileService.Download(video.FileName);
                return File(fileBytes, "application/octet-stream", video.FileName);

            }
            catch
            {
                _logger.LogError("Invalid Operation");
                return BadRequest("Invalid Operation");
            }

        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Video video)
        {
            _logger.LogError("Invalid Operation");
            return BadRequest(new NotImplementedException());
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _logger.LogError("Invalid Operation");
            return BadRequest(new NotImplementedException());
        }
    }
}
