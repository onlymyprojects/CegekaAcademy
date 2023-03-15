namespace BuilderPattern.Tests
{
    public class PetCreationTests
    {
        [Fact]
        public void CreateCat_ReturnsCatWithExpectedProperties()
        {
            var catBuilder = new CatPetBuilder();
            var petCreation = new PetCreation(catBuilder);
            var cat = petCreation.CreatePet();

            Assert.Equal("Cute cat", cat.Name);
            Assert.Equal(PetType.Cat, cat.Type);
            Assert.Equal(new DateTime(2026, 06, 26), cat.BirthDate);
            Assert.Equal("Very beautiful", cat.Description);
            Assert.Equal("super url", cat.ImageUrl);
            Assert.True(cat.IsHealthy);
            Assert.Equal(3, cat.WeightInKg);
            Assert.Equal("1234567890123", cat.Rescuer.IdNumber);
            Assert.Equal("Ion", cat.Rescuer.Name);
            Assert.Equal(new DateTime(2025, 05, 25), cat.Rescuer.DateOfBirth);
        }

        [Fact]
        public void CreateDog_ReturnsDogWithExpectedProperties()
        {
            var dogBuilder = new DogPetBuilder();
            var petCreation = new PetCreation(dogBuilder);
            var dog = petCreation.CreatePet();

            Assert.Equal("Friendly dog", dog.Name);
            Assert.Equal(PetType.Dog, dog.Type);
            Assert.Equal(new DateTime(2027, 07, 27), dog.BirthDate);
            Assert.Equal("Very fast", dog.Description);
            Assert.Equal("super url", dog.ImageUrl);
            Assert.True(dog.IsHealthy);
            Assert.Equal(9, dog.WeightInKg);
            Assert.Equal("3210987654321", dog.Rescuer.IdNumber);
            Assert.Equal("Vasile", dog.Rescuer.Name);
            Assert.Equal(new DateTime(2023, 03, 23), dog.Rescuer.DateOfBirth);
        }
    }
}