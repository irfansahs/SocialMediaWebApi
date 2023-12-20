using AutoMapper;
using Commerace.Application.Dto;
using Media.Application;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commerace.Application.Features.Queries.GetAllComments
{
    public class GetAllCommentsQuery : IRequest<List<CommentsViewDto>>
    {

        public int PostId { get; set; }

        public class GetAllCommentsQueryHandler : IRequestHandler<GetAllCommentsQuery, List<CommentsViewDto>>
        {

            private readonly ICommentRepository _repository;
            readonly UserManager<Media.Domain.Identity.AppUser> _userManager;
            private readonly IMapper _mapper;


            public GetAllCommentsQueryHandler(ICommentRepository repository, IMapper mapper, UserManager<Media.Domain.Identity.AppUser> userManager)
            {
                _userManager = userManager;
                _mapper = mapper;
                _repository = repository;
            }


            public async Task<List<CommentsViewDto>> Handle(GetAllCommentsQuery request, CancellationToken cancellationToken)
            {
                var comments = await _repository.GetByNameAsync(x => x.PostId == request.PostId);

                var viewmodel = _mapper.Map<List<CommentsViewDto>>(comments);

                foreach (var post in viewmodel)
                {
                    post.ProfileImage = _userManager.Users.Where(x => x.UserName == post.UserName).FirstOrDefault()?.ProfileImage;
                }

                return viewmodel;
            }
        }
    }
}
