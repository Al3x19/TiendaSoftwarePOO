using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TiendaSoftware.DataBase.Entities;
[Table("software_lists", Schema = "dbo")]
public class ListSoftwareEntity : BaseEntity
{
    [Column("Software_id")]
    public Guid SoftwareId { get; set; }

    [ForeignKey(nameof(SoftwareId))]
    public virtual SoftwareEntity Software { get; set; }

    [Column("List_id")]
    public Guid ListId { get; set; }

    [ForeignKey(nameof(ListId))]
    public virtual ListEntity List { get; set; }

}
