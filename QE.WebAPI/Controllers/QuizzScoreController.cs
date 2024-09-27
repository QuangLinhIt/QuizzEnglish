using Microsoft.AspNetCore.Mvc;
using QE.Business.Logic.QuizzScore;
using QE.Business.Model;
using QE.Core.CustomerError;
using QE.Core.Enum;

namespace QE.WebAPI.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class QuizzScoreController : ControllerBase
    {
        private readonly IQuizzScoreBo _quizzScoreBo;
        private readonly ILogger<QuizzScoreController> _logger;
        public QuizzScoreController(IQuizzScoreBo quizzScoreBo,ILogger<QuizzScoreController> logger)
        {
            _quizzScoreBo = quizzScoreBo;
            _logger = logger;
        }

        [HttpGet("GetQuizzScore")]
        public async Task<IActionResult> GetByQuizzIdAndUserId(int quizzId,string userId)
        {
            try
            {
                var quizzScores = await _quizzScoreBo.GetQuizzScoreByQuizzId(quizzId, userId);
                if (quizzScores != null && quizzScores.Any())
                {
                    return Ok(new DataApiResponse<IEnumerable<QuizzScoreModel>> { Data = quizzScores, Success = true, Message = "" });
                }
                return Ok(new DataApiResponse<object> { Success = false, Message = "Data not found" });
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
                throw new CustomeNotFoundException(ex.Message);
            }
        }

        [HttpPut("AddQuizzScore")]
        public async Task<IActionResult> CreateQuizzScore(QuizzScoreModel model)
        {
            try
            {
                var quizzScore = await _quizzScoreBo.Create(model);
                if (quizzScore == (int)ResponseEnumType.Fail)
                {
                    return Ok(new DataApiResponse<object> { Success = false, Message = "Create QuizzScore fail" });
                }
                return Ok(new DataApiResponse<object> { Success = true, Message = "Create QuizzScore success" });

            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
                throw new CustomeNotFoundException(ex.Message);
            }
        }
    }
}
