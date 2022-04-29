using NUnit.Framework;
using Support.Extensions;
using UnityEngine;

namespace Support.Tests.EditMode
{
    public sealed class ComponentExtensionsTest
    {
        private const int MAXIMUM_NUMBER_OF_CHILDREN = 5;

        private Transform _parentTransform;

        [SetUp]
        public void BeforeEach()
        {
            _parentTransform = new GameObject().transform;
            var numberOfChildren = Random.Range(0, MAXIMUM_NUMBER_OF_CHILDREN);
            
            for (var i = 0; i < numberOfChildren; i++)
            {
                var childTransform = new GameObject().transform;
                childTransform.SetParent(_parentTransform);
            }
        }
        
        [Test]
        public void AssignLayerToAllChildrenTest()
        {
            const int childLayer = 2;
            
            _parentTransform.SetLayerToAllChildren(childLayer);
            
            foreach (var childTransform in _parentTransform.GetComponentsInChildren<Transform>())
                Assert.AreEqual(childTransform.gameObject.layer, childLayer);
        }
    }
}