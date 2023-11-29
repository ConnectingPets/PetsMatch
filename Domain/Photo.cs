namespace Domain
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Microsoft.EntityFrameworkCore;

    [Comment("photo table")]
    public class Photo
    {
        [Comment("photo id")]
        [Key]
        public required string Id { get; set; }

        [Comment("photo url")]
        public required string Url { get; set; }

        [Comment("stores if the photo is main")]
        public required bool IsMain { get; set; }

        [Comment("photo animal id")]
        public Guid? AnimalId { get; set; }

        [Comment("photo animal owner")]
        [ForeignKey(nameof(AnimalId))]
        public Animal? Animal { get; set; }
    }
}
