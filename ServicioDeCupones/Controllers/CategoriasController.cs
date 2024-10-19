using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServicioDeCupones.Data;
using ServicioDeCupones.Models;

namespace ServicioDeCupones.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly DataContext _context;

        public CategoriasController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Categorias
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoriasModel>>> GetCategorias()
        {
            return await _context.Categorias.ToListAsync();
        }

        // GET: api/Categorias/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoriasModel>> GetCategoriasModel(int id)
        {
            var categoriasModel = await _context.Categorias.FindAsync(id);

            if (categoriasModel == null)
            {
                return NotFound();
            }

            return categoriasModel;
        }

        // PUT: api/Categorias/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategoriasModel(int id, CategoriasModel categoriasModel)
        {
            if (id != categoriasModel.id_Categorias)
            {
                return BadRequest();
            }

            _context.Entry(categoriasModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoriasModelExists(id))
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

        // POST: api/Categorias
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CategoriasModel>> PostCategoriasModel(CategoriasModel categoriasModel)
        {
            _context.Categorias.Add(categoriasModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategoriasModel", new { id = categoriasModel.id_Categorias }, categoriasModel);
        }

        // DELETE: api/Categorias/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoriasModel(int id)
        {
            var categoriasModel = await _context.Categorias.FindAsync(id);
            if (categoriasModel == null)
            {
                return NotFound();
            }

            _context.Categorias.Remove(categoriasModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CategoriasModelExists(int id)
        {
            return _context.Categorias.Any(e => e.id_Categorias == id);
        }
    }
}
