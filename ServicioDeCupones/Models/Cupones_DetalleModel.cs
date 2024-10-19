using System.ComponentModel.DataAnnotations.Schema;

namespace ServicioDeCupones.Models
{
    [Table("Cupones_Detalle")]
    public class Cupones_DetalleModel
    {
        public int id_Cupon { get; set; }
        public int id_Articulo { get; set; }
        public int Cantidad { get; set; }
    }
}
