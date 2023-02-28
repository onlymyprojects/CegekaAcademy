using Homework.Common.Enums;

namespace Homework.DataAccessLayer.Models
{
    public class Pet : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public PetType Type { get; set; }
        public decimal WeightInKg { get; set; }
        public bool IsHealthy { get; set; }
        public bool IsSheltered { get; set; }
        public int? RescuerId { get; set; }
        public int? AdopterId { get; set; }

        public Person Adopter { get; set; }
        public Person Rescuer { get; set; }
    }
}
