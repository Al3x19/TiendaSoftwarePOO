using TiendaSoftware.DataBase.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TiendaSoftware.DTOS.Publishers;

namespace TiendaSoftware.DTOS.Softwares
{
    public class SoftwareDto
    {

        public Guid Id {  get; set; }
        public string Name { get; set; }


        public string Description { get; set; }

        public string img { get; set; }

        public float Price { get; set; }

        public virtual PublisherDto Desarrollador { get; set; }

        public List<string> Tags { get; set; }


    }
}
