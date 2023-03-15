namespace BuilderPattern
{
    public interface PetBuilder
    {
        void Name();
        void Type();
        void BirthDate();
        void Description();
        void ImageUrl();
        void IsHealthy();
        void WeightInKg();
        void Rescuer();
        Pet GetPet();
    }
}
