using Microsoft.AspNetCore.Mvc;
using QE.Business.Logic.Vocabulary;
using QE.Business.Model;
using QE.Core.CustomerError;
using QE.Core.Enum;

namespace QE.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VocabularyController : ControllerBase
    {
        private readonly IVocabularyBo _vocabularyBo;
        public readonly ILogger<VocabularyController> _logger;
        public VocabularyController(IVocabularyBo vocabularyBo,ILogger<VocabularyController> logger)
        {
            _vocabularyBo = vocabularyBo;
            _logger = logger;
        }

        [HttpGet("Vocabulary")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var vocabulary = await _vocabularyBo.GetById(id);
                if (vocabulary == null)
                {
                    return Ok(new DataApiResponse<VocabularyModel> { Success = false, Message = "Data not found" });
                }
                return Ok(new DataApiResponse<VocabularyModel> { Data = vocabulary, Success = true ,Message=""});
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
                throw new CustomeNotFoundException(ex.Message);
            }
        }

        [HttpPost("AddVocabulary")]
        public async Task<IActionResult> CreateVocabulary([FromBody]VocabularyModel model)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(model.Name) || string.IsNullOrWhiteSpace(model.Meaning) || string.IsNullOrWhiteSpace(model.Pronoun))
                {
                    return Ok(new DataApiResponse<object> { Success = false, Message = "Create vocabulary fail" });
                }
                var vocabulary = await _vocabularyBo.Create(model);
                if (vocabulary == (int)ResponseEnumType.Fail)
                {
                    return Ok(new DataApiResponse<object> { Success = false, Message = "Create vocabulary fail" });
                }
                return Ok(new DataApiResponse<object> { Success = true, Message = "Create vocabulary success" });
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
                throw new CustomeNotFoundException(ex.Message);
            }
        }

        [HttpPut("UpdateVocabulary")]
        public async Task<IActionResult> UpdateVocabulary(VocabularyModel model)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(model.Name) || string.IsNullOrWhiteSpace(model.Meaning) || string.IsNullOrWhiteSpace(model.Pronoun))
                {
                    return Ok(new DataApiResponse<object> { Success = false, Message = "Create vocabulary fail" });
                }
                var vocabulary = await _vocabularyBo.Update(model);
                if (vocabulary == (int)ResponseEnumType.Fail)
                {
                    return Ok(new DataApiResponse<object> { Success = false, Message = "Update vocabulary fail" });
                }
                return Ok(new DataApiResponse<object> { Success = true, Message = "Update vocabulary success" });
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
                throw new CustomeNotFoundException(ex.Message);
            }
        }

        [HttpDelete("DeleteVocabulary")]
        public async Task<IActionResult> DeleteVocabulary(int id)
        {
            try
            {
                var vocabulary = await _vocabularyBo.Delete(id);
                if (vocabulary == (int)ResponseEnumType.Fail)
                {
                    return Ok(new DataApiResponse<VocabularyModel> { Success = false, Message = "Delete vocabulary fail" });
                }
                return Ok(new DataApiResponse<VocabularyModel> { Success = true, Message = "Delete vocabulary success" });
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
                throw new CustomeNotFoundException(ex.Message);
            }
        }
    }
}
