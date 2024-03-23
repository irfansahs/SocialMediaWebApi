namespace Media.Application.Features.Comments.Dtos
{
    public class CommentsViewDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedOn { get; set; }
        public int PostId { get; set; }
        public string? UserName { get; set; }
        public string? ProfileImage { get; set; }
        public int? LikeCount { get; set; }
        public bool? IsLiked { get; set; }
        public bool? IsFollow { get; set; }
        public string? Emotion { get; set; }
        public string? Polarity { get; set; }
        public string? SourceLanguageCode { get; set; }

    }
}