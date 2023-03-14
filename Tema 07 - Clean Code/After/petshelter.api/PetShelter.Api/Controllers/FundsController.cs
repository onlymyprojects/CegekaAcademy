using Microsoft.AspNetCore.Mvc;
using PetShelter.Api.Resources;
using PetShelter.Api.Resources.Extensions;
using PetShelter.Domain.Services;
using System.Collections.Immutable;

namespace PetShelter.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FundsController : ControllerBase
    {
        private readonly IFundService _fundService;

        public FundsController(IFundService fundService)
        {
            _fundService = fundService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IReadOnlyList<IdentifiableFund>>> GetFunds()
        {
            var data = await _fundService.GetAllFunds();
            return Ok(data.Select(p => p.AsResource()).ToImmutableArray());
        }

        [HttpGet("Donations")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IReadOnlyList<IdentifiableDonation>>> GetDonations()
        {
            var data = await _fundService.GetAllDonations();
            return Ok(data.Select(p => p.AsResource()).ToImmutableArray());
        }

        [HttpOptions]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Options()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE, OPTIONS");
            return Ok();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateFund([FromBody] CreatedFund fund)
        {
            await _fundService.CreateFundAsync(fund.Owner.AsDomainModel(), fund.AsDomainModel());
            return Ok();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IdentifiableFund>> Get(int id)
        {
            var fund = await _fundService.GetFund(id);
            if (fund is null)
            {
                return NotFound();
            }

            return Ok(fund.AsResource());
        }

        [HttpDelete("{id}/Delete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> DeleteFund(int id)
        {
            await _fundService.DeleteFundAsync(id);
            return NoContent();
        }

        [HttpPost("{id}/Donate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> AddDonation(int id, [FromBody] CreatedDonation donation)
        {
            await _fundService.AddDonationAsync(id, donation.AsDomainModel());
            return Ok();
        }
    }
}
