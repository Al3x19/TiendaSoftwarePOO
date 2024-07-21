using TiendaSoftware.API.Database.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TiendaSoftware.API.DTOS.Reviews;

namespace TiendaSoftware.API.DTOS.Reviews
{
    public class ReviewDto
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
