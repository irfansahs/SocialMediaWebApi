

using AutoMapper;
using Media.Application.Features.Posts.Dtos;
using Media.Application.Features.Posts.Queries;
using Media.Domain.Entities;
using Media.Persistence.Page;
using Org.BouncyCastle.Asn1.Ocsp;
using Org.BouncyCastle.Crypto.Agreement.Srp;

namespace Media.Application.Features.Posts.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {

            CreateMap<Post, PostViewDto>().ForMember(c => c.LikeCount, opt => opt.MapFrom(c => c.Likes.Count))
                                         .ForMember(c => c.IsLiked, opt => opt.Ignore())
                                         .ForMember(c => c.UserName, opt => opt.MapFrom(c => c.User.UserName))
                                         .ForMember(c => c.ProfileImage, opt => opt.MapFrom(c => c.User.ProfileImage))
                                         .ForMember(c => c.CommentsCount, opt => opt.MapFrom(c => c.Comments.Count))
                                         .ForMember(c => c.UserColor, opt => opt.MapFrom(c => c.User.UserColor))
                                         .ForMember(c => c.CreatedOn, opt => opt.MapFrom(c => c.CreatedOn))
                                         .ForMember(c => c.Emotion, opt => opt.MapFrom(c => c.Emotion))
                                         .ForMember(c => c.Polarity, opt => opt.MapFrom(c => c.Polarity))
                                         .ReverseMap();

            CreateMap<IPaginate<Post>, PostListModel>();

        }
    }
}