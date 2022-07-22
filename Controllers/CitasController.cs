using CitaAPI.Models;
using CitaAPI.Repositories;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CitaAPI.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowOrigin")]
    [ApiController]
    public class CitasController : ControllerBase
    {
        private readonly ICitasRepository _citaRepository;

        public CitasController(ICitasRepository citaRepository)
        {
            _citaRepository = citaRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Citas>> GetProducts()
        {
            return await _citaRepository.Get();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Citas>> GetProducts(int id)
        {
            return await _citaRepository.Get(id);
        }

        [HttpPost]
        public async Task<ActionResult<Citas>> PostProducts([FromBody] Citas cita)
        {
            var newCita = await _citaRepository.Create(cita);
            return CreatedAtAction(nameof(GetProducts), new { id = newCita.Id }, newCita);
        }

        [HttpPut]
        public async Task<ActionResult> PutProducts(int id, [FromBody] Citas cita)
        {
            if(id != cita.Id)
            {
                return BadRequest();
            }

            await _citaRepository.Update(cita);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete (int id)
        {
            var citaToDelete = await _citaRepository.Get(id);
            if (citaToDelete == null)
                return NotFound();

            await _citaRepository.Delete(citaToDelete.Id);
            return NoContent();
        }
    }
}
