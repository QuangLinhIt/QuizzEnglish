using Microsoft.AspNetCore.Mvc;
using QE.Business.Logic.CompetitionQuizz;
using QE.Business.Model;
using QE.Core.CustomerError;
using QE.Core.Enum;

namespace QE.WebAPI.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class CompetitionQuizzController : ControllerBase
    {
        private readonly ICompetitionQuizzBo _competitionQuizzBo;
        private readonly ILogger<CompetitionQuizzController> _logger;
        public CompetitionQuizzController(ICompetitionQuizzBo competitionQuizzBo, ILogger<CompetitionQuizzController> logger)
        {
            _competitionQuizzBo = competitionQuizzBo;
            _logger = logger;
        }

        [HttpGet("CompetitionQuizzes")]
        public async Task<IActionResult> GetCompetitionQuizzesByCompetitionId(int competitionId)
        {
            try
            {
                var competitionQuizzes = await _competitionQuizzBo.GetByCompetitionId(competitionId);
                if(competitionQuizzes !=null && competitionQuizzes.Any())
                {
                    return Ok(new DataApiResponse<IEnumerable<CompetitionQuizzModel>> { Data = competitionQuizzes, Success = true, Message = "" });
                }
                return Ok(new DataApiResponse<object> { Success = false, Message = "Data not found" });
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
                throw new CustomeNotFoundException(ex.Message);
            }
        }

        [HttpPost("AddCompetitionQuizz")]
        public async Task<IActionResult> CreatCompetitionQuizz(CompetitionQuizzModel model)
        {
            try
            {
                var competitionQuizz = await _competitionQuizzBo.Create(model);
                if (competitionQuizz == (int)ResponseEnumType.Fail)
                {
                    return Ok(new DataApiResponse<object> { Success = false, Message = "Create CompetitionQuizz fail" });
                }
                return Ok(new DataApiResponse<object> { Success = true, Message = " Create CompetitionQuizz success" });
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
                throw new CustomeNotFoundException(ex.Message);
            }
        }
    }
}
