using AutoMapper;
using Commerace.Application;
using Commerace.Application.Dto;
using Media.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Media.Application.Features.Commands.User.CreateProduct
{
    public class CreateProductCommand : IRequest<ProductViewDto>
    {
        public string? Name { get; set; }
        public string Description { get; set; }


        public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductViewDto>
        {

            private readonly IProductRepository _repository;
            private readonly IMapper _mapper;


            public CreateProductCommandHandler(IProductRepository repository, IMapper mapper)
            {
                _mapper = mapper;
                _repository = repository;
            }


            public async Task<ProductViewDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
            {
                var product = _mapper.Map<Product>(request);
                await _repository.AddAsync(product);


                return null;
            }
        }
    }
}
