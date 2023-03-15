namespace BuilderPattern
{
    public class DogPetBuilder : PetBuilder
    {
        private readonly Pet _pet;

        public DogPetBuilder()
        {
            _pet = new Pet();
        }

        public void Name()
        {
            _pet.Name = "Friendly dog";
        }

        public void Type()
        {
            _pet.Type = PetType.Dog;
        }

        public void BirthDate()
        {
            _pet.BirthDate = new DateTime(2027, 07, 27);
        }

        public void Description()
        {
            _pet.Description = "Very fast";
        }

        public void ImageUrl()
        {
            _pet.ImageUrl = "super url";
        }

        public void IsHealthy()
        {
            _pet.IsHealthy = true;
        }

        public void WeightInKg()
        {
            _pet.WeightInKg = 9;
        }

        public void Rescuer()
        {
            _pet.Rescuer = new Person("3210987654321", "Vasile", new DateTime(2023, 03, 23));
        }

        public Pet GetPet()
        {
            return _pet;
        }
    }
}
