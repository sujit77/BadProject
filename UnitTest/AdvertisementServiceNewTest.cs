using System;
using BadProject;
using BadProject.Interfaces;
using BadProject.UnityRegistration;
using NUnit.Framework;
using Unity;

namespace UnitTest
{
    [TestFixture]
    public class AdvertisementServiceNewTest
    {
        private UnityRegister _unityRegister;
        private IAdvertisementService _advertisementService;

        [SetUp]
        public void SetUp()
        {
            _unityRegister = new UnityRegister();
            _unityRegister.Register();
            _advertisementService = _unityRegister.Container.Resolve<IAdvertisementService>();
        }

        [Test]
        public void Test_AdvService_GetsData_FromHttp_ThenSets_Cache()
        {
            var httpResult = _advertisementService.GetAdvertisement("Test");
            var cachedResult = _advertisementService.GetAdvertisement("Test");
            Assert.AreEqual(httpResult, cachedResult);
        }

        [Test]
        public void Test_AdvService_GetsData_FromBackUp_ThenSets_Cache()
        {
            _advertisementService = _unityRegister.Container.Resolve<IAdvertisementService>();
            for (int i = 0; i < 15; i++)
            {
                _advertisementService.CacheDataProvider.Errors.Enqueue(DateTime.Now);
            }

            var backUpResult = _advertisementService.GetAdvertisement("Test");
            var cachedResult = _advertisementService.GetAdvertisement("Test");
            Assert.AreEqual(backUpResult, cachedResult);
        }
    }
}