using System.Linq;
using System.Runtime.Caching;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BadProject
{
    public class InMemoryCache
    {
        private static InMemoryCache _instance;
        public MemoryCache Cache;
        private static readonly object Lck = new object();

        private InMemoryCache()
        {
            Cache = new MemoryCache("InMemory");
        }

        public static InMemoryCache Instance
        {
           
            get
            {
                lock (Lck)
                {
                    return _instance ??= new InMemoryCache();
                }

                
            }
        }
    }
}
