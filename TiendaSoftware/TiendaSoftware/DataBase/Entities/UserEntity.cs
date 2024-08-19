using TiendaSoftware.DataBase.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TiendaSoftware.DataBase.Entities
{
    [Table("users", Schema = "dbo")]
    public class UserEntity : BaseEntity
    {
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El {0} de la categoría es requerido.")]
        [StringLength(50)]
        [Column("name")]
        public string Name { get; set; }

        [Display(Name = "Datos bancarios")]
        [Required(ErrorMessage = "Los {0} de la categoría es requerido.")]

        [Column("securitycode")]
        public string Securitycode { get; set; }

        //public virtual IEnumerable<ReviewEntity> Reseñas { get; set; }
        //public virtual IEnumerable<ListEntity> Listas { get; set; }

    }
}
