using TiendaSoftware.DataBase.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TiendaSoftware.DTOS.Publishers;

namespace TiendaSoftware.DTOS.Publishers
{
    public class PublisherDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public string Securitycode { get; set; }

    }
}
