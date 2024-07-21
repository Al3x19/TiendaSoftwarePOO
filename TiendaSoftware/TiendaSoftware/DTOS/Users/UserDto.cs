using TiendaSoftware.DataBase.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TiendaSoftware.DTOS.Users;

namespace TiendaSoftware.API.DTOS.Posts
{
    public class UserDto
    {
       
        public string Title { get; set; }

        public string AuthorId { get; set; }
        // cambiar AuthoId por UserDto

        public DateTime PublicationDate { get; set; }

        public virtual CategoryDto Category { get; set; }

        public string Content { get; set; }

        public virtual List<string> Tags { get; set; }
    }
}
