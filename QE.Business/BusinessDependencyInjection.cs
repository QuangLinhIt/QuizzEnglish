using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QE.Business.Logic.Competition;
using QE.Business.Logic.CompetitionQuizz;
using QE.Business.Logic.Question;
using QE.Business.Logic.Quizz;
using QE.Business.Logic.QuizzScore;
using QE.Business.Logic.Topic;
using QE.Business.Logic.Vocabulary;

namespace QE.Business
{
    public static class BusinessDependencyInjection
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection services)
        {
            services.AddScoped<ICompetitionBo, CompetitionBo>();
            services.AddScoped<ICompetitionQuizzBo, CompetitionQuizzBo>();
            services.AddScoped<IQuestionBo, QuestionBo>();
            services.AddScoped<IQuizzBo, QuizzBo>();
            services.AddScoped<IQuizzScoreBo, QuizzScoreBo>();
            services.AddScoped<ITopicBo, TopicBo>();
            services.AddScoped<IVocabularyBo, VocabularyBo>();

            return services;
        }
    }
}
