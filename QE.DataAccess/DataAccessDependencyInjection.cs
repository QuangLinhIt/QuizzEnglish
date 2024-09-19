using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QE.DataAccess.Context;
using QE.DataAccess.Repository.Common.Implement;
using QE.DataAccess.Repository.Common.Interface;
using QE.DataAccess.Repository.Detail.Implement;
using QE.DataAccess.Repository.Detail.Interface;
using QE.Entity.Identity;
using System;


namespace QE.DataAccess
{
    public static class DataAccessDependencyInjection
    {
        #region Khai báo Extension Method kết nối DB
        public static IServiceCollection AddDataAccess(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDataBase(configuration);
            services.AddRepository();
            services.AddIdentityDb();
            return services;
        }
        #endregion

        #region Khai báo Dependency Injection
        private static void AddRepository(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<ICompetitionQuizzRepository, CompetitionQuizzRepository>();
            services.AddScoped<ICompetitionRepository, CompetitionRepository>();
            services.AddScoped<IQuestionQuizzRepository, QuestionQuizzRepository>();
            services.AddScoped<IQuestionRepository, QuestionRepository>();
            services.AddScoped<IQuizzRepository, QuizzRepository>();
            services.AddScoped<IQuizzScoreRepository, QuizzScoreRepository>();
            services.AddScoped<ITopicRepository, TopicRepository>();
            services.AddScoped<IVocabularyRepository, VocabularyRepository>();
            services.AddScoped<IVocabularyTopicRepository, VocabularyTopicRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IVocabularyTopicUnitOfWork, VocabularyTopicUnitOfWork>();
            services.AddScoped<IQuestionQuizzUnitOfWork, QuestionQuizzUnitOfWork>();
        }
        #endregion

        #region Đăng ký AppDbContext, sử dụng kết nối đến MS SQL Server
        private static void AddDataBase(this IServiceCollection services, IConfiguration configuration)
        {
            var databaseConfig = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(databaseConfig));
        }
        #endregion

        #region Đăng ký các dịch vụ của Identity
        private static void AddIdentityDb(this IServiceCollection services)
        {
            services.AddIdentity<AppUser, IdentityRole>()
                    .AddEntityFrameworkStores<ApplicationDbContext>()
                    .AddDefaultTokenProviders();
        }
        #endregion
    }
}
