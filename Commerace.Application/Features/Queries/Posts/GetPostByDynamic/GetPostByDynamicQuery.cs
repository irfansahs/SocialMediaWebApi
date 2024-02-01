using AutoMapper;
using Commerace.Application.Dto;
using Commerace.Application;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Media.Persistence.Dynamic;
using Media.Persistence.Page;
using Media.Application.Requests;
using Media.Domain;
using Media.Application.Dto;
using System.Linq.Dynamic.Core;
using Org.BouncyCastle.Asn1.Cms;

namespace Media.Application.Features.Queries.Posts.GetPostByDynamic
{
    public class GetPostByDynamicQuery : IRequest<PostListModel>
    {
        public string UserId { get; set; }
        public PageRequest PageRequest { get; set; }
     //  public Dynamic dynamic { get; set; }

        public class GetPostByDynamicQueryHandler : IRequestHandler<GetPostByDynamicQuery, PostListModel>
        {
            private readonly IPostRepository _repository;
            private readonly IMapper _mapper;

            public GetPostByDynamicQueryHandler(IPostRepository repository, IMapper mapper)
            {
                _mapper = mapper;
                _repository = repository;
            }

            public async Task<PostListModel> Handle(GetPostByDynamicQuery request, CancellationToken cancellationToken)
            {

                IPaginate<Post> paginatedPosts = _repository.GetList(
                    include: t => t.Include(c => c.Comments).Include(c=>c.Likes).Include(c=>c.User),
                    index: request.PageRequest.Page,
                    size: request.PageRequest.PageSize
                );

                PostListModel postListModel = _mapper.Map<PostListModel>(paginatedPosts);

                return postListModel;
            }



        }
    }
}
