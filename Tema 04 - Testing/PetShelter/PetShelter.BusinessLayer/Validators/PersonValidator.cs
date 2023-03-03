using FluentValidation;
using PetShelter.BusinessLayer.Constants;
using PetShelter.BusinessLayer.Models;

namespace PetShelter.BusinessLayer.Validators;

public class PersonValidator: AbstractValidator<Person>
{
    public PersonValidator()
    {
        RuleFor(x => x.IdNumber).Length(PersonConstants.IdNumberLength);
        RuleFor(x => x.Name).NotEmpty().MinimumLength(PersonConstants.NameMinLength);
        RuleFor(x => x.DateOfBirth)
            .Must(x => x <= DateTime.Now.AddYears(-PersonConstants.AdultMinAge))
            .When(x => x.DateOfBirth != null);
    }
}