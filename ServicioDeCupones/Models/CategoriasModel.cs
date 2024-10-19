using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServicioDeCupones.Models
{
    [Table("Categorias")]
    public class CategoriasModel
    {
        [Key]
        public int id_Categorias { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<Cupones_CategoriasModel>? Cupones_Categorias { get; set; }

    }
}
