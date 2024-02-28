namespace Media.Application.Features.User.Dtos
{
    public class UserResponseDto
    {

        public string UserName { get; set; }
        public string ProfileImage { get; set; }
        public string UserColor { get; set; }
        public bool IsFollow { get; set; }
        public int FollowCount { get; set; }
        public int FollowersCount { get; set; }
        public int PostsCount { get; set; }

    }
}