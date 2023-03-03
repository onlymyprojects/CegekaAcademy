using Moq;
using PetShelter.BusinessLayer.Validators;
using PetShelter.DataAccessLayer.Models;
using PetShelter.DataAccessLayer.Repository;

namespace PetShelter.BusinessLayer.Tests;

public class AddDonationTests
{
    private readonly DonationService _donationServiceSut;
    private readonly Mock<IDonationRepository> _mockDonationRepository;

    private AddDonationRequest _request;

    public AddDonationTests()
    {
        _mockDonationRepository = new Mock<IDonationRepository>();
        _donationServiceSut = new DonationService(_mockDonationRepository.Object, new AddDonationRequestValidator());
    }

    private void SetupHappyPath()
    {
        _request = new AddDonationRequest
        {
            Amount = 10,
            Person = new Models.Person
            {
                DateOfBirth = DateTime.Now.AddYears(-Constants.PersonConstants.AdultMinAge),
                IdNumber = "1111222233334",
                Name = "TestName"
            }
        };
    }

    [Fact]
    public async Task GivenValidRequest_WhenAddDonation_DonationIsAdded()
    {
        SetupHappyPath();
        await _donationServiceSut.AddDonation(_request);

        _mockDonationRepository.Verify(x => x.Add(It.Is<Donation>(d => d.Amount == _request.Amount)), Times.Once);
    }

    [Theory]
    [InlineData(-5)]
    [InlineData(0)]
    public async Task GivenAmountIsInvalid_WhenAddDonation_ThenThrowsArgumentException_And_DonationIsNotAdded(decimal amount)
    {
        SetupHappyPath();
        _request.Amount = amount;

        await Assert.ThrowsAsync<ArgumentException>(() => _donationServiceSut.AddDonation(_request));

        _mockDonationRepository.Verify(x => x.Add(It.Is<Donation>(d => d.Amount == _request.Amount)), Times.Never);
    }

    [Fact]
    public async Task GivenDonorIsEmpty_WhenAddDonation_ThenThrowsArgumentException_And_DonationIsNotAdded()
    {
        // Arrange
        SetupHappyPath();
        _request.Person = new Models.Person();

        //Act
        await Assert.ThrowsAsync<ArgumentException>(() => _donationServiceSut.AddDonation(_request));

        //Assert
        _mockDonationRepository.Verify(x => x.Add(It.Is<Donation>(d => d.Amount == _request.Amount)), Times.Never);
    }

    [Theory]
    [InlineData("123456789123456")]
    [InlineData("1234")]
    public async Task GivenDonorIdNumberIsInvalid_WhenAddDonation_ThenThrowsArgumentException_And_DonationIsNotAdded(string idNumber)
    {
        // Arrange
        SetupHappyPath();
        _request.Person.IdNumber = idNumber;

        //Act
        await Assert.ThrowsAsync<ArgumentException>(() => _donationServiceSut.AddDonation(_request));

        //Assert
        _mockDonationRepository.Verify(x => x.Add(It.Is<Donation>(d => d.Amount == _request.Amount)), Times.Never);
    }

    [Theory]
    [InlineData("")]
    [InlineData("D")]
    public async Task GivenDonorNameIsInvalid_WhenAddDonation_ThenThrowsArgumentException_And_DonationIsNotAdded(string name)
    {
        // Arrange
        SetupHappyPath();
        _request.Person.Name = name;

        //Act
        await Assert.ThrowsAsync<ArgumentException>(() => _donationServiceSut.AddDonation(_request));

        //Assert
        _mockDonationRepository.Verify(x => x.Add(It.Is<Donation>(d => d.Amount == _request.Amount)), Times.Never);
    }

    [Fact]
    public async Task GivenDonorDateOfBirthNameIsInvalid_WhenAddDonation_ThenThrowsArgumentException_And_DonationIsNotAdded()
    {
        // Arrange
        SetupHappyPath();
        _request.Person.DateOfBirth = DateTime.Parse("2008-06-19");

        //Act
        await Assert.ThrowsAsync<ArgumentException>(() => _donationServiceSut.AddDonation(_request));

        //Assert
        _mockDonationRepository.Verify(x => x.Add(It.Is<Donation>(d => d.Amount == _request.Amount)), Times.Never);
    }

    [Fact]
    public async Task GivenRequestWithMissingData_WhenAddDonation_DonationIsNotAdded()
    {
        _request = new AddDonationRequest();
        await Assert.ThrowsAsync<ArgumentException>(() => _donationServiceSut.AddDonation(_request));

        _mockDonationRepository.Verify(x => x.Add(It.IsAny<Donation>()), Times.Never);
    }
}
