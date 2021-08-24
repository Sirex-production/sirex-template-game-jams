using Extensions;
using NUnit.Framework;
using UnityEngine;

namespace Tests.Playmode
{
    public class VectorExtensionsTest : MonoBehaviour
    {
        private Vector2 _vector2;
        private Vector3 _vector3;

        [SetUp]
        public void Before()
        {
            _vector2 = new Vector2(Random.Range(float.MinValue, 0), Random.Range(float.MinValue, 0));
            _vector3 = new Vector3(Random.Range(float.MinValue, 0), Random.Range(float.MinValue, 0), Random.Range(float.MinValue, 0));
        }

        [Test]
        public void AbsTest()
        {
            _vector2 = _vector2.Abs();
            _vector3 = _vector3.Abs();
            
            Assert.Positive(_vector2.x);
            Assert.Positive(_vector2.y);
            
            Assert.Positive(_vector3.x);
            Assert.Positive(_vector3.y);
            Assert.Positive(_vector3.z);
        }
    }
}