using ServicioDeCupones.Interfaces;
using System.Drawing;
using System.Net;
using System.Net.Mail;

namespace ServicioDeCupones.Services
{
    public class SendEmailService : ISendEmailService
    {
        public async Task EnviarEmailCliente(string emailCliente, string numeroCupon)
        {
            string emailDesde = "serviciocupones@gmail.com";
            string emailClave = "srak ldya lrrd rxqs";
            string servicioGoogle = "smtp.gmail.com";

            try
            {
                SmtpClient smtpClient = new SmtpClient(servicioGoogle);
                smtpClient.Port = 587;
                smtpClient.Credentials = new NetworkCredential(emailDesde, emailClave);
                smtpClient.EnableSsl = true;


                // mensaje que se envia al cliente
                MailMessage mensaje = new MailMessage();
                mensaje.From = new MailAddress(emailDesde, "Servicio Cupones");
                mensaje.To.Add(emailCliente);
                mensaje.Subject = "Cupones";
                mensaje.Body = $"Su número de cupón es: {numeroCupon}";

                await smtpClient.SendMailAsync(mensaje);



            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }
    }
}
