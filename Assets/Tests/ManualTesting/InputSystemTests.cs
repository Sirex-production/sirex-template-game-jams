using UnityEngine;
using Support.Extensions;
using Zenject;

namespace Support.Tests.Manual
{
    public class InputSystemTests : MonoBehaviour
    {
        [SerializeField] private bool debugDirectionalSwipe = false;
        [SerializeField] private bool debugSwipe = false;
        [SerializeField] private bool debugTouch = false;
        [SerializeField] private bool debugRelease = false;
        [SerializeField] private bool debugDrag = false;

        [Inject] private readonly TouchScreenInputSystem _touchScreenInputSystem;
        
        private void Start()
        {
            _touchScreenInputSystem.OnDirectionalSwipeAction += DebugDirectionalSwipe;
            _touchScreenInputSystem.OnSwipeAction += DebugSwipe;
            _touchScreenInputSystem.OnTouchAction += DebugTouch;
            _touchScreenInputSystem.OnReleaseAction += DebugTouchRelease;
            _touchScreenInputSystem.OnDragAction += DebugDrag;
        }

        private void OnDestroy()
        {
            _touchScreenInputSystem.OnDirectionalSwipeAction -= DebugDirectionalSwipe;
            _touchScreenInputSystem.OnSwipeAction -= DebugSwipe;
            _touchScreenInputSystem.OnTouchAction -= DebugTouch;
            _touchScreenInputSystem.OnReleaseAction -= DebugTouchRelease;
            _touchScreenInputSystem.OnDragAction -= DebugDrag;
        }

        private void DebugDirectionalSwipe(Vector2 swipeDirection)
        {
            if(debugDirectionalSwipe)
                this.SafeDebug($"Directional swipe performed {swipeDirection}");
        }

        private void DebugSwipe(SwipeDirection swipeDirection)
        {
            if(debugSwipe)
                this.SafeDebug($"Swipe performed {swipeDirection}");
        }

        private void DebugTouch(Vector2 touchPosition)
        {
            if(debugTouch)
                this.SafeDebug($"Touch performed {touchPosition}");
        }
        
        private void DebugTouchRelease(Vector2 releasePosition)
        {
            if(debugRelease)
                this.SafeDebug($"Touch release performed {releasePosition}");
        }
        
        private void DebugDrag(Vector2 dragDirection)
        {
            if(debugDrag)
                this.SafeDebug($"Drag preformed performed {dragDirection}");
        }
    }
}