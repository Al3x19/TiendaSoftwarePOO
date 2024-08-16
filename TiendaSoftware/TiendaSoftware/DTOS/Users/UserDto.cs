using TiendaSoftware.DataBase.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;


namespace TiendaSoftware.DTOS.Users
{
    public class UserDto
    {
        public string Name { get; set; }

        public string Securitycode { get; set; }

        public List<string> Compras { get; set; }
    }
}
