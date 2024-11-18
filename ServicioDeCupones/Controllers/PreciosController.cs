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
    public class PreciosController : ControllerBase
    {
        private readonly DataContext _context;

        public PreciosController(DataContext context)
        {
            _context = context;
        }

        // trae los articulos con sus precios, si no tienen precio pone 0
        [HttpGet("ObtenerPrecios")]
        public async Task<ActionResult<IEnumerable<PreciosModel>>> ObtenerPrecios()
        {
            var articulosConPrecios = await _context.Articulos
        .Select(a => new
        {
            a.Id_Articulo,
            a.Nombre_Articulo,
            Precio = _context.Precios
                        .Where(p => p.Id_Articulo == a.Id_Articulo)
                        .Select(p => p.Precio)
                        .FirstOrDefault() // Esto devuelve 0 si no hay coincidencias
        })
        .ToListAsync();

            return Ok(articulosConPrecios);
        }

        // GET: api/Precios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PreciosModel>> GetPreciosModel(int id)
        {
            var preciosModel = await _context.Precios.FindAsync(id);

            if (preciosModel == null)
            {
                return NotFound();
            }

            return preciosModel;
        }

        // PUT: api/Precios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPreciosModel(int id, PreciosModel preciosModel)
        {
            if (id != preciosModel.Id_Precio)
            {
                return BadRequest();
            }

            _context.Entry(preciosModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PreciosModelExists(id))
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

        // POST: api/Precios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PreciosModel>> AltaPrecio(PreciosModel preciosModel)
        {
            _context.Precios.Add(preciosModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPreciosModel", new { id = preciosModel.Id_Precio }, preciosModel);
        }

        // DELETE: api/Precios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> BorrarPrecio(int id)
        {
            var preciosModel = await _context.Precios.FindAsync(id);
            if (preciosModel == null)
            {
                return NotFound();
            }

            _context.Precios.Remove(preciosModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PreciosModelExists(int id)
        {
            return _context.Precios.Any(e => e.Id_Precio == id);
        }
    }
}
