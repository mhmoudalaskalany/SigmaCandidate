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
        /// Get All Candidates
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK , Type = typeof(ActionResult<List<CandidateDto>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> CandidatesAsync()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        /// <summary>
        /// Add Candidate
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ActionResult<List<CandidateDto>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> PostAsync(AddCandidateDto model)
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }
    }
}
