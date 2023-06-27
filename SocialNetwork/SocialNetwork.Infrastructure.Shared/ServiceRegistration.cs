using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SocialNetwork.Core.Application.Interfaces.Services;
using SocialNetwork.Core.Application.Services;
using SocialNetwork.Core.Domain.Settings;
using SocialNetwork.Infrastructure.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Infrastructure.Shared
{
    //Extension Metods - Decorator
    public static class ServiceRegistration
    {
        public static void AddAppSharedInfrastyructure(this IServiceCollection services, IConfiguration configuration)
        {
            #region services
                services.Configure<MailSettings>(configuration.GetSection("MailSettings"));
            services.AddTransient<IEmailService, EmailService>();
            #endregion
        }

    }
}
