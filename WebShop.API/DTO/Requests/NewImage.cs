using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebShop.API.DTO.Requests
{
    public class NewImage
    {
        [Required]
        public List<string> Path { get; set; }
        
    }
}
