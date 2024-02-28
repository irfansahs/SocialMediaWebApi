using Media.Domain.Entities;

namespace Media.Application.Features.Posts.Dtos
{
    public class PostAndComments
    {

        public int Id { get; set; }
        public string? Content { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? UserName { get; set; }
        public string? ProfileImage { get; set; }
        public string? UserColor { get; set; }
        public int? LikeCount { get; set; }
        public bool? IsLiked { get; set; }
        public int? CommentsCount { get; set; }
        public ICollection<Comment> Comments { get; set; }


    }
}