namespace Domain
{
    using Microsoft.EntityFrameworkCore;
    using System.ComponentModel.DataAnnotations;

    [Comment("passion table")]
    public class Passion
    {
        [Comment("passion id")]
        [Key]
        public int Id { get; set; }

        [Comment("passion name")]
        public required string Name { get; set; }
    }
}
