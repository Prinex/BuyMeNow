namespace BuyMeNow.Services;

public interface IStoreService
{
    Task<Store> GetStore(string name);
    Task<List<Store>> GetStores();
    Task<bool> AddStore(Store model);
    void AddStores();
}

