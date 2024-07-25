using TiendaSoftware.DataBase.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;


namespace TiendaSoftware.DTOS.Publishers
{
    public class PublisherCreateDto
    {
       // public Guid Id { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El {0} de la categoría es requerido.")]
        public string Name { get; set; }

        [Display(Name = "Descripción")]
        [StringLength(250)]
        [MinLength(10, ErrorMessage = "La {0} debe tener al menos {1} caracteres.")]
        public string Description { get; set; }

        [Display(Name = "Datos bancarios")]
        [StringLength(12)] 
        [MinLength(12, ErrorMessage = "Los {0} debe tener al menos {1} caracteres.")]
        [Required(ErrorMessage = "Los {0} de la categoría es requerido.")]

        public string Securitycode { get; set; }

     
    }
}
