namespace Media.Application.Features.User.Dtos
{
     public class UserViewDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool Succeeded { get; internal set; }
    }
}