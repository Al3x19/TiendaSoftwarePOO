using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TiendaSoftware.DataBase.Entities;
[Table("software_tags", Schema = "dbo")]
public class SoftwareTagsEntity : BaseEntity
{

    [Column("Software_id")]
    public Guid SoftwareId { get; set; }

    [ForeignKey(nameof(SoftwareId))]
    public virtual SoftwareEntity Software { get; set; }

    [Column("Tag_id")]
    public Guid TagId { get; set; }

    [ForeignKey(nameof(TagId))]
    public virtual TagEntity tags { get; set; }

}
