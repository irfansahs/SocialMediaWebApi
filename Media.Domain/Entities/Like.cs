using Media.Domain.Entities.Identity;

namespace Media.Domain.Entities
{
    public class Like : BaseEntity
    {
        public int PostId { get; set; }
        public virtual Post Post { get; set; }
        public string UserId { get; set; }
        public virtual AppUser User { get; set; }

    }
}