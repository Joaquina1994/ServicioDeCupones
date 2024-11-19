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
        public async Task<IActionResult> SolicitarCupon([FromBody] ClientesDto clientesDto)
        {
            try
            {
                
                if (string.IsNullOrEmpty(clientesDto.CodCliente))
                {
                    return BadRequest("El campo CodCliente no puede estar vacío.");
                }

                if (string.IsNullOrEmpty(clientesDto.Email))
                {
                    return BadRequest("El campo Email no puede estar vacío.");
                }



                
                string numeroCupon = await _cuponesServices.GenerarNumeroCupon();

               
                if (string.IsNullOrEmpty(numeroCupon))
                {
                    return BadRequest("Error al generar el número de cupón.");
                }

                
                Cupones_ClientesModel cupones_Cliente = new Cupones_ClientesModel
                {
                    id_Cupon = clientesDto.Id_Cupon,
                    CodCliente = clientesDto.CodCliente,
                    FechaAsignado = DateTime.Now,
                    NroCupon = numeroCupon
                };

               
                _dataContext.Cupones_Clientes.Add(cupones_Cliente);
                await _dataContext.SaveChangesAsync();

               
                await _sendEmailService.EnviarEmailCliente(clientesDto.Email, numeroCupon);

                return Ok(new
                {
                    Mensaje = "El registro se dio de alta.",
                    NroCupon = numeroCupon
                });
            }
            catch (DbUpdateException dbEx)
            {
                
                return BadRequest($"Error en la base de datos: {dbEx.Message}");
            }
            catch (Exception ex)
            {
                
                return BadRequest($"Error: {ex.Message}");
            }
        }




        // se debe enviar el numero de cupon, y eliminar el registro de cupones_clientes y agregar el registro en cupones_historial
        [HttpPost("QuemarCupon")]
        public async Task<IActionResult> QuemarCupon([FromQuery] string nroCupon, ClientesDto clientesDto)
        {
            try
            {
                // Validar nroCupon
                if (string.IsNullOrEmpty(nroCupon))
                {
                    return BadRequest("El número de cupón no puede estar vacío.");
                }

                // Buscar el cupón en la base de datos
                var cuponCliente = await _dataContext.Cupones_Clientes
                    .FirstOrDefaultAsync(c => c.NroCupon == nroCupon);

                if (cuponCliente == null)
                {
                    return NotFound("El cupón no existe o ya ha sido usado.");
                }

                // Crear un historial del uso del cupón
                var cuponHistorial = new Cupones_HistorialModel
                {
                    id_Cupon = cuponCliente.id_Cupon,
                    CodCliente = cuponCliente.CodCliente,
                    NroCupon = cuponCliente.NroCupon,
                    FechaUso = DateTime.Now
                };

                _dataContext.Cupones_Historials.Add(cuponHistorial);

                // Eliminar el cupón de la lista de cupones activos
                _dataContext.Cupones_Clientes.Remove(cuponCliente);

                // Guardar cambios en la base de datos
                await _dataContext.SaveChangesAsync();

                // Validar si el email existe en clientesDto
                if (string.IsNullOrEmpty(clientesDto.Email))
                {
                    return BadRequest("El email del cliente no está disponible.");
                }

                // Enviar email al cliente informando que el cupón fue quemado
                await _sendEmailService.EnviarEmailClienteCuponUsado(clientesDto.Email, nroCupon);

                // Respuesta exitosa
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
        public async Task<IActionResult> ObtenerCuponesActivos([FromQuery] string codCliente)
        {
            try
            {
                // Validación del parámetro codCliente
                if (string.IsNullOrEmpty(codCliente))
                {
                    return BadRequest("El código de cliente es obligatorio.");
                }

                // Consulta para obtener cupones activos
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

                // Verificar si no hay cupones activos
                if (!cuponesActivos.Any())
                {
                    return NotFound($"No se encontraron cupones activos para el cliente con código {codCliente}.");
                }

                // Devolver la respuesta con los cupones activos encontrados
                return Ok(cuponesActivos);
            }
            catch (Exception ex)
            {
                // Manejo de errores generales
                return BadRequest($"Error: {ex.Message}");
            }
        }

    }
}
