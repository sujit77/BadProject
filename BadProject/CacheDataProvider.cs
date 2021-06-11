using System;
using System.Collections.Generic;
using System.Runtime.Caching;
using BadProject.Interfaces;
using ThirdParty;

namespace BadProject
{
    public class CacheDataProvider : ICacheDataProvider
    {
        public CacheDataProvider()
        {
            MemoryCache = InMemoryCache.Instance.Cache;
            Errors = SingleQueue.Instance.MyQueue;
        }

        public Advertisement GetData(string id)
        {
            // Use Cache if available
            Adv = (Advertisement) MemoryCache.Get($"AdvKey_{id}");

            // Count HTTP error timestamps in the last hour
            while (Errors.Count > 20) Errors.Dequeue();
            ErrorCount = 0;
            foreach (var dat in Errors)
            {
                if (dat > DateTime.Now.AddHours(-1))
                {
                    ErrorCount++;
                }
            }

            return Adv;
        }

        public Queue<DateTime> Errors { get; set; }
        public int ErrorCount { get; set; }
        public MemoryCache MemoryCache { get; set; }
        public Advertisement Adv { get; set; }
    }
}