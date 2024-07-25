using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TiendaSoftware.DataBase.Entities;
[Table("user_downloads", Schema = "dbo")]
public class UserDownloadsEntity : BaseEntity
{         
        [Column("User_id")]
        public Guid UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual UserEntity User { get; set; }

        [Column("Software_id")]
        public Guid SoftwareId { get; set; }
        
        [ForeignKey(nameof(SoftwareId))]
        public virtual SoftwareEntity Software { get; set; }

}
