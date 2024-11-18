using ServicioDeCupones.Interfaces;

namespace ServicioDeCupones.Services
{
    public class CuponesService : ICuponesService
    {
        public async Task<string> GenerarNumeroCupon()
        {
            Random random = new Random();
            string numeroCupon = $"{random.Next(100, 1000)}-{random.Next(100, 1000)}-{random.Next(100, 1000)}";
            return numeroCupon;

        }
    }
}
