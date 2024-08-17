using TiendaSoftware.DataBase.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using TiendaSoftware.DTOS.Users;


namespace TiendaSoftware.DTOS.Lists
{
    public class ListDto
    {

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El {0} de la categoría es requerido.")]
        [StringLength(50)]
        public string Name { get; set; }

        [Display(Name = "Descripción")]
        [MinLength(10, ErrorMessage = "La {0} debe tener al menos {1} caracteres.")]
        [StringLength(500)]
        public string Description { get; set; }

        public UserDto User { get; set; }

        public List<string> Softwares { get; set; }

    }
}
