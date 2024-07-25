﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TiendaSoftware.DataBase.Entities
{
	    [Table("reviews", Schema = "dbo")]
    public class ReviewEntity : BaseEntity
    {
	    [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El {0} de la categoría es requerido.")]
        [StringLength(50)]
        [Column("name")]
        public string Name { get; set; }

        [Display(Name = "Contenido")]
        [MinLength(10, ErrorMessage = "el {0} debe tener al menos {1} caracteres.")]
        [StringLength(500)]
        [Column("content")]
        public string Content { get; set; }

        [Display(Name = "Puntuacion")]
        [Required(ErrorMessage = "La {0} de la categoría es requerido.")]
        [Column("score")]
        public int Score { get; set; }

        [Column("software_id")]
        public Guid SoftwareId { get; set; }


        [ForeignKey(nameof(SoftwareId))]
        public virtual SoftwareEntity Software { get; set; }

        [Column("user_id")]
        public Guid UserId { get; set; }


        [ForeignKey(nameof(UserId))]
        public virtual UserEntity Creator { get; set; }


       
    }
}
