using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Support;

namespace Tests.Playmode
{
    public class MonoSingletonTest
    {
        private const int NUMBER_OF_SINGLETONS = 10;
        private List<SingletonImplementation> _singletons = new List<SingletonImplementation>(NUMBER_OF_SINGLETONS);

        private class SingletonImplementation : MonoSingleton<SingletonImplementation>
        {
        }

        [SetUp]
        public void Before()
        {
            for (var i = 0; i < NUMBER_OF_SINGLETONS; i++)
            {
                var go = new GameObject();
                _singletons.Add(go.AddComponent<SingletonImplementation>());
            }
        }

        [UnityTest]
        public IEnumerator InstanceTest()
        {
            yield return null;

            var numberOfAliveObjects = _singletons.Count(item => item != null);

            Assert.AreEqual(numberOfAliveObjects, 1);
        }
    }
}