using FluentAssertions;
using FluentValidation;
using Moq;
using PetShelter.BusinessLayer.Constants;
using PetShelter.BusinessLayer.ExternalServices;
using PetShelter.BusinessLayer.Models;
using PetShelter.BusinessLayer.Validators;
using PetShelter.DataAccessLayer.Models;
using PetShelter.DataAccessLayer.Repository;

namespace PetShelter.BusinessLayer.Tests
{
    public class RescuePetTests
    {
        private readonly IPersonService _personService;
        private readonly PetService _petServiceSut;

        private readonly Mock<IPersonRepository> _mockPersonRepository;
        private readonly Mock<IPetRepository> _mockPetRepository;
        private readonly Mock<IIdNumberValidator> _mockIdNumberValidator;

        private RescuePetRequest _request;

        public RescuePetTests()
        {
            _mockPersonRepository= new Mock<IPersonRepository>();
            _mockPetRepository= new Mock<IPetRepository>(); 
            _mockIdNumberValidator = new Mock<IIdNumberValidator>();

            _personService = new PersonService(_mockPersonRepository.Object, _mockIdNumberValidator.Object, new PersonValidator());
            _petServiceSut = new PetService(_personService, _mockPetRepository.Object, new RescuePetRequestValidator(), new AdoptPetRequestValidator());
        }

        private void SetupHappyPath()
        {
            _mockIdNumberValidator.Setup(x => x.Validate(It.IsAny<string>())).ReturnsAsync(true);

            _request = new RescuePetRequest
            {
                PetName = "Max",
                Type = Constants.PetType.Dog,
                Description = "Nice dog",
                IsHealthy = true,
                ImageUrl = "test",
                WeightInKg = 10,
                Person = new Models.Person
                {
                    DateOfBirth = DateTime.Now.AddYears(-Constants.PersonConstants.AdultMinAge),
                    IdNumber = "1111222233334",
                    Name = "TestName"
                }
            };
        }

        [Fact]
        public async Task GivenValidData_WhenRescuePet_PetIsAdded()
        {
            //Arrange
            SetupHappyPath();

            //Act
            await _petServiceSut.RescuePet(_request);

            //Assert
            _mockPetRepository.Verify(x => x.Add(It.Is<Pet>(p => p.Name == _request.PetName)), Times.Once);
        }

        [Theory]
        [InlineData(200)]
        [InlineData(-5)]
        [InlineData(0)]
        public async Task GivenWeightIsInvalid_WhenRescuePet_ThenThrowsArgumentException_And_PetIsNotAdded(decimal weight)
        {
            // Arrange
            SetupHappyPath();
            _request.WeightInKg = weight;

            //Act
            await Assert.ThrowsAsync<ArgumentException>(() => _petServiceSut.RescuePet(_request));

            //Assert
            _mockPetRepository.Verify(x => x.Add(It.Is<Pet>(p => p.Name == _request.PetName)), Times.Never);
        }

        [Fact]
        public async Task GivenPetNameIsInvalid_WhenRescuePet_ThenThrowsArgumentException_And_PetIsNotAdded()
        {
            // Arrange
            SetupHappyPath();
            _request.PetName = string.Empty;

            //Act
            await Assert.ThrowsAsync<ArgumentException>(() => _petServiceSut.RescuePet(_request));

            //Assert
            _mockPetRepository.Verify(x => x.Add(It.Is<Pet>(p => p.Name == _request.PetName)), Times.Never);
        }

        [Theory]
        [InlineData((PetType)(-1))]
        [InlineData((PetType)0)]
        [InlineData((PetType)100)]
        public async Task GivenTypeIsInvalid_WhenRescuePet_ThenThrowsArgumentException_And_PetIsNotAdded(PetType type)
        {
            // Arrange
            SetupHappyPath();
            _request.Type = type;

            //Act
            await Assert.ThrowsAsync<ArgumentException>(() => _petServiceSut.RescuePet(_request));

            //Assert
            _mockPetRepository.Verify(x => x.Add(It.Is<Pet>(p => p.Name == _request.PetName)), Times.Never);
        }

        [Fact]
        public async Task GivenPersonIsEmpty_WhenRescuePet_ThenThrowsArgumentException_And_PetIsNotAdded()
        {
            // Arrange
            SetupHappyPath();
            _request.Person = new Models.Person();

            //Act
            await Assert.ThrowsAsync<ArgumentException>(() => _petServiceSut.RescuePet(_request));

            //Assert
            _mockPetRepository.Verify(x => x.Add(It.Is<Pet>(p => p.Name == _request.PetName)), Times.Never);
        }

        [Theory]
        [InlineData("123456789123456")]
        [InlineData("1234")]
        public async Task GivenPersonIdNumberIsInvalid_WhenRescuePet_ThenThrowsArgumentException_And_PetIsNotAdded(string idNumber)
        {
            // Arrange
            SetupHappyPath();
            _request.Person.IdNumber = idNumber;

            //Act
            await Assert.ThrowsAsync<ArgumentException>(() => _petServiceSut.RescuePet(_request));

            //Assert
            _mockPetRepository.Verify(x => x.Add(It.Is<Pet>(p => p.Name == _request.PetName)), Times.Never);
        }

        [Theory]
        [InlineData("")]
        [InlineData("D")]
        public async Task GivenPersonNameIsInvalid_WhenRescuePet_ThenThrowsArgumentException_And_PetIsNotAdded(string name)
        {
            // Arrange
            SetupHappyPath();
            _request.Person.Name = name;

            //Act
            await Assert.ThrowsAsync<ArgumentException>(() => _petServiceSut.RescuePet(_request));

            //Assert
            _mockPetRepository.Verify(x => x.Add(It.Is<Pet>(p => p.Name == _request.PetName)), Times.Never);
        }

        [Fact]
        public async Task GivenIdNumberIsInvalid_WhenRescuePet_ThenThowsArgumentException()
        {
            //Arrange
            SetupHappyPath();

            _mockIdNumberValidator.Setup(x => x.Validate(It.IsAny<string>())).ReturnsAsync(false);

            //Act
            var exception = await Assert.ThrowsAsync<ArgumentException>(() => _petServiceSut.RescuePet(_request));

            exception.Message.Should().Be("CNP format is invalid");

            //Assert
            _mockPetRepository.Verify(x => x.Add(It.Is<Pet>(p => p.Name == _request.PetName)), Times.Never);
        }
    }
}