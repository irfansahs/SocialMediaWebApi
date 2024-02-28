using AutoMapper;
using Media.Domain.Entities;
using Media.Domain.Entities.Identity;
using Media.Application.Features.Comments.Commands;
using Media.Application.Features.Posts.Commands;
using Media.Application.Features.Likes.Commands;
using Media.Application.Features.User.Dtos;
using Media.Application.Features.Tags.Dtos;
using Media.Application.Features.Comments.Dtos;



namespace Media.Application.Mapping
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping() 
        {

            CreateMap<Comment, CreateCommentCommand>().ReverseMap();

            CreateMap<Comment, CommentsViewDto>().ReverseMap();
            
            CreateMap<Post, CreatePostCommand>().ReverseMap();


            CreateMap<AppUser, UserResponseDto>().ReverseMap();

            CreateMap<Tag, TrendsResponseDto>().ReverseMap();

            CreateMap<Like, CreateLikeCommand>().ReverseMap();

      

        }


    }
}