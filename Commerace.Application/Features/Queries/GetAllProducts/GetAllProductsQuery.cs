using AutoMapper;
using Commerace.Application.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commerace.Application.Features.Queries.GetAllProducts
{
    public class GetAllProductsQuery : IRequest<List<ProductViewDto>>
    {
        public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, List<ProductViewDto>>
        {

            private readonly IProductRepository _repository;
            private readonly IMapper _mapper;


            public GetAllProductsQueryHandler(IProductRepository repository, IMapper mapper)
            {
                _mapper = mapper;
                _repository = repository;
            }


            public async Task<List<ProductViewDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
            {
                var products = await _repository.GetAllAsync();

                var viewmodel = _mapper.Map<List<ProductViewDto>>(products);

                return viewmodel;
            }
        }
    }
}
