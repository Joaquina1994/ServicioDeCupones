﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServicioDeCupones.Models
{
    [Table("Cupones")]
    public class CuponesModel
    {
        [Key]
        public int id_Cupon { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal PorcentajeDto { get; set; }
        public decimal ImportePromo { get; set; }
        public DateOnly FechaInicio { get; set; }
        public DateOnly FechaFin {  get; set; }
        public int Id_Tipo_Cupon { get; set; }
        public bool Activo {  get; set; }

        public virtual ICollection<Cupones_CategoriasModel>? Cupones_Categorias { get; set; }

        [ForeignKey("Id_Tipo_Cupon")]
        public virtual Tipo_CuponModel? Tipo_Cupon { get; set;}

    }
}
