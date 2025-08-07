using freelanceProjectEgypt03.Dtos.freelanceProjectEgypt03.Dtos;
using freelanceProjectEgypt03.Interfaces;
using freelanceProjectEgypt03.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace freelanceProjectEgypt03.Controllers
{
    [Route("api/[controller]")]
    [Authorize]

    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IRepository<Client> _repository;

        public ClientsController(IRepository<Client> repository)
        {
            _repository = repository;
        }

        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var clients = await _repository.GetAllAsync();
            var result = clients.Select(c => new ClientDto
            {
                Id = c.Id,
                Name = c.Name,
                PhoneNumber = c.PhoneNumber,
                Email = c.email,
                StartDate = c.StartDate
            }).ToList();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var c = await _repository.GetByIdAsync(id);
            if (c == null)
                return NotFound("Client not found");

            var dto = new ClientDto
            {
                Id = c.Id,
                Name = c.Name,
                PhoneNumber = c.PhoneNumber,
                Email = c.email,
                StartDate = c.StartDate
            };

            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateClientDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var client = new Client
            {
                Name = dto.Name,
                PhoneNumber = dto.PhoneNumber,

                email = dto.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                StartDate = dto.StartDate
            };

            var result = await _repository.AddAsync(client);
            return CreatedAtAction(nameof(GetById), new { id = client.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateClientDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var client = new Client
            {
                Name = dto.Name,
                PhoneNumber = dto.PhoneNumber,
                email = dto.Email,
                StartDate = dto.StartDate,
                Password  = dto.Password
            };

            var result = await _repository.UpdateAsync(id, client);
            if (result == "Not Found")
                return NotFound("Client not found");

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repository.DeleteAsync(id);
            return deleted ? Ok("Client deleted successfully") : NotFound("Client not found");
        }
    }
}
