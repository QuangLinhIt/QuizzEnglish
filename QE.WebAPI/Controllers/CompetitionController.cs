using Microsoft.AspNetCore.Mvc;
using QE.Business.Logic.Competition;
using QE.Business.Model;
using QE.Core.CustomerError;
using QE.Core.Enum;

namespace QE.WebAPI.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class CompetitionController : ControllerBase
    {
        private readonly ICompetitionBo _competitionBo;
        private readonly ILogger<CompetitionController> _logger;
        public CompetitionController(ICompetitionBo competitionBo,ILogger<CompetitionController> logger)
        {
            _competitionBo = competitionBo;
            _logger = logger;
        }

        [HttpGet("Competition")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var competition = await _competitionBo.GetById(id);
                if (competition != null)
                {
                    return Ok(new DataApiResponse<CompetitionModel> { Data = competition, Success = true, Message = "" });
                }
                return Ok(new DataApiResponse<object> { Success = false, Message = "Data not found" });
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
                throw new CustomeNotFoundException(ex.Message);
            }
        }

        [HttpPost("AddCompetition")]
        public async Task<IActionResult> CreateCompetition(CompetitionModel model)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(model.Player1Id))
                {
                    return Ok(new DataApiResponse<object> { Success = false, Message = "Create competition fail" });
                }
                var competition = await _competitionBo.Create(model);
                if (competition == (int)ResponseEnumType.Fail)
                {
                    return Ok(new DataApiResponse<object> { Success = false, Message = "Create competition fail" });
                }
                return Ok(new DataApiResponse<object> { Success = true, Message = "Create competition success" });
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
                throw new CustomeNotFoundException(ex.Message);
            }
        }

        [HttpPut("UpdateCompetition")]
        public async Task<IActionResult> UpdateCompetition(CompetitionModel model)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(model.Player1Id))
                {
                    return Ok(new DataApiResponse<object> { Success = false, Message = "Update competition fail" });
                }
                var competition = await _competitionBo.Update(model);
                if (competition == (int)ResponseEnumType.Fail)
                {
                    return Ok(new DataApiResponse<object> { Success = false, Message = "Update competition fail" });
                }
                return Ok(new DataApiResponse<object> { Success = true, Message = "Update competition success" });
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
                throw new CustomeNotFoundException(ex.Message);
            }
        }

        [HttpDelete("DeleteCompetition")]
        public async Task<IActionResult> DeleteCompetition(int id)
        {
            try
            {
                var competition = await _competitionBo.Delete(id);
                if (competition == (int)ResponseEnumType.Fail)
                {
                    return Ok(new DataApiResponse<object> { Success = false, Message = "Delete Competition fail" });
                }
                return Ok(new DataApiResponse<object> { Success = true, Message = "Delete competition success" });
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
                throw new CustomeNotFoundException(ex.Message);
            }
        }
    }
}
