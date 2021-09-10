using System;
using Extensions;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Support
{
	public class InputSystem : MonoSingleton<InputSystem>, IPointerDownHandler, IPointerUpHandler, IDragHandler
	{
		[SerializeField] private float minimumDeltaSwipe = 2f;
		
		public event Action<Vector2> OnTouchAction;
		public event Action<Vector2> OnReleaseAction;
		public event Action<Vector2> OnDirectionalSwipeAction;
		public event Action<SwipeDirection> OnSwipeAction;
		public event Action<Vector2> OnDragAction;

		private bool _isHolding = true;
		private bool _isAbleToInput = true;
		private Vector2 _deltaSwipe = Vector2.zero;

		public void OnPointerDown(PointerEventData eventData)
		{
			if (!_isAbleToInput)
				return;

			OnTouchAction?.Invoke(eventData.position);
			_isHolding = true;
		}

		public void OnPointerUp(PointerEventData eventData)
		{
			if (!_isAbleToInput)
				return;

			OnReleaseAction?.Invoke(eventData.position);
			_isHolding = false;
			_deltaSwipe = eventData.delta;

			CheckIfSwipeWasPerformed();
		}

		public void OnDrag(PointerEventData eventData)
		{
			if (!_isAbleToInput)
				return;

			OnDragAction?.Invoke(eventData.delta);
		}

		private void CheckIfSwipeWasPerformed()
		{
			if (_isHolding || _deltaSwipe.magnitude < minimumDeltaSwipe || !_isAbleToInput)
				return;

			var absDelta = _deltaSwipe.Abs();

			OnDirectionalSwipeAction?.Invoke(_deltaSwipe.normalized);
			if (_deltaSwipe.x > 0 && absDelta.x > absDelta.y) OnSwipeAction?.Invoke(SwipeDirection.Right);
			if (_deltaSwipe.x < 0 && absDelta.x > absDelta.y) OnSwipeAction?.Invoke(SwipeDirection.Left);
			if (_deltaSwipe.y > 0 && absDelta.x < absDelta.y) OnSwipeAction?.Invoke(SwipeDirection.Up);
			if (_deltaSwipe.y < 0 && absDelta.x < absDelta.y) OnSwipeAction?.Invoke(SwipeDirection.Down);
			
			_deltaSwipe = Vector2.zero;
		}
	}
	
	public enum SwipeDirection
	{
		Left,
		Right,
		Up,
		Down
	}
}