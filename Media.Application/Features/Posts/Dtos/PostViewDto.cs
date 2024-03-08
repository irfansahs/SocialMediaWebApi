namespace Media.Application.Features.Posts.Dtos
{
    public class PostViewDto
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public string? UserName { get; set; }
        public string? ProfileImage { get; set; }
        public string? UserColor { get; set; }
        public int? LikeCount { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool? IsLiked { get; set; }
        public string? Emotion { get; set; }
        public string? Polarity { get; set; }
        public int? CommentsCount { get; set; }
        public string? Content { get; set; }

    }
}