using AutoMapper;
using QE.Business.Model;
using QE.Core.Enum;
using QE.DataAccess.Repository.Detail.Interface;

namespace QE.Business.Logic.Vocabulary
{
    public class VocabularyBo:IVocabularyBo
    {
        private readonly IVocabularyTopicUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public VocabularyBo(IVocabularyTopicUnitOfWork unitOfWork,IMapper mapper)
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
        public async Task<int> Create(VocabularyModel vocabularyModel)
        {
            await _unitOfWork.BeginTransactionAsync();
            //1: Add Vocabulary
            if (vocabularyModel.VocabularyTopics == null || !vocabularyModel.VocabularyTopics.Any())
            {
                await _unitOfWork.RollbackAsync();
                return (int)ResponseEnumType.Fail;
            }
            var vocabularyEntity = new QE.Entity.Entity.Vocabulary()
            {
                Name = vocabularyModel.Name,
                Meaning = vocabularyModel.Meaning,
                Pronoun = vocabularyModel.Pronoun,
                Image = vocabularyModel.Image,
                Audio = vocabularyModel.Audio,
            };
            var vocabularyResult=await _unitOfWork.Vocabulary.InsertAsync(vocabularyEntity);
            await _unitOfWork.SaveChangesAsync();
            //2: Add VocabularyTopic
            foreach(var item in vocabularyModel.VocabularyTopics)
            {
                var vocabularyTopicEntity = new QE.Entity.Entity.VocabularyTopic()
                {
                    TopicId = item.TopicId,
                    VocabularyId = item.VocabularyId,
                };
                await _unitOfWork.VocabularyTopic.InsertAsync(vocabularyTopicEntity);
                await _unitOfWork.SaveChangesAsync();
            }
            await _unitOfWork.CommitAsync();
            return (int)ResponseEnumType.Sucess;
        }

        public async Task<int> Update(VocabularyModel vocabularyModel)
        {
          
            await _unitOfWork.BeginTransactionAsync();
            //1: update Vocabulary
            if(vocabularyModel.VocabularyTopics==null || !vocabularyModel.VocabularyTopics.Any())
            {
                await _unitOfWork.RollbackAsync();
                return (int)ResponseEnumType.Fail;
            }
            var existingVocabulary = await _unitOfWork.Vocabulary.GetByIdAsync(vocabularyModel.Id);
            if (existingVocabulary != null)
            {
                var vocabularyEntity = new QE.Entity.Entity.Vocabulary()
                {
                    Id = vocabularyModel.Id,
                    Name = vocabularyModel.Name,
                    Meaning = vocabularyModel.Meaning,
                    Pronoun = vocabularyModel.Pronoun,
                    Image = vocabularyModel.Image,
                    Audio = vocabularyModel.Audio,
                };
                await _unitOfWork.Vocabulary.UpdateAsync(vocabularyEntity);
                await _unitOfWork.SaveChangesAsync();
                //2: update VocabularyTopic
                //2.1: delete all old VocabularyTopic relationship
                if (existingVocabulary.VocabularyTopics != null && existingVocabulary.VocabularyTopics.Any())
                {
                    foreach(var item in existingVocabulary.VocabularyTopics)
                    {
                        var existingVocabularyTopicEntity = new QE.Entity.Entity.VocabularyTopic()
                        {
                            TopicId = item.TopicId,
                            VocabularyId = item.VocabularyId,
                        };
                        await _unitOfWork.VocabularyTopic.DeleteAsync(existingVocabularyTopicEntity);
                        await _unitOfWork.SaveChangesAsync();
                    }
                }
                //2.3: Add new VocabularyTopic list
                if(vocabularyModel.VocabularyTopics!=null && vocabularyModel.VocabularyTopics.Any())
                {
                    foreach(var item in vocabularyModel.VocabularyTopics)
                    {
                        var vocabularyTopicEntity=new QE.Entity.Entity.VocabularyTopic()
                        {
                            TopicId = item.TopicId,
                            VocabularyId = item.VocabularyId,
                        };
                        await _unitOfWork.VocabularyTopic.InsertAsync(vocabularyTopicEntity);
                        await _unitOfWork.SaveChangesAsync();
                    }
                }
                else
                {
                    await _unitOfWork.RollbackAsync();
                    return (int)ResponseEnumType.Fail;
                }
            }
            await _unitOfWork.CommitAsync();
            return (int)ResponseEnumType.Sucess;
        }

        public async Task<int> Delete(int id)
        {
           
            await _unitOfWork.BeginTransactionAsync();
            //find vocabulary
            var existingVocabulary = await _unitOfWork.Vocabulary.GetByIdAsync(id);
            if (existingVocabulary != null)
            {
                //Delete all VocabularyTopic relationship
                if(existingVocabulary.VocabularyTopics!=null && existingVocabulary.VocabularyTopics.Any())
                {
                    foreach(var item in existingVocabulary.VocabularyTopics)
                    {
                        var vocabularyTopicEntity = new QE.Entity.Entity.VocabularyTopic()
                        {
                            TopicId = item.TopicId,
                            VocabularyId = item.VocabularyId,
                        };
                        await _unitOfWork.VocabularyTopic.DeleteAsync(vocabularyTopicEntity);
                        await _unitOfWork.SaveChangesAsync();
                    }
                }
                await _unitOfWork.Vocabulary.DeleteAsync(existingVocabulary);
                await _unitOfWork.SaveChangesAsync();
            }
            await _unitOfWork.CommitAsync();
            return (int)ResponseEnumType.Sucess;
        }
    }
}
