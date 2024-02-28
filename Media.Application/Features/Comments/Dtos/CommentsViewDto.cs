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

    }
}