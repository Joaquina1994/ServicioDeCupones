using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServicioDeCupones.Models
{
    [Table("Cupones_Categorias")]
    public class Cupones_CategoriasModel
    {
        [Key]
        public int Id_Cupones_Categorias { get; set; }
        public int Id_Cupon {  get; set; }
        public int Id_Categoria { get; set; }

        [ForeignKey("Id_Categoria")]
        public virtual CategoriasModel? Categorias { get; set; }

        [ForeignKey("Id_Cupon")]
        public virtual CuponesModel? Cupon { get; set; } 


    }
}
