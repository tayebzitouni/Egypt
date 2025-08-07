using freelanceProjectEgypt03.Dtos;
using freelanceProjectEgypt03.Dtos.freelanceProjectEgypt03.Dtos;
using freelanceProjectEgypt03.Interfaces;
using freelanceProjectEgypt03.Models;
using Microsoft.AspNetCore.Mvc;

namespace freelanceProjectEgypt03.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactUsController : ControllerBase
    {
        private readonly IRepository<ContactUs> _repository;

        public ContactUsController(IRepository<ContactUs> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var contactMessages = await _repository.GetAllAsync();
            var result = contactMessages.Select(c => new ContactUsDto
            {
                Name = c.Name,
                id = c.Id,
                Email = c.Email,
                Phone = c.Phone,
                Subject = c.Subject,
                Description = c.description,
                Priorite = c.priorite,
                PreferedContactMethode = c.PreferedContactMethode,
                Date = c.date
            }).ToList();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var contact = await _repository.GetByIdAsync(id);
            if (contact == null)
                return NotFound();

            var dto = new ContactUsDto
            {
                Name = contact.Name,
                id = contact.Id,
                Email = contact.Email,
                Phone = contact.Phone,
                Subject = contact.Subject,
                Description = contact.description,
                Priorite = contact.priorite,
                PreferedContactMethode = contact.PreferedContactMethode,
                Date = contact.date
            };

            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ContactUsDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var model = new ContactUs
            {
                Name = dto.Name,
                Email = dto.Email,
                Phone = dto.Phone,
                Subject = dto.Subject,
                description = dto.Description,
                priorite = dto.Priorite,
                PreferedContactMethode = dto.PreferedContactMethode,
                date = dto.Date
            };

            await _repository.AddAsync(model);
            return Ok("Message envoyé avec succès.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ContactUsDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var model = new ContactUs
            {
                Name = dto.Name,
                Email = dto.Email,
                Phone = dto.Phone,
                Subject = dto.Subject,
                description = dto.Description,
                priorite = dto.Priorite,
                PreferedContactMethode = dto.PreferedContactMethode,
                date = dto.Date
            };

            var result = await _repository.UpdateAsync(id, model);
            if (result == "Not Found")
                return NotFound();

            return Ok("Message mis à jour avec succès.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repository.DeleteAsync(id);
            return deleted ? Ok("Message supprimé.") : NotFound();
        }
    }
}
