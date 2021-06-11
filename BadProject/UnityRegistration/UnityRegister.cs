using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BadProject.Interfaces;
using Unity;
using Unity.Injection;

namespace BadProject.UnityRegistration
{
    public class UnityRegister
    {
        public IUnityContainer Container = new UnityContainer().AddExtension(new Diagnostic());
  

        public void Register()
        {
            Container.RegisterType<IDataProvider, HttpDataProvider>("Http");
            Container.RegisterType<IDataProvider, BackUpProvider>("BackUp");
            Container.RegisterType<ICacheDataProvider, CacheDataProvider>();

            Container.RegisterType<IAdvertisementService, AdvertisementServiceNew>();
            Container.RegisterType<AdvertisementServiceNew>(
                new InjectionConstructor(new ResolvedParameter<CacheDataProvider>(), new ResolvedParameter<IDataProvider>("Http"), new ResolvedParameter<IDataProvider>("BackUp")));
        }
    }
}
