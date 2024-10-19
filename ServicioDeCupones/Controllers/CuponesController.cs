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
    public class CuponesController : ControllerBase
    {
        private readonly DataContext _context;

        public CuponesController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Cupones
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CuponesModel>>> GetCupones()
        {
            return await _context.Cupones.ToListAsync();
        }

        // GET: api/Cupones/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CuponesModel>> GetCuponesModel(int id)
        {
            var cuponesModel = await _context.Cupones.FindAsync(id);

            if (cuponesModel == null)
            {
                return NotFound();
            }

            return cuponesModel;
        }

        // PUT: api/Cupones/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCuponesModel(int id, CuponesModel cuponesModel)
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
        [HttpPost]
        public async Task<ActionResult<CuponesModel>> PostCuponesModel(CuponesModel cuponesModel)
        {
            _context.Cupones.Add(cuponesModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCuponesModel", new { id = cuponesModel.id_Cupon }, cuponesModel);
        }

        // DELETE: api/Cupones/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCuponesModel(int id)
        {
            var cuponesModel = await _context.Cupones.FindAsync(id);
            if (cuponesModel == null)
            {
                return NotFound();
            }

            _context.Cupones.Remove(cuponesModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CuponesModelExists(int id)
        {
            return _context.Cupones.Any(e => e.id_Cupon == id);
        }
    }
}
