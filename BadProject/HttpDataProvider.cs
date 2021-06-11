using System;
using System.Configuration;
using System.Threading;
using BadProject.Interfaces;
using ThirdParty;

namespace BadProject
{
    public class HttpDataProvider : IDataProvider
    {
        private readonly CacheDataProvider _adv;

        public HttpDataProvider(CacheDataProvider cdpv)
        {
            _adv = cdpv;
        }

        public Advertisement GetData(string id)
        {
            int retry = 0;
            do
            {
                retry++;
                try
                {
                    var dataProvider = new NoSqlAdvProvider();
                    _adv.Adv = dataProvider.GetAdv(id);
                }
                catch
                {
                    Thread.Sleep(1000);
                    _adv.Errors.Enqueue(DateTime.Now); // Store HTTP error timestamp              
                }
            } while ((_adv.Adv == null) && (retry < int.Parse(ConfigurationManager.AppSettings["RetryCount"])));

            if (_adv.Adv != null)
            {
                _adv.MemoryCache.Set($"AdvKey_{id}", _adv.Adv, DateTimeOffset.Now.AddMinutes(5));
            }

            return _adv.Adv;
        }
    }
}