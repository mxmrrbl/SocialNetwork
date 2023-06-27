using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SocialNetwork.Core.Application.Interfaces.Repositories;
using SocialNetwork.Infrastructure.Persistence.Contexts;
using SocialNetwork.Infrastructure.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Infrastructure.Persistence
{
    //Extension Metods - Decorator
    public static class ServiceRegistration
    {
        public static void AddPersistenceInfrastructure(this IServiceCollection service, IConfiguration configuration)
        {
            #region Contexts

            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                service.AddDbContext<ApplicationContext>(options => options.UseInMemoryDatabase("ApplicationDb"));
            }
            else
            {
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                service.AddDbContext<ApplicationContext>(options =>
                                                         options.UseSqlServer(connectionString,
                                                         m => m.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName)));
            }

            #endregion

            #region Repositories

            service.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            service.AddTransient<IUserRepository, UserRepository>();
            service.AddTransient<IPostRepository, PostRepository>();
            service.AddTransient<ICommentRepository, CommentRepository>();
            service.AddTransient<IFriendshipRepository, FriendshipRepository>();

            #endregion
        }

    }
}
