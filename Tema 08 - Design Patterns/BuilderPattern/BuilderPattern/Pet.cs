namespace BuilderPattern
{
    public class Pet
    {
        public string Name { get; set; }
        public PetType Type { get; set; }
        public DateTime BirthDate { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public bool IsHealthy { get; set; }
        public decimal WeightInKg { get; set; }
        public Person Rescuer { get; set; }
    }
}
