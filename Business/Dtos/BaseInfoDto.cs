﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Dtos
{
    public class BaseInfoDto
    {
 
        public string FirstName { get; set; } = null!;


       
        public string LastName { get; set; } = null!;


        
        public string Email { get; set; } = null!;


     
        public string? Phone { get; set; }


        public string? Biography { get; set; }
    }
}
