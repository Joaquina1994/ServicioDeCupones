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
        [HttpGet("ObtenerArticulos")]
        public async Task<ActionResult<IEnumerable<ArticulosModel>>> ObtenerArticulos()
        {
            return await _context.Articulos.ToListAsync();
        }

        // GET: api/Articulos/5
        [HttpGet("ObtenerArticuloPorId{id}")]
        public async Task<ActionResult<ArticulosModel>> ObtenerArticuloPorId(int id)
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
        public async Task<IActionResult> ModificarArticulo(int id, ArticulosModel articulosModel)
        {
            if (id != articulosModel.Id_Articulo)
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

        
        //Cuando se agrega un articulo se da de alta un precio
        [HttpPost("AgregarArticulo")]
        public async Task<ActionResult<ArticulosModel>> AgregarArticulo(ArticulosModel articulosModel, decimal precio)
        {
            _context.Articulos.Add(articulosModel);
            await _context.SaveChangesAsync();

            PreciosModel precioModel = new PreciosModel
            {
                Id_Articulo = articulosModel.Id_Articulo,
                Precio = precio
            };

            _context.Precios.Add(precioModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("ObtenerArticuloPorId", new { id = articulosModel.Id_Articulo }, articulosModel);
        }

        // DELETE: api/Articulos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> BorrarArticulo(int id)
        {
            var articulosModel = await _context.Articulos.FindAsync(id);
            if (articulosModel == null)
            {
                return NotFound();
            }

            articulosModel.Activo = false;

            _context.Articulos.Update(articulosModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ArticulosModelExists(int id)
        {
            return _context.Articulos.Any(e => e.Id_Articulo == id);
        }
    }
}
