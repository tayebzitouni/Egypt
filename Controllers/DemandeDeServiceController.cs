using freelanceProjectEgypt03.Dtos;
using freelanceProjectEgypt03.Models;
using freelanceProjectEgypt03.data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using freelanceProjectEgypt03.data.freelanceProjectEgypt03.Data;
using freelanceProjectEgypt03.Dtos.freelanceProjectEgypt03.Dtos;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace freelanceProjectEgypt03.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DemandeDeServiceController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DemandeDeServiceController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Add a new service request.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateDemandeDeServiceDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

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

            _context.DemandeDeServices.Add(demande);
            await _context.SaveChangesAsync();

            return Ok("Created");
        }

        /// <summary>
        /// Get all demandes.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReadDemandeDeServiceDto>>> GetAll()
        {
            var demandes = await _context.DemandeDeServices
                .Include(d => d.client)
                .Include(d => d.service)
                .Select(d => new ReadDemandeDeServiceDto
                {
                    Id = d.Id,
                    Date = d.date,
                    Priorite = d.priorite,
                    PreferedContactMethode = d.PreferedContactMethode,
                    Description = d.description,
                    PreferedDateAndTime = d.preferedDateAndTime,
                    Location = d.location,
                    Budget = d.budget,
                    AdditionalServices = d.AdditionalServices,
                    ClientId = d.ClientId,
                    ClientName = d.client.Name,
                    ServiceId = d.ServiceId,
                    ServiceName = d.service.Title
                }).ToListAsync();

            return Ok(demandes);
        }

        /// <summary>
        /// Get a demande by id.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<ReadDemandeDeServiceDto>> GetById(int id)
        {
            var d = await _context.DemandeDeServices
                .Include(d => d.client)
                .Include(d => d.service)
                .FirstOrDefaultAsync(d => d.Id == id);

            if (d == null) return NotFound();

            var dto = new ReadDemandeDeServiceDto
            {
                Id = d.Id,
                Date = d.date,
                Priorite = d.priorite,
                PreferedContactMethode = d.PreferedContactMethode,
                Description = d.description,
                PreferedDateAndTime = d.preferedDateAndTime,
                Location = d.location,
                Budget = d.budget,
                AdditionalServices = d.AdditionalServices,
                ClientId = d.ClientId,
                ClientName = d.client.Name,
                ServiceId = d.ServiceId,
                ServiceName = d.service.Title
            };

            return Ok(dto);
        }

        /// <summary>
        /// Delete a demande.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _context.DemandeDeServices.FindAsync(id);
            if (entity == null) return NotFound();

            _context.DemandeDeServices.Remove(entity);
            await _context.SaveChangesAsync();

            return Ok("Deleted");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDemande(int id, [FromBody] CreateDemandeDeServiceDto dto)
        {
            

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var demande = await _context.DemandeDeServices.FindAsync(id);

            if (demande == null)
                return NotFound($"Demande with ID {id} not found.");


            demande.priorite = dto.Priorite;
            demande.PreferedContactMethode = dto.PreferedContactMethode;
            demande.description = dto.Description;
            demande.preferedDateAndTime = dto.PreferedDateAndTime;
            demande.location = dto.Location;
            demande.budget = dto.Budget;
            demande.AdditionalServices = dto.AdditionalServices;
            demande.ClientId = dto.ClientId;
            demande.ServiceId = dto.ServiceId;

            await _context.SaveChangesAsync();

            return Ok(new { message = "Demande updated successfully." });
        }


        // GET: api/DemandeDeService/client/5
        [HttpGet("client/{clientId}")]
        public async Task<IActionResult> GetDemandesByClientId(int clientId)
        {
            var demandes = await _context.DemandeDeServices
                .Where(d => d.ClientId == clientId)
                .Include(d => d.client)
                .Include(d => d.service)
                .ToListAsync();

            if (!demandes.Any())
                return NotFound($"No demandes found for client with ID {clientId}");

            var response = demandes.Select(d => new
            {
                d.Id,
                d.priorite,
                d.PreferedContactMethode,
                d.description,
                d.preferedDateAndTime,
                d.location,
                d.budget,
                d.AdditionalServices,
                Client = new { d.client.Id, d.client.Name, d.client.email },
                Service = new { d.service.Id, d.service.Title}
            });

            return Ok(response);
        }

    }
}
