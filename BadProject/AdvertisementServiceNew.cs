using BadProject.Interfaces;
using ThirdParty;

namespace BadProject
{
    public class AdvertisementServiceNew : IAdvertisementService
    {
        private readonly IDataProvider _httpDataProvider;
        private readonly IDataProvider _backUpDataProvider;
        private readonly object _lockObj = new object();

        public AdvertisementServiceNew(ICacheDataProvider cachedDataProvider, IDataProvider httpDataProvider,
            IDataProvider backUpDataProvider)
        {
            CacheDataProvider = cachedDataProvider;
            _httpDataProvider = httpDataProvider;
            _backUpDataProvider = backUpDataProvider;
        }

        public Advertisement GetAdvertisement(string id)
        {
            lock (_lockObj)
            {
                // Use Cache if available
                var advertisement = CacheDataProvider.GetData(id);
                // If Cache is empty and ErrorCount<10 then use HTTP provider
                if ((advertisement == null) && (CacheDataProvider.ErrorCount < 10))
                {
                    advertisement = _httpDataProvider.GetData(id);
                }

                // if needed try to use Backup provider
                return advertisement ??= _backUpDataProvider.GetData(id);
            }
        }

        public ICacheDataProvider CacheDataProvider { get; set; }
    }
}