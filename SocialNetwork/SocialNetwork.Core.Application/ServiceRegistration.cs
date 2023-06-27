using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SocialNetwork.Core.Application.Interfaces.Services;
using SocialNetwork.Core.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Infrastructure.Persistence
{
    //Extension Metods - Decorator
    public static class ServiceRegistration
    {
        public static void AddApplicationLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            #region services

            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IPostService, PostService>();
            services.AddTransient<ICommentService, CommentService>();
            services.AddTransient<IFriendshipService, FriendshipService>();

            #endregion
        }

    }
}
