using System.Collections;
using Extensions;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.Playmode
{
    public class MonoBehaviourExtensionTest
    {
        private class MonoBehaviourImplementation : MonoBehaviour { }

        private const float TIME_ERROR_OFFSET = .1f;
        
        private GameObject _objectToTurnOff;
        private GameObject _objectToTurnOn;
        private MonoBehaviour _monoBehaviourForTurningGameObjectOff; 
        private MonoBehaviour _monoBehaviourForTurningGameObjectOn;

        private MonoBehaviour _monoBehaviourForTestingCoroutines;
        private Coroutine _testingCoroutine;
        
        [SetUp]
        public void BeforeEach()
        {
            _objectToTurnOff = new GameObject();
            _objectToTurnOn = new GameObject();

            _monoBehaviourForTurningGameObjectOff = _objectToTurnOff.AddComponent<MonoBehaviourImplementation>();
            _monoBehaviourForTurningGameObjectOn = _objectToTurnOn.AddComponent<MonoBehaviourImplementation>();
            
            _objectToTurnOff.SetActive(true);
            _objectToTurnOn.SetActive(false);

            var tmpObjectForTestingCoroutines = new GameObject();
            _monoBehaviourForTestingCoroutines = tmpObjectForTestingCoroutines.AddComponent<MonoBehaviourImplementation>();
        }

        [TearDown]
        public void AfterEach()
        {
            if (_monoBehaviourForTestingCoroutines != null)
            {
                if (_testingCoroutine != null)
                    _monoBehaviourForTestingCoroutines.StopCoroutine(_testingCoroutine);
                
                Object.Destroy(_monoBehaviourForTestingCoroutines.gameObject);
            }
            
            _testingCoroutine = null;
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
        
        [UnityTest]
        public IEnumerator RepeatCoroutineTestStartingWithPause()
        {
            int numberOfPauses = Random.Range(1, 3);
            int incrementor = Random.Range(int.MinValue, int.MaxValue);
            int initialIncrementorValue = incrementor;
            
            _monoBehaviourForTestingCoroutines.RepeatCoroutine(1, () => incrementor++, true);

            yield return new WaitForSeconds(numberOfPauses + TIME_ERROR_OFFSET);

            Assert.AreEqual(incrementor, initialIncrementorValue + numberOfPauses);
        }
        
        [UnityTest]
        public IEnumerator RepeatCoroutineTestStartingWithoutPause()
        {
            int numberOfPauses = Random.Range(1, 3);
            int incrementor = Random.Range(int.MinValue, int.MaxValue);
            int initialIncrementorValue = incrementor;
            
            _monoBehaviourForTestingCoroutines.RepeatCoroutine(1, () => incrementor++);

            yield return new WaitForSeconds(numberOfPauses + TIME_ERROR_OFFSET);

            Assert.AreEqual(incrementor, initialIncrementorValue + numberOfPauses + 1);
        }
        
        [UnityTest]
        public IEnumerator DoAfterNextFrameCoroutineTest()
        {
            bool testingBoolean = Random.value > .5f;
            bool initialTestingBoolean = testingBoolean;

            _monoBehaviourForTestingCoroutines.DoAfterNextFrameCoroutine(() => testingBoolean = !testingBoolean);
            
            Assert.AreEqual(testingBoolean, initialTestingBoolean);

            yield return null;

            Assert.AreNotEqual(testingBoolean, initialTestingBoolean);
        }
        
        [UnityTest]
        public IEnumerator WaitAndDoCoroutineTest()
        {
            float timeToWait = Random.Range(1f, 3f);
            bool testingBoolean = Random.value > .5f;
            bool initialTestingBoolean = testingBoolean;

            _monoBehaviourForTestingCoroutines.WaitAndDoCoroutine(timeToWait, () => testingBoolean = !testingBoolean);
            
            Assert.AreEqual(testingBoolean, initialTestingBoolean);
            
            yield return new WaitForSeconds(timeToWait + TIME_ERROR_OFFSET);
            
            Assert.AreNotEqual(testingBoolean, initialTestingBoolean);
        }

        [UnityTest]
        public IEnumerator LerpCoroutineTest()
        {
            const float SPEED = 100;
            float aValue = Random.Range(-1000, 1000);
            float bValue = Random.Range(-1000, 1000);

            if (aValue == bValue) 
                bValue += Random.Range(-500, 500);

            float timeToWait = Mathf.Abs(aValue - bValue) / SPEED;
            float testingValue = 0;
            
            void Lerp(float intermediateValue)
            {
                testingValue = intermediateValue;
            }
            
            _monoBehaviourForTestingCoroutines.LerpCoroutine(SPEED, aValue, bValue, Lerp);

            yield return new WaitForSeconds(timeToWait / 2);

            if(bValue > aValue)
                Assert.Greater(testingValue, aValue);
            else if (bValue < aValue)
                Assert.Less(testingValue, aValue);
        }
    }
}