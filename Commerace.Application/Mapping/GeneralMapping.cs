using AutoMapper;
using Commerace.Application.Dto;
using Commerace.Application.Features.Queries.GetAllProducts;
using Media.Application.Dto;
using Media.Application.Features.Commands.Like.CreateLike;
using Media.Application.Features.Commands.Posts.CreatePost;
using Media.Domain;
using Media.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commerace.Application.Mapping
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping() 
        {
            CreateMap<Product, ProductViewDto>().ReverseMap();

            CreateMap<Comment, CreateCommentCommand>().ReverseMap();

            CreateMap<Comment, CommentsViewDto>().ReverseMap();
            
            CreateMap<Post, CreatePostCommand>().ReverseMap();

            CreateMap<Post, PostViewDto>().ReverseMap();

            CreateMap<AppUser, UserResponseDto>().ReverseMap();

            CreateMap<Tag, TrendsResponseDto>().ReverseMap();

            CreateMap<Like, CreateLikeCommand>().ReverseMap();

        }


    }
}
