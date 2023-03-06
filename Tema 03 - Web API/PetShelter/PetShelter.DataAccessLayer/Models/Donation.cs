namespace PetShelter.DataAccessLayer.Models;

public class Donation: IEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Amount { get; set; }
    public int DonorId { get; set; }

    public Person Donor { get; set; }
}