using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebShop.API.Database.Entities;

namespace WebShop.API.DTO.Requests
{
    public class NewImage
    {
        [Required]
        public List<string> Path { get; set; }
    }
}
