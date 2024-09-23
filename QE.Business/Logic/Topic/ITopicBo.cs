using QE.Business.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QE.Business.Logic.Topic
{
    public interface ITopicBo
    {
        Task<IEnumerable<TopicModel>> GetAll(int pageIndex, int pageSize);
        Task<TopicModel> GetById(int id);
        Task<int> Create(TopicModel topicModel);
        Task<int> Update(TopicModel topicModel);
        Task<int> Delete(int id);
    }
}
