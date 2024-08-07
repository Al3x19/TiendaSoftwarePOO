using TiendaSoftware.DataBase.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;


namespace TiendaSoftware.DTOS.Softwares
{
    public class SoftwareCreateDto
    {
        [Display(Name = "Nombre")]
        [StringLength(50)]
        [Required(ErrorMessage = "El {0} de la categoría es requerido.")]

        public string Name { get; set; }

        [Display(Name = "Descripción")]
        [StringLength(500)]
        [MinLength(10, ErrorMessage = "La {0} debe tener al menos {1} caracteres.")]
        public string Description { get; set; }

        [Display(Name = "Precio")]
        [Required(ErrorMessage = "El {0} de la categoría es requerido.")]
        public float Price { get; set; }

        public Guid PublisherId { get; set; }

        public virtual List<string> TagList { get; set; } 
    }
}
