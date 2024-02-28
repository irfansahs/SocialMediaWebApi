using Microsoft.AspNetCore.Identity;


namespace Media.Domain.Entities.Identity
{
     public class AppUser : IdentityUser
    {
        public int? ConfirmCode { get; set; }
        public string? ProfileImage { get; set; }
        public string? UserColor { get; set; }
        public string? RefhreshToken { get; set; }
        public DateTime? RefhreshTokenEndDate { get; set; }
        public virtual ICollection<Follow> Followers { get; set; }
        public virtual ICollection<Follow> Following { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
    }
}