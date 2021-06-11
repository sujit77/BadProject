using System;
using BadProject.Interfaces;
using ThirdParty;

namespace BadProject
{
    public class BackUpProvider : IDataProvider
    {
        private readonly CacheDataProvider _adv;
        public BackUpProvider(CacheDataProvider cdpv)
        {
            _adv = cdpv;
        }
        public Advertisement GetData(string id)
        {
            _adv.Adv = SQLAdvProvider.GetAdv(id);

            if (_adv != null)
            {
                _adv.MemoryCache.Set($"AdvKey_{id}", _adv.Adv, DateTimeOffset.Now.AddMinutes(5));
            }

            return _adv.Adv;
        }
    }
}