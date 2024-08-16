using TiendaSoftware.DataBase.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;


namespace TiendaSoftware.DTOS.Reviews
{
    public class ReviewDto
    {

        public string Content { get; set; }


        public int Score { get; set; }


        public Guid SoftwareId { get; set; }



        public virtual SoftwareEntity Software { get; set; }



        public virtual UserEntity Creator { get; set; }


    }
}
