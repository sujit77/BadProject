using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThirdParty;

namespace BadProject.Interfaces
{
    // single responsibility vs open/closed principle of SOLID 
    public interface IDataProvider
    {
        Advertisement GetData(string id);

    }
}
