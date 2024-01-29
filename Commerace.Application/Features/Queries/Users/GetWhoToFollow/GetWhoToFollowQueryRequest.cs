using MediatR;

namespace Media.Application.Features.Queries.Users.GetWhoToFollow
{
    public class GetWhoToFollowQueryRequest : IRequest<List<GetWhoToFollowQueryResponse>>
    {
        public string UserId { get; set; }
    }
}