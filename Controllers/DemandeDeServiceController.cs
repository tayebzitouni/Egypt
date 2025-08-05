using freelanceProjectEgypt03.Dtos.freelanceProjectEgypt03.Dtos;
using freelanceProjectEgypt03.Interfaces;
using freelanceProjectEgypt03.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace freelanceProjectEgypt03.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemandeDeServiceController : ControllerBase
    {
        private readonly IRepository<DemandeDeService> _repository;

        public DemandeDeServiceController(IRepository<DemandeDeService> repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> Add(DemandeDeServiceDto dto)
        {
            var demande = new DemandeDeService
            {
                date = dto.Date,
                priorite = dto.Priorite,
                PreferedContactMethode = dto.PreferedContactMethode,
                description = dto.Description,
                preferedDateAndTime = dto.PreferedDateAndTime,
                location = dto.Location,
                budget = dto.Budget,
                AdditionalServices = dto.AdditionalServices,
                ClientId = dto.ClientId,
                ServiceId = dto.ServiceId
            };

            string result = await _repository.AddAsync(demande);
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<List<DemandeDeService>>> GetAll()
        {
            var result = await _repository.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DemandeDeService>> GetById(int id)
        {
            var result = await _repository.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, DemandeDeServiceDto dto)
        {
            var entity = new DemandeDeService
            {
                Id = id,
                date = dto.Date,
                priorite = dto.Priorite,
                PreferedContactMethode = dto.PreferedContactMethode,
                description = dto.Description,
                preferedDateAndTime = dto.PreferedDateAndTime,
                location = dto.Location,
                budget = dto.Budget,
                AdditionalServices = dto.AdditionalServices,
                ClientId = dto.ClientId,
                ServiceId = dto.ServiceId
            };

            string result = await _repository.UpdateAsync(id, entity);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _repository.DeleteAsync(id);
            if (!success) return NotFound();
            return Ok("Deleted");
        }
    }
}

