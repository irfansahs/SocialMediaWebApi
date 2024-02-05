using FluentValidation;
using Media.Application.Abstractions.Services;
using Media.Domain.Identity;
using Media.Infrastructure.Contexts;
using Media.Infrastructure.Repositories;
using Media.Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Media.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {

        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {


            services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<UserDbContext>().AddDefaultTokenProviders();

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddScoped<TokenHandler>();

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddScoped<ITokenHandler, TokenHandler>();
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<ILikeRepository, LikeRepository>();
            services.AddScoped<IFollowRepository, FollowRepository>();
            services.AddScoped<ITagRepository, TagRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IEmailService, EmailService>();

            return services;

        }

    }
}
