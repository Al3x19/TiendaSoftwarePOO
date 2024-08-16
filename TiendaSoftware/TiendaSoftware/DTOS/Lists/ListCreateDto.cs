using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TiendaSoftware.DTOS.Lists;

public class ListCreateDto
{
    [Display(Name = "Nombre")]
    [Required(ErrorMessage = "El {0} de la categoría es requerido.")]
    [StringLength(50)]
    public string Name { get; set; }

    [Display(Name = "Descripción")]
    [MinLength(10, ErrorMessage = "La {0} debe tener al menos {1} caracteres.")]
    [StringLength(500)]
    public string Description { get; set; }

    public Guid UserId { get; set; }

    public List<string> Softwares { get; set; }

}
