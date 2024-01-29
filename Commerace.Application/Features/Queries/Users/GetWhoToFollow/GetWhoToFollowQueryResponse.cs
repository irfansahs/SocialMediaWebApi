namespace Media.Application.Features.Queries.Users.GetWhoToFollow
{
    public class GetWhoToFollowQueryResponse
    {
        public string UserName { get; set; }
        public string ProfileImage { get; set; }
        public string UserColor { get; set; }
        public bool IsFollow { get; set; }
    }
}
