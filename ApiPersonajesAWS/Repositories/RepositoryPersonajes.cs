using ApiPersonajesAWS.Data;
using ApiPersonajesAWS.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiPersonajesAWS.Repositories
{
    public class RepositoryPersonajes
    {
        private PersonajesContext context;

        public RepositoryPersonajes(PersonajesContext context)
        {
            this.context = context;
        }

        public async Task<List<Personaje>> GetPersonajes()
        {
            return await context.Personajes.ToListAsync();
        }

        public async Task<Personaje> FindPersonaje(int id)
        {
            return await context.Personajes.FirstOrDefaultAsync(p => p.IdPersonaje == id);
        }

        public async Task<int> GetMaxIdPersonaje()
        {
            return await context.Personajes.MaxAsync(p => p.IdPersonaje);
        }

        public async Task<Personaje> AddPersonaje(string nombre, string imagen)
        {
            Personaje personaje = new Personaje();
            personaje.IdPersonaje = await GetMaxIdPersonaje() + 1;
            personaje.Nombre = nombre;
            personaje.Imagen = imagen;
            context.Personajes.Add(personaje);
            await context.SaveChangesAsync();
            return personaje;
        }
    }
}
