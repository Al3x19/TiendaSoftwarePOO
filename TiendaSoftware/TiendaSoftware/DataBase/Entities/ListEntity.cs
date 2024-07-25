
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using TiendaSoftware.DataBase.Entities;

namespace TiendaSoftware.DataBase.Entities
{
	[Table("lists", Schema = "dbo")]
    public class ListEntity : BaseEntity
    {
		    
		[Display(Name = "Nombre")]
        [Required(ErrorMessage = "El {0} de la categoría es requerido.")]
        [StringLength(50)]
        [Column("name")]
        public string Name { get; set; }

        [Display(Name = "Descripción")]
        [MinLength(10, ErrorMessage = "La {0} debe tener al menos {1} caracteres.")]
        [StringLength(500)]
        [Column("description")]
        public string Description { get; set; }

        [Column("user_id")]
        public Guid UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual UserEntity Creator { get; set; }

        public virtual IEnumerable<ListSoftwareEntity> Software { get; set; }
    }
}
