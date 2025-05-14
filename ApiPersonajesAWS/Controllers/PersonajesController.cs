using ApiPersonajesAWS.Models;
using ApiPersonajesAWS.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiPersonajesAWS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonajesController : ControllerBase
    {
        private RepositoryPersonajes repo;

        public PersonajesController(RepositoryPersonajes repo)
        {
            this.repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetPersonajes()
        {
            List<Personaje> personajes = await repo.GetPersonajes();
            return Ok(personajes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPersonaje(int id)
        {
            Personaje personaje = await repo.FindPersonaje(id);
            if (personaje == null)
            {
                return NotFound();
            }
            return Ok(personaje);
        }

        [HttpPost]
        public async Task<IActionResult> AddPersonaje([FromBody] Personaje personaje)
        {
            if (personaje == null || string.IsNullOrEmpty(personaje.Nombre) || string.IsNullOrEmpty(personaje.Imagen))
            {
                return BadRequest("Invalid character data.");
            }
            Personaje newPersonaje = await repo.AddPersonaje(personaje.Nombre, personaje.Imagen);
            return CreatedAtAction(nameof(GetPersonaje), new { id = newPersonaje.IdPersonaje }, newPersonaje);
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePersonaje([FromBody] Personaje personaje)
        {
            if (personaje == null || string.IsNullOrEmpty(personaje.Nombre) || string.IsNullOrEmpty(personaje.Imagen))
            {
                return BadRequest("Invalid character data.");
            }
            bool updated = await repo.UpdatePersonaje(personaje.IdPersonaje, personaje.Nombre, personaje.Imagen);
            if (!updated)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePersonaje(int id)
        {
            bool deleted = await repo.DeletePersonaje(id);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
