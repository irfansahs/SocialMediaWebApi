using Media.Domain.Entities.Identity;

namespace Media.Domain.Entities
{
    public class Comment : BaseEntity
    {
        public string Content { get; set; }
        public string? Emotion { get; set; }
        public string? Polarity { get; set; }

        public int PostId { get; set; }
        public virtual Post Post { get; set; }

        public string UserId { get; set; }
        public virtual AppUser User { get; set; }
    }
}