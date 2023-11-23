using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WillsPlatform.Application;
using WillsPlatform.Application.Repositories;
using WillsPlatform.Application.Services;
using WillsPlatform.Infrastructure;
using WillsPlatform.Infrastructure.Repositories;
using WillsPlatform.Infrastructure.Services;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            #region -- Repositories Registration --
            services.AddScoped<IQuestionRepository, QuestionRepository>();
            services.AddScoped<ITemplateRepository, TemplateRepository>();
            services.AddScoped<IFormRepository, FormRepository>();
            #endregion

            #region -- Services Registration --
            services.AddScoped<IFormService, FormService>();
            services.AddScoped<IQuestionService, QuestionService>();
            #endregion

            services.AddDbContext<ApplicationDbContext>(option =>
            {
                option.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });          
            return services;
        }
    }
}
