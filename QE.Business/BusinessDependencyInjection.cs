using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QE.Business.Logic.Topic;
using QE.Business.Logic.Vocabulary;

namespace QE.Business
{
    public static class BusinessDependencyInjection
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection services)
        {
            services.AddScoped<ITopicBo, TopicBo>();
            services.AddScoped<IVocabularyBo, VocabularyBo>();

            return services;
        }
    }
}
