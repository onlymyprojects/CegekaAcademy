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
            var data = await this._fundService.GetAllFunds();
            return this.Ok(data.Select(p => p.AsResource()).ToImmutableArray());
        }

        [HttpOptions]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Options()
        {
            this.Response.Headers.Add("Allow", "GET, POST, PUT, DELETE, OPTIONS");
            return this.Ok();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateFund([FromBody] CreatedFund fund)
        {
            var id = await _fundService.CreateFundAsync(fund.Owner.AsDomainModel(), fund.AsDomainModel());
            return CreatedAtAction(nameof(CreateFund), id);
        }

        //[HttpPost]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //[ProducesResponseType(StatusCodes.Status201Created)]
        //public async Task<IActionResult> DonateFund([FromBody] CreatedDonation donation)
        //{
        //    var id = await _fundService.DonateAsync(donation.Donor.AsDomainModel(), donation.AsDomainModel());
        //    return CreatedAtAction(nameof(DonateFund), id);
        //}

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IdentifiableFund>> Get(int id)
        {
            var fund = await this._fundService.GetFund(id);
            if (fund is null)
            {
                return this.NotFound();
            }

            return this.Ok(fund.AsResource());
        }

        [HttpDelete("{id}/delete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> DeleteFund(int id)
        {
            await _fundService.DeleteFundAsync(id);
            return NoContent();
        }
    }
}


// TO DO
// donate to fund
// list of donors ???
// status update
// error 500