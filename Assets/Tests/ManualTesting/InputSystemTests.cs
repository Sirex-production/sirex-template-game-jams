using Support;
using UnityEngine;
using Extensions;

namespace Tests.Manual
{
    public class InputSystemTests : MonoBehaviour
    {
        [SerializeField] private bool debugSwipe = false;
        [SerializeField] private bool debugTouch = false;
        [SerializeField] private bool debugRelease = false;
        [SerializeField] private bool debugDrag = false;

        private void Start()
        {
            InputSystem.Instance.OnSwipeAction += DebugSwipe;
            InputSystem.Instance.OnTouchAction += DebugTouch;
            InputSystem.Instance.OnReleaseAction += DebugTouchRelease;
            InputSystem.Instance.OnDragAction += DebugDrag;
        }

        private void OnDestroy()
        {
            InputSystem.Instance.OnSwipeAction -= DebugSwipe;
            InputSystem.Instance.OnTouchAction -= DebugTouch;
            InputSystem.Instance.OnReleaseAction -= DebugTouchRelease;
            InputSystem.Instance.OnDragAction -= DebugDrag;
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