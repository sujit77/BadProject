using System;
using System.Collections.Generic;
using System.Runtime.Caching;


namespace BadProject.Interfaces
{
    // interface segregation from SOLID 
    public interface ICacheDataProvider : IDataProvider
    {
        Queue<DateTime> Errors { get; set; }
        int ErrorCount { get; }
        MemoryCache MemoryCache { get; }
    }
}