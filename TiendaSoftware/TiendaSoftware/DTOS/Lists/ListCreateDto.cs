using TiendaSoftware.DataBase.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using TiendaSoftware.DTOS.Lists;

namespace TiendaSoftware.DTOS.Lists
{
    public class ListCreateDto
    {
        [Display(Name = "Titulo")]
        [StringLength(100, ErrorMessage = " El {0} debe tener menos de {1}")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        public string Title { get; set; }

        // TODO: DEFINIR LA RELACION ENTRE USUARIOS Y POST
        [Display(Name ="Autor")]
        [StringLength(100, ErrorMessage ="El {0} no puede tener mas de{1}.")]
        [Required(ErrorMessage = "El {0} es requerido.")]

        public string AuthorId { get; set; }

        public DateTime PublicationDate { get; set; }

        public Guid CategoryId { get; set; }

        public string Content { get; set; }

        public virtual List<string> Tags { get; set; }
    }
}
