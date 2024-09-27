using Microsoft.AspNetCore.Mvc;
using QE.Business.Logic.Question;
using QE.Business.Model;
using QE.Core.CustomerError;
using QE.Core.Enum;
using QE.DataAccess.Repository.Detail.Interface;

namespace QE.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionBo _questionBo;
        private readonly ILogger<QuestionController> _logger;
        public QuestionController(IQuestionBo questionBo, ILogger<QuestionController> logger)
        {
            _questionBo = questionBo;
            _logger = logger;
        }

        [HttpPost("AddFillinBlankQuestion")]
        public async Task<IActionResult> CreateFillinBlankQuestion(FillinBlankQuestionModel model)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(model.Title) || string.IsNullOrWhiteSpace(model.Answer))
                {
                    return Ok(new DataApiResponse<object> { Success = false, Message = "Create question fail" });
                }
                var question = await _questionBo.CreateFillinBlankQuestion(model);
                if (question == (int)ResponseEnumType.Fail)
                {
                    return Ok(new DataApiResponse<object> { Success = false, Message = "Create question fail" });
                }
                return Ok(new DataApiResponse<object> { Success = true, Message = "Create question success" });
            }
            catch(Exception ex)
            {
                _logger.LogWarning(ex.Message);
                throw new CustomeNotFoundException(ex.Message);
            }
        }

        [HttpPost("AddMultipleChoiceQuestion")]
        public async Task<IActionResult> CreateMultipleChoiceQuestion(MultipleChoiceQuestionModel model)
        {
            try
            {
                if(string.IsNullOrWhiteSpace(model.Title)|| string.IsNullOrWhiteSpace(model.OptionA)
                    || string.IsNullOrWhiteSpace(model.OptionB) || string.IsNullOrWhiteSpace(model.OptionC)
                    || string.IsNullOrWhiteSpace(model.OptionD))
                {
                    return Ok(new DataApiResponse<object> { Success = false, Message = "Create question fail" });
                }
                var question = await _questionBo.CreateMultipleChoiceQuestion(model);
                if (question == (int)ResponseEnumType.Fail)
                {
                    return Ok(new DataApiResponse<object> { Success = false, Message = "Create question fail" });
                }
                return Ok(new DataApiResponse<object> { Success = true, Message = "Create question success" });
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
                throw new CustomeNotFoundException(ex.Message);
            }
        }

        [HttpPut("UpdateFillinBlankQuestion")]
        public async Task<IActionResult> UpdateFillinBlankQuestion(FillinBlankQuestionModel model)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(model.Title) || string.IsNullOrWhiteSpace(model.Answer))
                {
                    return Ok(new DataApiResponse<object> { Success = false, Message = "Update question fail" });
                }
                var question = await _questionBo.UpdateFillinBlankQuestion(model);
                if (question == (int)ResponseEnumType.Fail)
                {
                    return Ok(new DataApiResponse<object> { Success = false, Message = "Update question fail" });
                }
                return Ok(new DataApiResponse<object> { Success = true, Message = "Update question success" });
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
                throw new CustomeNotFoundException(ex.Message);
            }
        }

        [HttpPut("UpdateMultipleChoiceQuestion")]
        public async Task<IActionResult> UpdateMultipleChoiceQuestion(MultipleChoiceQuestionModel model)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(model.Title) || string.IsNullOrWhiteSpace(model.OptionA)
                    || string.IsNullOrWhiteSpace(model.OptionB) || string.IsNullOrWhiteSpace(model.OptionC)
                    || string.IsNullOrWhiteSpace(model.OptionD))
                {
                    return Ok(new DataApiResponse<object> { Success = false, Message = "Update question fail" });
                }
                var question = await _questionBo.UpdateMultipleChoiceQuestion(model);
                if (question == (int)ResponseEnumType.Fail)
                {
                    return Ok(new DataApiResponse<object> { Success = false, Message = "Update question fail" });
                }
                return Ok(new DataApiResponse<object> { Success = true, Message = "Update question success" });
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
                throw new CustomeNotFoundException(ex.Message);
            }
        }

        [HttpDelete("DeleteQuestion")]
        public async Task<IActionResult> DeleteQuestion(int id)
        {
            try
            {
                var question = await _questionBo.DeleteQuestion(id);
                if (question == (int)ResponseEnumType.Fail)
                {
                    return Ok(new DataApiResponse<object> { Success = false, Message = "Delete question fail" });
                }
                return Ok(new DataApiResponse<object> { Success = true, Message = "Delete question success" });
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
                throw new CustomeNotFoundException(ex.Message);
            }
        }
    }
}
