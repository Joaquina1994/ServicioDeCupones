using ClientesApi.Interfaces;
using ClientesApi.Models.DTO;
using Newtonsoft.Json;
using System.Text;

namespace ClientesApi.Services
{
    public class ClienteService : IClienteService
    {
        public async Task<string> ObtenerCuponesActivos(string codCliente)
        {
            try
            {
                var cliente = new HttpClient();
                var respuesta = await cliente.GetAsync($"https://localhost:7131/api/SolicitudCupones/ObtenerCuponesActivos/{codCliente}");

                if (respuesta.IsSuccessStatusCode)
                {
                    return await respuesta.Content.ReadAsStringAsync();
                }
                else
                {
                    var error = await respuesta.Content.ReadAsStringAsync();
                    throw new Exception($"{error}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
        }

        public async Task<string> QuemarCupon(string nroCupon)
        {
            try
            {
                var cliente = new HttpClient();
                var contenido = new StringContent(JsonConvert.SerializeObject(new { NroCupon = nroCupon }), Encoding.UTF8, "application/json");
                var respuesta = await cliente.PostAsync("https://localhost:7131/api/SolicitudCupones/QuemarCupon", contenido);

                if (respuesta.IsSuccessStatusCode)
                {
                    return await respuesta.Content.ReadAsStringAsync();
                }
                else
                {
                    var error = await respuesta.Content.ReadAsStringAsync();
                    throw new Exception($"{error}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
        }

        public async Task<string> SolicitarCupon(ClientesDto clientesDto)
        {
            try
            {
               
                var jsonCliente = JsonConvert.SerializeObject(clientesDto);
                var contenido = new StringContent(jsonCliente, Encoding.UTF8, "application/json");
                var cliente = new HttpClient();
                var respuesta = await cliente.PostAsync("https://localhost:7131/api/SolicitudCupones/SolicitarCupon", contenido);

                if (respuesta.IsSuccessStatusCode)
                {
                    var msg = await respuesta.Content.ReadAsStringAsync();  
                    return msg;
                }
                else
                {
                    var error = await respuesta.Content.ReadAsStringAsync(); 
                    throw new Exception($"{error}");  
                }

            }catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
               
            }
        }
    }
}
