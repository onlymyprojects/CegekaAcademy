using FluentValidation;
using PetShelter.BusinessLayer.Tests;
namespace PetShelter.BusinessLayer.Validators;

public class AddDonationRequestValidator: AbstractValidator<AddDonationRequest>
{
	public AddDonationRequestValidator()
	{
        RuleFor(x => x.Amount).NotEmpty().GreaterThan(0);
        RuleFor(x => x.Person).NotEmpty()
            .SetValidator(new PersonValidator());
    }
}
