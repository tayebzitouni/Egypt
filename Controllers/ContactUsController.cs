using freelanceProjectEgypt03.Interfaces;
using freelanceProjectEgypt03.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace freelanceProjectEgypt03.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactUsController : ControllerBase
    {
        private readonly IRepository<ContactUs> _repository;

        public ContactUsController(IRepository<ContactUs> repository) => _repository = repository;

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _repository.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _repository.GetByIdAsync(id);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add(ContactUs contact)
        {
            var response = await _repository.AddAsync(contact);
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ContactUs contact)
        {
            var response = await _repository.UpdateAsync(id, contact);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _repository.DeleteAsync(id);
            return success ? Ok() : NotFound();
        }
    }
}

