using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using _20GRPED.MVC2.Data.Context;
using _20GRPED.MVC2.Domain.Model.Entities;

namespace _20GRPED.MVC2.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutorEntitiesController : ControllerBase
    {
        private readonly BibliotecaContext _context;

        public AutorEntitiesController(BibliotecaContext context)
        {
            _context = context;
        }

        // GET: api/AutorEntities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AutorEntity>>> GetAutores()
        {
            return await _context.Autores.ToListAsync();
        }

        // GET: api/AutorEntities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AutorEntity>> GetAutorEntity(int id)
        {
            var autorEntity = await _context.Autores
                .Include(x => x.Livros)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (autorEntity == null)
            {
                return NotFound();
            }

            return autorEntity;
        }

        // PUT: api/AutorEntities/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAutorEntity(int id, AutorEntity autorEntity)
        {
            if (id != autorEntity.Id)
            {
                return BadRequest();
            }

            _context.Entry(autorEntity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AutorEntityExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/AutorEntities
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<AutorEntity>> PostAutorEntity(AutorEntity autorEntity)
        {
            _context.Autores.Add(autorEntity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAutorEntity", new { id = autorEntity.Id }, autorEntity);
        }

        // DELETE: api/AutorEntities/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AutorEntity>> DeleteAutorEntity(int id)
        {
            var autorEntity = await _context.Autores.FindAsync(id);
            if (autorEntity == null)
            {
                return NotFound();
            }

            _context.Autores.Remove(autorEntity);
            await _context.SaveChangesAsync();

            return autorEntity;
        }

        private bool AutorEntityExists(int id)
        {
            return _context.Autores.Any(e => e.Id == id);
        }
    }
}
