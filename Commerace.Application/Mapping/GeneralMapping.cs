using AutoMapper;
using Commerace.Application.Dto;
using Media.Application.Dto;
using Media.Application.Features.Commands.Like.CreateLike;
using Media.Application.Features.Commands.Posts.CreatePost;
using Media.Domain;
using Media.Domain.Identity;
using Media.Persistence.Page;
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


            CreateMap<AppUser, UserResponseDto>().ReverseMap();

            CreateMap<Tag, TrendsResponseDto>().ReverseMap();

            CreateMap<Like, CreateLikeCommand>().ReverseMap();

            CreateMap<Post, PostViewDto>().ForMember(c => c.LikeCount, opt => opt.MapFrom(c => c.Likes.Count))
                                          .ForMember(c => c.IsLiked, opt => opt.MapFrom(c => c.Likes.Count))
                                          .ForMember(c=>c.UserName,opt=>opt.MapFrom(c=>c.User.UserName))
                                          .ForMember(c => c.ProfileImage, opt => opt.MapFrom(c => c.User.ProfileImage))
                                          .ForMember(c => c.CommentsCount, opt => opt.MapFrom(c => c.Comments.Count))
                                          .ForMember(c => c.UserColor, opt => opt.MapFrom(c => c.User.UserColor)).ReverseMap();

            CreateMap<IPaginate<Post>,PostListModel>().ReverseMap();

        }


    }
}
