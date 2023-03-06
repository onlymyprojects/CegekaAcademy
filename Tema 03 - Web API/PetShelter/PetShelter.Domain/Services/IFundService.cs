namespace PetShelter.Domain.Services
{
    public interface IFundService
    {
        Task<IReadOnlyCollection<Fund>> GetAllFunds();
        Task<Fund> GetFund(int fundId);
        Task<int> CreateFundAsync(Person owner, Fund fund);
        Task DeleteFundAsync(int fundId);
    }
}
