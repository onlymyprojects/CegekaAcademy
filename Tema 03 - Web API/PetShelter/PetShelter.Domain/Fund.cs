namespace PetShelter.Domain
{
    public class Fund : FundInfo, INamedEntity
    {
        public int Id { get; set; }
        public FundStatus Status { get; set; }

        public Person Owner { get; set; }

        public Fund(FundStatus status, int id = 0)
        {
            Status = status;
            Id = id;
        }
    }
}
