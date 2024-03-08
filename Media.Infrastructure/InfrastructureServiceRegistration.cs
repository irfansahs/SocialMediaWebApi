using FluentValidation;
using Media.Infrastructure.Contexts;
using Media.Infrastructure.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Media.Domain.Entities.Identity;
using Media.Application.Services.Repositories;
using Media.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Media.Infrastructure.Services;
using Media.Application.Services;

namespace Media.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
         public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {



    services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<UserDbContext>().AddDefaultTokenProviders();

    services.AddDbContext<UserDbContext>(options => { options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")); });


            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<ILikeRepository, LikeRepository>();
            services.AddScoped<IFollowRepository, FollowRepository>();
            services.AddScoped<ITagRepository, TagRepository>();
            services.AddScoped<ITokenHandler, TokenHandler>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IEmotionAnalyzeService, EmotionAnalyzeService>();
            services.AddScoped<ITranslateService, TranslateService>();

            return services;

        }
    }
}