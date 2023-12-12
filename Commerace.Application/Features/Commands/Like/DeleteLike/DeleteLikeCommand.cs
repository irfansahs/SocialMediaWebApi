using AutoMapper;
using Commerace.Application.Features.Queries.GetAllProducts;
using Media.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Media.Application.Features.Commands.Like.CreateLike
{
    public class DeleteLikeCommand : IRequest<object>
    {
        public string UserName { get; set; }
        public int PostId { get; set; }

        public class DeleteLikeCommandHandler : IRequestHandler<DeleteLikeCommand, object>
        {

            private readonly ILikeRepository _repository;
            private readonly IMapper _mapper;


            public DeleteLikeCommandHandler(ILikeRepository repository, IMapper mapper)
            {
                _mapper = mapper;
                _repository = repository;
            }


            public async Task<object> Handle(DeleteLikeCommand request, CancellationToken cancellationToken)
            {


                var Delete = await _repository.DeleteLike(request.PostId,request.UserName);

                return Delete;
            }


        }

    }
}
