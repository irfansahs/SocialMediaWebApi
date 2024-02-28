using Media.Persistence.Page;

namespace Media.Application.Features.Posts.Dtos
{
    public class PostListModel : BasePageableModel
    {
        public IList<PostViewDto> items { get; set; }

    }

    

}