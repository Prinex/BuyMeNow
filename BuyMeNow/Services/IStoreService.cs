namespace BuyMeNow.Services;

public interface IStoreService
{
    Task<Store> GetStore(string name);
    Task<Store> GetStoreByID(int id);
    Task<List<Store>> GetStores();
    Task<bool> AddStore(Store model);
    void AddStores();
}

