using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ServicioDeCupones.Data;
using ServicioDeCupones.Interfaces;
using ServicioDeCupones.Models;
using ServicioDeCupones.Models.DTO;
using System.Drawing;

namespace ServicioDeCupones.Controllers
{
    public class SolicitudCuponesController : ControllerBase
    {
        private readonly DataContext _dataContext;
        private readonly ICuponesService _cuponesServices;
        private readonly ISendEmailService _sendEmailService;

        public SolicitudCuponesController(DataContext dataContext, ICuponesService cuponesServices, ISendEmailService sendEmailService)
        {
            _dataContext = dataContext;
            _cuponesServices = cuponesServices;
            _sendEmailService = sendEmailService;
        }

        [HttpPost("SolicitarCupon")]
        public async Task<IActionResult> SolicitarCupon(ClientesDto clientesDto)
        {
            try
            {
                if (clientesDto.CodCliente.IsNullOrEmpty())
                
                    throw new Exception("El campo Dni no puede estar vacio");

                    string numeroCupon = await _cuponesServices.GenerarNumeroCupon();

                    Cupones_ClientesModel cupones_Cliente = new Cupones_ClientesModel();    
                    cupones_Cliente.id_Cupon = clientesDto.Id_Cupon;
                    cupones_Cliente.CodCliente = clientesDto.CodCliente;
                    cupones_Cliente.FechaAsignado = DateTime.Now;
                    cupones_Cliente.NroCupon = numeroCupon;

                    _dataContext.Cupones_Clientes.Add(cupones_Cliente);
                    await _dataContext.SaveChangesAsync();

                    await _sendEmailService.EnviarEmailCliente(clientesDto.Email, numeroCupon);    

                    return Ok(new
                    {
                        Mensaje = "El registro se dio de alta.",
                        NroCupon = numeroCupon
                    });

                

            }catch (Exception ex)
            {
                return BadRequest($"Error:{ex.Message}");
            }

        }


        // se debe enviar el numero de cupon, y eliminar el registro de cupones_clientes y agregar el registro en cupones_historial
        [HttpPost("QuemarCupon")]
        public async Task<IActionResult> QuemarCupon(string nroCupon)
        {
            try
            {
                var cuponCliente = await _dataContext.Cupones_Clientes
                    .FirstOrDefaultAsync(c => c.NroCupon == nroCupon);

                if (cuponCliente == null)
                {
                    return NotFound("El cupón no existe o ya ha sido usado.");
                }

                
                Cupones_HistorialModel cuponHistorial = new Cupones_HistorialModel
                {
                    id_Cupon = cuponCliente.id_Cupon,
                    CodCliente = cuponCliente.CodCliente,
                    NroCupon = cuponCliente.NroCupon,
                    FechaUso = DateTime.Now
                };

                _dataContext.Cupones_Historials.Add(cuponHistorial);

                
                _dataContext.Cupones_Clientes.Remove(cuponCliente);

                
                await _dataContext.SaveChangesAsync();

                return Ok(new
                {
                    Mensaje = "El cupón ha sido utilizado correctamente.",
                    NroCupon = nroCupon
                });
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpGet("ObtenerCuponesActivos")]
        public async Task<IActionResult> ObtenerCuponesActivos(string codCliente)
        {
            if (string.IsNullOrEmpty(codCliente))
            {
                return BadRequest("El código de cliente es obligatorio.");
            }

            var cuponesActivos = await (from c in _dataContext.Cupones
                                        join cc in _dataContext.Cupones_Clientes 
                                        on c.id_Cupon equals cc.id_Cupon
                                        where c.Activo == true && cc.CodCliente == codCliente 
                                        select new
                                        {
                                            c.id_Cupon,
                                            c.Nombre,
                                            c.Descripcion,
                                            c.PorcentajeDto,
                                            c.ImportePromo,
                                            c.FechaInicio,
                                            c.FechaFin,
                                            cc.NroCupon,
                                            cc.FechaAsignado
                                        }).ToListAsync();

            if (cuponesActivos == null || !cuponesActivos.Any())
            {
                return NotFound($"No se encontraron cupones activos para el cliente con código {codCliente}.");
            }

            return Ok(cuponesActivos);
        }



    }
}
