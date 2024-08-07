﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TiendaSoftware.DataBase.Entities
{
	[Table("software", Schema = "dbo")]
    public class SoftwareEntity : BaseEntity
    {[Display(Name = "Nombre")]
        [Required(ErrorMessage = "El {0} de la categoría es requerido.")]
        [StringLength(50)]
        [Column("name")]
        public string Name { get; set; }

        [Display(Name = "Descripción")]
        [MinLength(10, ErrorMessage = "La {0} debe tener al menos {1} caracteres.")]
        [StringLength(500)]
        [Column("description")]
        public string Description { get; set; }

        [Display(Name = "Precio")]
        [Required(ErrorMessage = "El {0} de la categoría es requerido.")]
        [Column("price")]
        public float Price { get; set; }
        
		[Column("Publisher_id")]	
        public Guid PublisherId { get; set; }


        [ForeignKey(nameof(PublisherId))]
        public virtual PublisherEntity Desarrollador { get; set; }


        //public virtual IEnumerable<UserDownloadsEntity> Usuarios { get; set; }
        public virtual IEnumerable<SoftwareTagsEntity> Tags { get; set; }
        //public virtual IEnumerable<ListSoftwareEntity> Listas { get; set; }   
    }
}
