namespace Application.DTOs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class UserDto
    {
        public string? Photo { get; set; }

        public required string Name { get; set; }

        public required string Token {  get; set; } 
    }
}
