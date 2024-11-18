using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServicioDeCupones.Data;
using ServicioDeCupones.Interfaces;
using ServicioDeCupones.Models;

namespace ServicioDeCupones.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuponesController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly ICuponesService _cuponesService;

        public CuponesController(DataContext context, ICuponesService cuponesService)
        {
            _context = context;
            _cuponesService = cuponesService;
        }

        // GET: api/Cupones
        [HttpGet("ObtenerCupones")]
        public async Task<ActionResult<IEnumerable<CuponesModel>>> ObtenerCupones()
        {
            var cupones = await _context.Cupones.ToListAsync();

            if (cupones == null || !cupones.Any())
            {
                return NotFound("No se encontraron cupones.");
            }

            return Ok(cupones);
        }


        // GET: api/Cupones/5
        [HttpGet("id/{id}")]
        public async Task<ActionResult<CuponesModel>> ObtenerCuponPorId(int id)
        {
            var cuponesModel = await _context.Cupones.FindAsync(id);

            if (cuponesModel == null)
            {
                return NotFound();
            }

            return cuponesModel;
        }

        [HttpGet("cliente/{codCliente}")]
        public async Task<ActionResult<Cupones_ClientesModel>> ObtenerCuponPorCodCliente(string codCliente)
        {
            var cuponesModel = await _context.Cupones_Clientes.FindAsync(codCliente);

            if (cuponesModel == null)
            {
                return NotFound();
            }

            return cuponesModel;
        }



        // PUT: api/Cupones/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> ModificarCupon(int id, CuponesModel cuponesModel)
        {
            if (id != cuponesModel.id_Cupon)
            {
                return BadRequest();
            }

            _context.Entry(cuponesModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CuponesModelExists(id))
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

        // POST: api/Cupones
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("AltaCupon")]
        public async Task<ActionResult<CuponesModel>> AltaCupon(CuponesModel cuponesModel)
        {
            _context.Cupones.Add(cuponesModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("ObtenerCupones", new { id = cuponesModel.id_Cupon }, cuponesModel);
        }

        // DELETE: api/Cupones/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> BorrarCupon(int id)
        {
            var cuponModel = await _context.Cupones.FindAsync(id);
            if (cuponModel == null)
            {
                return NotFound();
            }

            cuponModel.Activo = false;

            _context.Cupones.Update(cuponModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        

        private bool CuponesModelExists(int id)
        {
            return _context.Cupones.Any(e => e.id_Cupon == id);
        }
    }
}
