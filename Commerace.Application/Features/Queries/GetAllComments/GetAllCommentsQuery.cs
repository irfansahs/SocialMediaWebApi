using AutoMapper;
using Commerace.Application.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commerace.Application.Features.Queries.GetAllComments
{
    public class GetAllCommentsQuery : IRequest<object>
    {

        public int PostId { get; set; }

        public class GetAllCommentsQueryHandler : IRequestHandler<GetAllCommentsQuery, object>
        {

            private readonly ICommentRepository _repository;
            private readonly IMapper _mapper;


            public GetAllCommentsQueryHandler(ICommentRepository repository, IMapper mapper)
            {
                _mapper = mapper;
                _repository = repository;
            }


            public async Task<object> Handle(GetAllCommentsQuery request, CancellationToken cancellationToken)
            {
                var yorum =  await _repository.GetByNameAsync(x => x.PostId == request.PostId);

                return yorum;
            }
        }
    }
}
