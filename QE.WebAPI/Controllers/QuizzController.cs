using Microsoft.AspNetCore.Mvc;
using QE.Business.Logic.Quizz;
using QE.Business.Model;
using QE.Core.CustomerError;
using QE.Core.Enum;

namespace QE.WebAPI.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class QuizzController : ControllerBase
    {
        private readonly IQuizzBo _quizzBo;
        private readonly ILogger<QuizzController> _logger;
        public QuizzController(IQuizzBo quizzBo, ILogger<QuizzController> logger)
        {
            _quizzBo = quizzBo;
            _logger = logger;
        }

        [HttpGet("Quizzes")]
        public async Task<IActionResult> GetAll(int pageIndex, int pageSize)
        {
            try
            {
                var quizzes = await _quizzBo.GetAll(pageIndex, pageSize);
                if (quizzes != null && quizzes.Any())
                {
                    return Ok(new DataApiResponse<IEnumerable<QuizzModel>> { Data = quizzes, Success = true, Message = "" });
                }
                return Ok(new DataApiResponse<object> { Success = false, Message = "Data not found" });
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
                throw new CustomeNotFoundException(ex.Message);
            }
        }

        [HttpGet("Quizz")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var quizz = await _quizzBo.GetById(id);
                if (quizz != null)
                {
                    return Ok(new DataApiResponse<object> { Data = quizz, Success = true, Message = "" });
                }
                return Ok(new DataApiResponse<object> { Success = false, Message = "Data not found" });
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
                throw new CustomeNotFoundException(ex.Message);
            }
        }

        [HttpPost("AddQuizz")]
        public async Task<IActionResult> CreateQuizz(QuizzModel model)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(model.Name) || string.IsNullOrWhiteSpace(model.CreatorId))
                {
                    return Ok(new DataApiResponse<object> { Success = false, Message = "Create quizz fail" });
                }
                var quizz = await _quizzBo.Create(model);
                if (quizz == (int)ResponseEnumType.Fail)
                {
                    return Ok(new DataApiResponse<object> { Success = false, Message = "Create quizz fail" });
                }
                return Ok(new DataApiResponse<object> { Success = true, Message = "Create quizz success" });
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
                throw new CustomeNotFoundException(ex.Message);
            }
        }

        [HttpPut("UpdateQuizz")]
        public async Task<IActionResult> UpdateQuizz(QuizzModel model)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(model.Name) || string.IsNullOrWhiteSpace(model.CreatorId))
                {
                    return Ok(new DataApiResponse<object> { Success = false, Message = "Update quizz fail" });
                }
                var quizz = await _quizzBo.Update(model);
                if (quizz == (int)ResponseEnumType.Fail)
                {
                    return Ok(new DataApiResponse<object> { Success = false, Message = "Update quizz fail" });
                }
                return Ok(new DataApiResponse<object> { Success = true, Message = "Update quizz success" });
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
                throw new CustomeNotFoundException(ex.Message);
            }
        }

        [HttpDelete("DeleteQuizz")]
        public async Task<IActionResult> DeleteQuizz(int id)
        {
            try
            {
                var quizz = await _quizzBo.Delete(id);
                if (quizz == (int)ResponseEnumType.Fail)
                {
                    return Ok(new DataApiResponse<object> { Success = false, Message = "Delete quizz fail" });
                }
                return Ok(new DataApiResponse<object> { Success = true, Message = "Delete quizz success" });
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
                throw new CustomeNotFoundException(ex.Message);
            }
        }
    }
}
