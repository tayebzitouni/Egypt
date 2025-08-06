using freelanceProjectEgypt03.Interfaces;
using freelanceProjectEgypt03.Models;
using freelanceProjectEgypt03.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using freelanceProjectEgypt03.Dtos.freelanceProjectEgypt03.Dtos;
using freelanceProjectEgypt03.Repositories;
using freelanceProjectEgypt03.data.freelanceProjectEgypt03.Data;

namespace freelanceProjectEgypt03.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartnersController : ControllerBase
    {
        private readonly IPartnerRepository _repository;
        private readonly AppDbContext _context;

        public PartnersController(IPartnerRepository repository, AppDbContext context)
        {
            _repository = repository;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var partners = await _repository.GetAllAsync();

            var result = partners.Select(p => new PartnerDto
            {
                id = p.Id,
                Name = p.Name,
                Location = p.Location,
                Description = p.Description,
                StartDate = p.StartDate,
                PhoneNumber = p.PhoneNumber,
                Email = p.email,
                serviceId = p.ServiceId
            });

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var partner = await _repository.GetByIdAsync(id);
            if (partner == null) return NotFound();

            var dto = new PartnerDto
            {
                id = partner.Id,
                Name = partner.Name,
                Location = partner.Location,
                Description = partner.Description,
                StartDate = partner.StartDate,
                PhoneNumber = partner.PhoneNumber,
                Email = partner.email,
                serviceId = partner.ServiceId
            };

            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreatePartnerDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var model = new Partner
            {
                Name = dto.Name,
                Location = dto.Location,
                Description = dto.Description,
                StartDate = dto.StartDate,
                PhoneNumber = dto.PhoneNumber,
                email = dto.Email,
                Password = dto.Password,
                ServiceId = dto.ServiceId
            };

            await _repository.AddAsync(model);
            return Ok("Added");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreatePartnerDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return NotFound();

            existing.Name = dto.Name;
            existing.Location = dto.Location;
            existing.Description = dto.Description;
            existing.StartDate = dto.StartDate;
            existing.PhoneNumber = dto.PhoneNumber;
            existing.Password = dto.Password;
            existing.email = dto.Email;
            existing.ServiceId = dto.ServiceId;

            await _repository.UpdateAsync(id, existing);
            return Ok("Updated");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _repository.DeleteAsync(id);
            return success ? Ok("Deleted") : NotFound();
        }

        [HttpGet("by-service/{serviceId}")]
        public async Task<ActionResult<List<PartnerDto>>> GetByServiceId(int serviceId)
        {
            var partners = await _repository.GetPartnersByServiceIdAsync(serviceId);

            var result = partners.Select(p => new PartnerDto
            {
                id = p.Id,
                Name = p.Name,
                Email = p.email,
                PhoneNumber = p.PhoneNumber,
                Location = p.Location,
                Description = p.Description,
                serviceId = p.ServiceId,
               
            }).ToList();

            return Ok(result);
        }

    }
}
