using AutoMapper;
using Commerace.Application.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commerace.Application.Features.Queries.GetAllUsers
{
    public class GetAllUsersQuery : IRequest<List<object>>
    {

        public string UserName { get; set; }
        public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<object>>
        {

            private readonly IMapper _mapper;
            private readonly IPostRepository _repository;


            public GetAllUsersQueryHandler(IMapper mapper, IPostRepository repository)
            {
                _mapper = mapper;
                _repository = repository;
            }


            public async Task<List<object>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
            {

                var users = await _repository.GetAllAsync();

                var viewmodel = _mapper.Map<List<object>>(users);

                return viewmodel;
            }
        }
    }
}
