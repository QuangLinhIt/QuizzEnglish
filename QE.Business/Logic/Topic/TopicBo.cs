﻿using AutoMapper;
using QE.Business.Model;
using QE.Core.Enum;
using QE.DataAccess.Context;
using QE.DataAccess.Repository.Detail.Implement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QE.Business.Logic.Topic
{
    public class TopicBo:ITopicBo
    {
        private VocabularyTopicUnitOfWork _unitOfWork;
        private IMapper _mapper;
        public TopicBo(VocabularyTopicUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TopicModel>> GetAll(int pageIndex,int pageSize)
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
            if(topic!=null)
            {
                return _mapper.Map<TopicModel>(topic);
            }
            return null!;
        }

        public async Task<int> Create(TopicModel topicModel)
        {
            // Chỉ tạo mới Topic
            if (topicModel != null)
            {
                var topicEntity = new QE.Entity.Entity.Topic()
                {
                    Id = topicModel.Id,
                    Name = topicModel.Name,
                };
                await _unitOfWork.Topic.InsertAsync(topicEntity);
                return (int)ResponseEnumType.Sucess;
            }
            return (int)ResponseEnumType.Fail;
        }

        public async Task<int> Update(TopicModel topicModel)
        {
            if(topicModel != null)
            {
                await _unitOfWork.BeginTransactionAsync();
                //Find Topic
                var existingTopic = await _unitOfWork.Topic.GetByIdAsync(topicModel.Id);
                if (existingTopic != null)
                {
                    //step 1: update topic
                    var topicEntity = new QE.Entity.Entity.Topic()
                    {
                        Id = topicModel.Id,
                        Name = topicModel.Name,
                    };
                    await _unitOfWork.Topic.UpdateAsync(topicEntity);
                    await _unitOfWork.SaveChangesAsync();

                    //step 2: update Vocabulary
                    //step 2.1: delete all VocabularyTopic relationship
                    if (existingTopic.VocabularyTopics!=null && existingTopic.VocabularyTopics.Any())
                    {
                        foreach(var item in existingTopic.VocabularyTopics)
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
                    //step 2.2: add list VocabularyTopic
                    if(topicModel.VocabularyTopics!=null && topicModel.VocabularyTopics.Any())
                    {
                        foreach(var item in topicModel.VocabularyTopics)
                        {
                            var vocabularyTopicEntity = new QE.Entity.Entity.VocabularyTopic()
                            {
                                TopicId = item.TopicId,
                                VocabularyId = item.VocabularyId,
                            };
                            await _unitOfWork.VocabularyTopic.InsertAsync(vocabularyTopicEntity);
                            await _unitOfWork.SaveChangesAsync();
                        }
                    }
                    //step 3: delete all Vocabulary not relationship
                    await _unitOfWork.Vocabulary.DeleteVocabulariesNotRelationship();
                    await _unitOfWork.SaveChangesAsync();
                }
               
                await _unitOfWork.CommitAsync();
                return (int)ResponseEnumType.Sucess;
            }
            return (int)ResponseEnumType.Fail;
        }

        public async Task<int> Delete(TopicModel topicModel)
        {
            if (topicModel != null)
            {
                await _unitOfWork.BeginTransactionAsync();
                //Find Topic
                var existingTopic = await _unitOfWork.Topic.GetByIdAsync(topicModel.Id);
                if (existingTopic != null)
                {
                    //step 1: Delete VocabularyTopic
                    if(topicModel.VocabularyTopics!=null && topicModel.VocabularyTopics.Any())
                    {
                        foreach(var item in topicModel.VocabularyTopics)
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
                    //step 2: delete Topic
                    await _unitOfWork.Topic.DeleteAsync(existingTopic);
                    await _unitOfWork.SaveChangesAsync();
                    //step 3: delete all Vocabulary not relationship
                    await _unitOfWork.Vocabulary.DeleteVocabulariesNotRelationship();
                    await _unitOfWork.SaveChangesAsync();
                }
                await _unitOfWork.CommitAsync();
                return (int)ResponseEnumType.Sucess;
            }
            return (int)ResponseEnumType.Fail;
        }
    }
}
