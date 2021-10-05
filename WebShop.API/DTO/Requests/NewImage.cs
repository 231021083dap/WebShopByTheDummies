using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebShop.API.DTO.Requests
{
    public class NewImage
    {
        [Required]
        public List<string> Path { get; set; }
    }
}
