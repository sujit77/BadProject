using ThirdParty;

namespace BadProject.Interfaces
{
    
    public interface IAdvertisementService
    {
        Advertisement GetAdvertisement(string id);
        ICacheDataProvider CacheDataProvider { get; set; }
    }
}
