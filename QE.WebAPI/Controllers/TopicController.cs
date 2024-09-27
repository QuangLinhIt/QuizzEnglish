using Microsoft.AspNetCore.Mvc;
using QE.Business.Logic.Topic;
using QE.Business.Model;
using QE.Core.CustomerError;
using QE.Core.Enum;

namespace QE.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopicController : ControllerBase
    {
        private readonly ITopicBo _topicBo;
        private readonly ILogger<TopicController> _logger;
        public TopicController(ITopicBo topicBo, ILogger<TopicController> logger)
        {
            _topicBo = topicBo;
            _logger = logger;
        }

        [HttpGet("Topics")]
        public async Task<IActionResult> GetAll(int pageIndex, int pageSize)
        {
            try
            {
                var topics = await _topicBo.GetAll(pageIndex, pageSize);
                if (topics != null && topics.Any())
                {
                    return Ok(new DataApiResponse<IEnumerable<TopicModel>> { Data = topics, Success = true, Message = "" });
                }
                return Ok(new DataApiResponse<IEnumerable<TopicModel>> { Success = false, Message = "Data not found" });
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
                throw new CustomeNotFoundException(ex.Message);
            }
        }

        [HttpGet("Topic")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var topic = await _topicBo.GetById(id);
                if (topic == null)
                {
                    return Ok(new DataApiResponse<TopicModel> { Success = false, Message = "Data not found" });
                }
                return Ok(new DataApiResponse<TopicModel> { Data = topic, Success = true, Message = "" });
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
                throw new CustomeNotFoundException(ex.Message);
            }
        }

        [HttpPost("AddTopic")]
        public async Task<IActionResult> CreateTopic([FromBody] TopicModel model)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(model.Name))
                {
                    return Ok(new DataApiResponse<object> { Success = false, Message = "Create topic fail" });
                }
                var topic = await _topicBo.Create(model);
                if (topic == (int)ResponseEnumType.Fail)
                {
                    return Ok(new DataApiResponse<object> { Success = false, Message = "Create topic fail" });
                }
                return Ok(new DataApiResponse<object> { Success = true, Message = "Create topic success" });
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
                throw new CustomeNotFoundException(ex.Message);
            }
        }

        [HttpPut("UpdateTopic")]
        public async Task<IActionResult> UpdateTopic([FromBody] TopicModel model)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(model.Name))
                {
                    return Ok(new DataApiResponse<object> { Success = false, Message = "Update topic fail" });
                }
                var topic = await _topicBo.Update(model);
                if (topic == (int)ResponseEnumType.Fail)
                {
                    return Ok(new DataApiResponse<object> { Success = false, Message = "Update topic fail" });
                }
                return Ok(new DataApiResponse<object> { Success = true, Message = "Update topic success" });
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
                throw new CustomeNotFoundException(ex.Message);
            }
        }

        [HttpDelete("DeleteTopic")]
        public async Task<IActionResult> DeleteTopic(int id)
        {
            try
            {
                var topic = await _topicBo.Delete(id);
                if (topic == (int)ResponseEnumType.Fail)
                {
                    return Ok(new DataApiResponse<object> { Success = false, Message = "Delete topic fail" });
                }
                return Ok(new DataApiResponse<object> { Success = true, Message = "Delete topic success" });
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
                throw new CustomeNotFoundException(ex.Message);
            }
        }
    }
}
