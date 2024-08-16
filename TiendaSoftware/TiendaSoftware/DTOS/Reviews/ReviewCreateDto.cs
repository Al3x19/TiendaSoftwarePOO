using System;
using System.Linq;
using System.Collections.Generic;

namespace TiendaSoftware.DTOS.Reviews;

public class ReviewCreateDto
{



    public string Content { get; set; }




    public int Score { get; set; }


    public Guid SoftwareId { get; set; }




    public Guid UserId { get; set; }





}
