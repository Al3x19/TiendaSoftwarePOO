using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TiendaSoftware.DataBase.Entities;
[Table("tags", Schema = "dbo")]
public class TagEntity : BaseEntity
{         
        [StringLength(50)]
        [Required]
        [Column("name")]
        public string Name { get; set; }

        [StringLength(250)]
        [Column("description")]
        public string Description { get; set; }

        public virtual IEnumerable<SoftwareTagsEntity> Software { get; set; }
  
}
