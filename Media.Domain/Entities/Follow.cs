using Media.Domain.Entities.Identity;

namespace Media.Domain.Entities
{
     public class Follow : BaseEntity
    {
        public string FollowerId { get; set; }
        public virtual AppUser Follower { get; set; }

        public string FollowingId { get; set; }
        public virtual AppUser Following { get; set; }

    }
}