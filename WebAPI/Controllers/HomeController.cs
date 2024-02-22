using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [ApiVersion(1.0)]
    [ApiVersion(2.0)]
    [ApiController]
    [Route("api/v{version:apiVersion}/home")]
    
    public class HomeController : ControllerBase
    {
        // GET: api/<HomeController>
        [HttpGet, MapToApiVersion(2.0)]
        public IEnumerable<string> Get(ApiVersion version)
        {
            return new string[] { "value1" + version, "value2" };
        }

        // GET api/<HomeController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
    //    // GET api/<HomeController>/5
    //    [HttpGet("download/{fileName}")]
    //    public IActionResult DownloadVideo(string fileName)
    //    {
    //        string filePath = Path.Combine(Directory.GetCurrentDirectory(),"Upload",fileName);
    //        if(!System.IO.File.Exists(filePath))
    //        {
    //            return NotFound();
    //        }
    //        byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
    //        return File(fileBytes, "application/octet-stream", fileName);
    //    }

    //    // POST api/<HomeController>
    //    [HttpPost("upload")]
    //    public async Task<IActionResult> Post(IFormFile file)
    //    {
    //        string filePath = Path.Combine(Directory.GetCurrentDirectory(),"Upload",file.FileName);
    //        using (var fileStream = new FileStream(filePath, FileMode.Create))
    //        {
    //            await file.CopyToAsync(fileStream);
    //        }
    //        return Ok(file.FileName);
    //    }

    //    // PUT api/<HomeController>/5
    //    [HttpPut("{id}")]
    //    public void Put(int id, [FromBody] string value)
    //    {
    //    }

    //    // DELETE api/<HomeController>/5
    //    [HttpDelete("{fileName}")]
    //    public IActionResult Delete(string fileName)
    //    {
    //        string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Upload", fileName);
    //        if (!System.IO.File.Exists(filePath))
    //        {
    //            return NotFound();
    //        }
    //        System.IO.File.Delete(filePath);
    //        return Ok();
    //    }
    //
    }
}
