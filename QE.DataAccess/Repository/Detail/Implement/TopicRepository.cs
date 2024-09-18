using Microsoft.EntityFrameworkCore;
using QE.DataAccess.Context;
using QE.DataAccess.Repository.Common.Implement;
using QE.DataAccess.Repository.Detail.Interface;
using QE.Entity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QE.DataAccess.Repository.Detail.Implement
{
    public class TopicRepository : Repository<Topic>, ITopicRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public TopicRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public override async Task<Topic?> GetByIdAsync(int id)
        {
            return await _applicationDbContext.Topics
                .Include(x => x.VocabularyTopics)
                .ThenInclude(x => x.Vocabulary)
                .FirstOrDefaultAsync(x => x.Id == id);
        }


    }
}
