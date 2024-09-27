using AutoMapper;
using QE.Business.Model;
using QE.Core.Enum;
using QE.DataAccess.Repository.Detail.Interface;

namespace QE.Business.Logic.Vocabulary
{
    public class VocabularyBo : IVocabularyBo
    {
        private readonly IVocabularyTopicUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public VocabularyBo(IVocabularyTopicUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<VocabularyModel> GetById(int id)
        {
            var vocabulary = await _unitOfWork.Vocabulary.GetByIdAsync(id);
            if (vocabulary != null)
            {
                return _mapper.Map<VocabularyModel>(vocabulary);
            }
            return null!;
        }

        public async Task<int> Create(VocabularyModel model)
        {
            //1: start UnitOfWork
            await _unitOfWork.BeginTransactionAsync();
            //2: add vocabulary
            var vocabulary = new QE.Entity.Entity.Vocabulary()
            {
                Name = model.Name,
                Meaning = model.Meaning,
                Pronoun = model.Pronoun,
                Image = model.Image,
                Audio = model.Audio,
            };
            await _unitOfWork.Vocabulary.InsertAsync(vocabulary);
            await _unitOfWork.SaveChangesAsync();
            //3: add VocabularyTopic 
            if(model.VocabularyTopics==null || !model.VocabularyTopics.Any())
            {
                await _unitOfWork.RollbackAsync();
                return (int)ResponseEnumType.Fail;
            }
            foreach(var item in model.VocabularyTopics)
            {
                var vocabularyTopic = new QE.Entity.Entity.VocabularyTopic()
                {
                    TopicId = item.TopicId,
                    VocabularyId = vocabulary.Id,
                };
                await _unitOfWork.VocabularyTopic.InsertAsync(vocabularyTopic);
                await _unitOfWork.SaveChangesAsync();
            }
            //4: close UnitOfWork
            await _unitOfWork.CommitAsync();
            return (int)ResponseEnumType.Sucess;
        }

        public async Task<int> Update(VocabularyModel model)
        {
            //1: open UnitOfWork
            await _unitOfWork.BeginTransactionAsync();
            //2: find vocabulary
            var existingVocabulary = await _unitOfWork.Vocabulary.GetByIdAsync(model.Id);
            if (existingVocabulary == null)
            {
                await _unitOfWork.RollbackAsync();
                return (int)ResponseEnumType.Fail;
            }
            //3: update vocabulary
            var vocabulary = new QE.Entity.Entity.Vocabulary()
            {
                Id = model.Id,
                Name = model.Name,
                Meaning = model.Meaning,
                Pronoun = model.Pronoun,
                Image = model.Image,
                Audio = model.Audio,
            };
            await _unitOfWork.Vocabulary.UpdateAsync(vocabulary);
            await _unitOfWork.SaveChangesAsync();
            //4: delete all old VocabularyTopic relationship
            foreach(var item in existingVocabulary.VocabularyTopics)
            {
                var oldVocabularyTopic = new QE.Entity.Entity.VocabularyTopic()
                {
                    TopicId = item.TopicId,
                    VocabularyId = item.VocabularyId,
                };
                await _unitOfWork.VocabularyTopic.DeleteAsync(oldVocabularyTopic);
                await _unitOfWork.SaveChangesAsync();
            }
            //5: all new list VocabularyTopic relationship
            if(model.VocabularyTopics==null || !model.VocabularyTopics.Any())
            {
                await _unitOfWork.RollbackAsync();
                return (int)ResponseEnumType.Fail;
            }
            foreach(var item in model.VocabularyTopics)
            {
                var newVocabularyTopic = new QE.Entity.Entity.VocabularyTopic()
                {
                    TopicId = item.TopicId,
                    VocabularyId = model.Id,
                };
                await _unitOfWork.VocabularyTopic.InsertAsync(newVocabularyTopic);
                await _unitOfWork.SaveChangesAsync();
            }
            //6: close UnitOfWork
            await _unitOfWork.CommitAsync();
            return (int)ResponseEnumType.Sucess;
        }

        public async Task<int> Delete(int id)
        {
            //1: open UnitOfWork
            await _unitOfWork.BeginTransactionAsync();
            //2: Find Vocabulary
            var vocabulary = await _unitOfWork.Vocabulary.GetByIdAsync(id);
            if (vocabulary == null)
            {
                await _unitOfWork.RollbackAsync();
                return (int)ResponseEnumType.Fail;
            }
            //3: delete all VocabularyTopic relationship
            foreach(var item in vocabulary.VocabularyTopics)
            {
                var vocabularyTopic = new QE.Entity.Entity.VocabularyTopic()
                {
                    TopicId = item.TopicId,
                    VocabularyId = item.VocabularyId,
                };
                await _unitOfWork.VocabularyTopic.DeleteAsync(vocabularyTopic);
                await _unitOfWork.SaveChangesAsync();
            }
            //4: delete vocabulary
            await _unitOfWork.Vocabulary.DeleteAsync(vocabulary);
            await _unitOfWork.SaveChangesAsync();
            //5:close UnitOfWork
            await _unitOfWork.CommitAsync();
            return (int)ResponseEnumType.Sucess;
        }
    }
}
