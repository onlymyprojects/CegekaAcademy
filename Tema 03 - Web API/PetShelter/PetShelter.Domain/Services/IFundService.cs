namespace PetShelter.Domain.Services
{
    public interface IFundService
    {
        Task<IReadOnlyCollection<Fund>> GetAllFunds();
        Task<Fund> GetFund(int fundId);
        Task<int> CreateFundAsync(Person owner, Fund fund);
        Task<int> AddDonationAsync(int fundId, Donation donation);

        Task DeleteFundAsync(int fundId);
    }
}
