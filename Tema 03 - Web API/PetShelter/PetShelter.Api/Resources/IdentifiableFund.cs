namespace PetShelter.Api.Resources
{
    public class IdentifiableFund : Fund
    {
        public int Id { get; set; }

        public Person Owner { get; set; }
    }
}
