using PetShelter.Domain;
using System.ComponentModel;

namespace PetShelter.Api.Resources
{
    public class Fund
    {
        public string Name { get; set; }
        [DefaultValue(0)]
        public decimal DonationAmout { get; set; }
        public decimal Goal { get; set; }
        public DateTime DueDate { get; set; }

        [DefaultValue("Active")]
        public string Status { get; set; }
    }
}
