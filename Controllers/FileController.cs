
using AutoMapper;
using TestingHTTPie.Dto;
using TestingHTTPie.Interfaces;
using TestingHTTPie.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestingHTTPie.Data;
using TestingHTTPie.Interfaces;
using TestingHTTPie.Models;
using Microsoft.Data.SqlClient;

namespace TestingHTTPie.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : Controller
    {
        private readonly ContextTestingHTTPie _context;

        public FilesController(ContextTestingHTTPie context)
        {
            _context = context;
        }

        // POST: api/files/upload
        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            var fileModel = new FileModel
            {
                FileName = file.FileName,
                ContentType = file.ContentType
            };

            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                fileModel.Data = memoryStream.ToArray();
            }
            
            Console.WriteLine(fileModel.FileName);

            _context.Files.Add(fileModel);
            await _context.SaveChangesAsync();

            return Ok(new { fileModel.Id });
        }

        // GET: api/files/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFile(Guid id)
        {
            var fileModel = await _context.Files.FindAsync(id);

            if (fileModel == null)
                return NotFound();

            return File(fileModel.Data, fileModel.ContentType, fileModel.FileName);
        }

        // DELETE: api/files/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFile(int id)
        {
            var fileModel = await _context.Files.FindAsync(id);

            if (fileModel == null)
                return NotFound();

            _context.Files.Remove(fileModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }

}
