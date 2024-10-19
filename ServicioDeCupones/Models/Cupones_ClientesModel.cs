using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServicioDeCupones.Models
{
    [Table("Cupones_Clientes")]
    public class Cupones_ClientesModel
    {
        public int id_Cupon { get; set; }
        [Key]
        public string NroCupon { get; set; }
        public DateTime FechaAsignado { get; set; }
        public string CodCliente { get; set; }



    }
}
