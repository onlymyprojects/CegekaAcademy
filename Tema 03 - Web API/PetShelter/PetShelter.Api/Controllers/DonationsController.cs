using Microsoft.AspNetCore.Mvc;
using PetShelter.Api.Resources;
using PetShelter.Api.Resources.Extensions;
using PetShelter.Domain.Services;
using System.Collections.Immutable;

namespace PetShelter.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DonationsController : ControllerBase
    {
        private readonly IDonationService _donationService;

        public DonationsController(IDonationService donationService)
        {
            _donationService = donationService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IReadOnlyList<IdentifiableDonation>>> GetDonations()
        {
            var data = await this._donationService.GetAllDonations();
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
        public async Task<IActionResult> DonateFund([FromBody] CreatedDonation donation)
        {
            var id = await _donationService.DonateAsync(donation.Donor.AsDomainModel(), donation.AsDomainModel());
            return CreatedAtAction(nameof(DonateFund), id);
        }
    }
}
