using Extensions;
using NUnit.Framework;
using UnityEngine;

namespace Tests.Playmode
{
    public class MonoBehaviourExtensionTest
    {
        private class MonoBehaviourImplementation : MonoBehaviour { }
        
        private GameObject _objectToTurnOff;
        private GameObject _objectToTurnOn;
        private MonoBehaviour _monoBehaviourForTurningGameObjectOff; 
        private MonoBehaviour _monoBehaviourForTurningGameObjectOn;
        
        [SetUp]
        public void Before()
        {
            _objectToTurnOff = new GameObject();
            _objectToTurnOn = new GameObject();

            _monoBehaviourForTurningGameObjectOff = _objectToTurnOff.AddComponent<MonoBehaviourImplementation>();
            _monoBehaviourForTurningGameObjectOn = _objectToTurnOn.AddComponent<MonoBehaviourImplementation>();
            
            _objectToTurnOff.SetActive(true);
            _objectToTurnOn.SetActive(false);
        }

        [Test]
        public void SetGameObjectInactiveTest()
        {
            Assert.True(_objectToTurnOff.activeSelf);

            _monoBehaviourForTurningGameObjectOff.SetGameObjectInactive();
            
            Assert.True(!_objectToTurnOff.activeSelf);
        }
        
        [Test]
        public void SetGameObjectActiveTest()
        {
            Assert.True(!_objectToTurnOn.activeSelf);
            
            _monoBehaviourForTurningGameObjectOn.SetGameObjectActive();

            Assert.True(_objectToTurnOn.activeSelf);
        }
    }
}