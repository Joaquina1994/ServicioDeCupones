using ServicioDeCupones.Interfaces;

namespace ServicioDeCupones.Services
{
    public class CuponesService : ICuponesService
    {
        public async Task<string> GenerarNumeroCupon()
        {
            //crear n cupon aleatorio
            var NumeroCupon = "111-111-111";

            return NumeroCupon;
            
        }
    }
}
