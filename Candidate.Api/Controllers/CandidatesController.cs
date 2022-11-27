using System.Collections.Generic;
using System.Threading.Tasks;
using Candidate.Api.Controllers.Base;
using Candidate.Application.Services.Candidate;
using Candidate.Common.Core;
using Candidate.Common.DTO.Candidate;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Candidate.Api.Controllers
{
    /// <summary>
    /// Applications Controller
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v1/[controller]")]
    [Produces("application/json")]
    public class CandidatesController : BaseController
    {
        private readonly ICandidateService _service;
        /// <summary>
        /// Constructor
        /// </summary>
        public CandidatesController(ICandidateService service)
        {
            _service = service;
        }

        /// <summary>
        /// Get Single Candidates
        /// </summary>
        /// <returns></returns>
        [HttpGet("{email}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ActionResult<List<CandidateDto>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> CandidateAsync(string email)
        {
            var result = await _service.GetAsync(email);
            return Ok(result);
        }

        /// <summary>
        /// Add Candidate
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ActionResult<string>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> PostAsync(AddCandidateDto model)
        {
            var result = await _service.AddAsync(model);
            return Ok(result);
        }

        /// <summary>
        /// Update Candidate
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ActionResult<string>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> PutAsync(UpdateCandidateDto model)
        {
            var result = await _service.UpdateAsync(model);
            return Ok(result);
        }

        /// <summary>
        /// Delete Candidate
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{email}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ActionResult<string>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> DeleteAsync(string email)
        {
            var result = await _service.DeleteAsync(email);
            return Ok(result);
        }
    }
}
