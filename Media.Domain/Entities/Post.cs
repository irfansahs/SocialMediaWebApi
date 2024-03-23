using Media.Domain.Entities.Identity;

namespace Media.Domain.Entities
{
    public class Post : BaseEntity
    {
        public string UserId { get; set; }
        public virtual AppUser User { get; set; }
        public string Content { get; set; }
        public string? Emotion { get; set; }
        public string? Polarity { get; set; }
        public string? PostImage { get; set; }
        public string? SourceLanguageCode { get; set; }
        public string? TranslatedPost { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }

    }
}