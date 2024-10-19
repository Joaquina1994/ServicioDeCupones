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
    public class ArticulosController : ControllerBase
    {
        private readonly DataContext _context;

        public ArticulosController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Articulos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ArticulosModel>>> GetArticulos()
        {
            return await _context.Articulos.ToListAsync();
        }

        // GET: api/Articulos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ArticulosModel>> GetArticulosModel(int id)
        {
            var articulosModel = await _context.Articulos.FindAsync(id);

            if (articulosModel == null)
            {
                return NotFound();
            }

            return articulosModel;
        }

        // PUT: api/Articulos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArticulosModel(int id, ArticulosModel articulosModel)
        {
            if (id != articulosModel.id_Articulos)
            {
                return BadRequest();
            }

            _context.Entry(articulosModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArticulosModelExists(id))
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

        // POST: api/Articulos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ArticulosModel>> PostArticulosModel(ArticulosModel articulosModel)
        {
            _context.Articulos.Add(articulosModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetArticulosModel", new { id = articulosModel.id_Articulos }, articulosModel);
        }

        // DELETE: api/Articulos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArticulosModel(int id)
        {
            var articulosModel = await _context.Articulos.FindAsync(id);
            if (articulosModel == null)
            {
                return NotFound();
            }

            _context.Articulos.Remove(articulosModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ArticulosModelExists(int id)
        {
            return _context.Articulos.Any(e => e.id_Articulos == id);
        }
    }
}
