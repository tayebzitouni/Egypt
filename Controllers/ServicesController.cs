using freelanceProjectEgypt03.Interfaces;
using freelanceProjectEgypt03.Models;
using freelanceProjectEgypt03.data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using freelanceProjectEgypt03.data.freelanceProjectEgypt03.Data;
using freelanceProjectEgypt03.Dtos.freelanceProjectEgypt03.Dtos;
using Microsoft.AspNetCore.Authorization;

namespace freelanceProjectEgypt03.Controllers
{
    [Route("api/[controller]")]
    [Authorize]

    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly IRepository<Service> _repository;
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
       


        public ServicesController(IRepository<Service> repository, AppDbContext context, IWebHostEnvironment env)
        {
            _repository = repository;
            _context = context;
            _env = env;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var baseUrl = $"{Request.Scheme}://{Request.Host}";
            var services = await _context.Services
                .Include(s => s.Files)
                .ToListAsync();

            var result = services.Select(service => new GetServiceDto
            {
                Id = service.Id,
                Title = service.Title,
                Description = service.Description,
                DurationMin = service.DurationMin,
                DurationMax = service.DurationMax,
                DurationUnit = service.DurationUnit.ToString(),
                Price = service.Price,
                details = service.details,
                Files = service.Files.Select(file => new FileAttachmentDto
                {
                    Id = file.Id,
                    FileName = file.FileName,
                    FilePath = file.FilePath,
                    Size = file.Size,
                    UploadedAt = file.UploadedAt,
                    FileUrl = $"{baseUrl}/{file.FilePath.Replace("\\", "/")}" // ✅ Full URL

                }).ToList()
            }).ToList();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var baseUrl = $"{Request.Scheme}://{Request.Host}";

            var service = await _context.Services
                .Include(s => s.Files)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (service == null)
                return NotFound();

            var result = new GetServiceDto
            {
                Id = service.Id,
                Title = service.Title,
                Description = service.Description,
                DurationMin = service.DurationMin,
                DurationMax = service.DurationMax,
                DurationUnit = service.DurationUnit.ToString(),
                Price = service.Price,
                details = service.details,
                Files = service.Files.Select(file => new FileAttachmentDto
                {
                    Id = file.Id,
                    FileName = file.FileName,
                    FilePath = file.FilePath,
                    Size = file.Size,
                    UploadedAt = file.UploadedAt,
                    FileUrl = $"{baseUrl}/{file.FilePath.Replace("\\", "/")}" 

                }).ToList()
            };

            return Ok(result);
        }


        [HttpPost]
        [RequestSizeLimit(104857600)] 
        public async Task<IActionResult> AddService([FromForm] ServiceDto dto)
        {
            var service = new Service
            {
                Title = dto.Title,
                Description = dto.Description,
                DurationMin = dto.DurationMin,
                DurationMax = dto.DurationMax,
                DurationUnit = Enum.TryParse<DurationUnit>(dto.DurationUnit, true, out var unit) ? unit : DurationUnit.Hour,
                Price = dto.Price,
                details = dto.Details,
                Files = new List<FileAttachment>()
            };

            // حفظ الملفات
            var uploadPath = Path.Combine(_env.WebRootPath, "uploads", "services");
            Directory.CreateDirectory(uploadPath);

            foreach (var file in dto.Files)
            {
                if (file.Length > 0)
                {
                    var uniqueFileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                    var fullPath = Path.Combine(uploadPath, uniqueFileName);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    service.Files.Add(new FileAttachment
                    {
                        FileName = file.FileName,
                        FilePath = $"uploads/services/{uniqueFileName}",
                        Size = file.Length,
                        UploadedAt = DateTime.UtcNow
                    });
                }
            }

            await _repository.AddAsync(service);
            return Ok(new { message = "Service created successfully", service.Id });
        }
        [HttpPut("{id}")]
        [RequestSizeLimit(104_857_600)] 
        public async Task<IActionResult> UpdateService(int id, [FromForm] ServiceDto dto)
        {
            var existing = await _context.Services.Include(s => s.Files).FirstOrDefaultAsync(s => s.Id == id);
            if (existing == null) return NotFound();

            existing.Title = dto.Title;
            existing.Description = dto.Description;
            existing.DurationMin = dto.DurationMin;
            existing.DurationMax = dto.DurationMax;
            existing.DurationUnit = Enum.TryParse<DurationUnit>(dto.DurationUnit, true, out var unit) ? unit : DurationUnit.Hour;
            existing.Price = dto.Price;
            existing.details = dto.Details;

            foreach (var file in existing.Files)
            {
                var physicalPath = Path.Combine(_env.WebRootPath, file.FilePath);
                if (System.IO.File.Exists(physicalPath))
                {
                    System.IO.File.Delete(physicalPath);
                }
            }

            _context.files.RemoveRange(existing.Files);
            existing.Files.Clear(); 

            var uploadPath = Path.Combine(_env.WebRootPath, "uploads", "services");
            Directory.CreateDirectory(uploadPath);

            foreach (var file in dto.Files)
            {
                if (file.Length > 0)
                {
                    var uniqueFileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                    var fullPath = Path.Combine(uploadPath, uniqueFileName);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    existing.Files.Add(new FileAttachment
                    {
                        FileName = file.FileName,
                        FilePath = $"uploads/services/{uniqueFileName}",
                        Size = file.Length,
                        UploadedAt = DateTime.UtcNow
                    });
                }
            }

            await _context.SaveChangesAsync();
            return Ok(new { message = "Service updated successfully", id });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteService(int id)
        {
            var service = await _context.Services.Include(s => s.Files).FirstOrDefaultAsync(s => s.Id == id);
            if (service == null) return NotFound();

            foreach (var file in service.Files)
            {
                var path = Path.Combine(_env.WebRootPath, file.FilePath);
                if (System.IO.File.Exists(path))
                    System.IO.File.Delete(path);
            }

            _context.Services.Remove(service);
            await _context.SaveChangesAsync();

           
            return Ok(new { message = "Service deleted successfully" });
        }

       




    }
}
