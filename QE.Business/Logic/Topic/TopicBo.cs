using AutoMapper;
using QE.Business.Model;
using QE.Core.Enum;
using QE.DataAccess.Context;
using QE.DataAccess.Repository.Detail.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace QE.Business.Logic.Topic
{
    public class TopicBo:ITopicBo
    {
        private readonly IVocabularyTopicUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public TopicBo(IVocabularyTopicUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

       

        public async Task<IEnumerable<TopicModel>> GetAll(int pageIndex, int pageSize)
        {
            var topics = await _unitOfWork.Topic.GetAsync(pageIndex, pageSize);
            if(topics!=null && topics.Any())
            {
                return _mapper.Map<IEnumerable<TopicModel>>(topics);
            }
            return null!;
        }

        public async Task<TopicModel> GetById(int id)
        {
            var topic = await _unitOfWork.Topic.GetByIdAsync(id);
            if (topic != null)
            {
                return _mapper.Map<TopicModel>(topic);
            }
            return null!;
        }

        public async Task<int> Create(TopicModel model)
        {
            var topic = new QE.Entity.Entity.Topic()
            {
                Name = model.Name,
            };
            await _unitOfWork.Topic.InsertAsync(topic);
            return (int)ResponseEnumType.Sucess;
        }

        public async Task<int> Update(TopicModel model)
        {
            //1:open UnitOfWork
            await _unitOfWork.BeginTransactionAsync();
            //2: find Topic
            var existingTopic = await _unitOfWork.Topic.GetByIdAsync(model.Id);
            if (existingTopic == null)
            {
                await _unitOfWork.RollbackAsync();
                return (int)ResponseEnumType.Fail;
            }
            //3: update topic
            var topic = new QE.Entity.Entity.Topic()
            {
                Id = model.Id,
                Name = model.Name,
            };
            await _unitOfWork.Topic.UpdateAsync(topic);
            await _unitOfWork.SaveChangesAsync();
            //4: delete all old VocabularyTopic relationship
            if(existingTopic.VocabularyTopics!=null && existingTopic.VocabularyTopics.Any())
            {
                foreach(var item in existingTopic.VocabularyTopics)
                {
                    var oldVocabularyTopic = new QE.Entity.Entity.VocabularyTopic()
                    {
                        TopicId = item.TopicId,
                        VocabularyId = item.VocabularyId,
                    };
                    await _unitOfWork.VocabularyTopic.DeleteAsync(oldVocabularyTopic);
                    await _unitOfWork.SaveChangesAsync();
                }
            }
            //5: add new list VocabularyTopic
            if(model.VocabularyTopics!=null && model.VocabularyTopics.Any())
            {
                foreach (var item in model.VocabularyTopics)
                {
                    var newVocabularyTopic = new QE.Entity.Entity.VocabularyTopic()
                    {
                        TopicId = model.Id,
                        VocabularyId = item.VocabularyId,
                    };
                    await _unitOfWork.VocabularyTopic.InsertAsync(newVocabularyTopic);
                    await _unitOfWork.SaveChangesAsync();
                }
            }
            //6: delete vocabulary not relationship
            await _unitOfWork.Vocabulary.DeleteVocabulariesNotRelationship();
            await _unitOfWork.SaveChangesAsync();
            //7: close UnitOfWork
            await _unitOfWork.CommitAsync();
            return (int)ResponseEnumType.Sucess;
        }

        public async Task<int> Delete(int id)
        {
            //1:open Vocabulary
            await _unitOfWork.BeginTransactionAsync();
            //2: find topic
            var existingTopic = await _unitOfWork.Topic.GetByIdAsync(id);
            if (existingTopic == null)
            {
                await _unitOfWork.RollbackAsync();
                return (int)ResponseEnumType.Fail;
            }
            //3: delete all VocabularyTopic relationship
            if (existingTopic.VocabularyTopics != null && existingTopic.VocabularyTopics.Any())
            {
                foreach (var item in existingTopic.VocabularyTopics)
                {
                    var oldVocabularyTopic = new QE.Entity.Entity.VocabularyTopic()
                    {
                        TopicId = item.TopicId,
                        VocabularyId = item.VocabularyId,
                    };
                    await _unitOfWork.VocabularyTopic.DeleteAsync(oldVocabularyTopic);
                    await _unitOfWork.SaveChangesAsync();
                }
            }
            //4: delete vocabulary not relationship
            await _unitOfWork.Vocabulary.DeleteVocabulariesNotRelationship();
            await _unitOfWork.SaveChangesAsync();
            //5: delete topic
            await _unitOfWork.Topic.DeleteAsync(existingTopic);
            await _unitOfWork.SaveChangesAsync();
            //6: close UnitOfWork
            await _unitOfWork.CommitAsync();
            return (int)ResponseEnumType.Sucess;
        }          
    }
}
