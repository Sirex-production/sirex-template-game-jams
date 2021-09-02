using System;
using Extensions;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Support
{
	public class InputSystem : MonoSingleton<InputSystem>, IPointerDownHandler, IPointerUpHandler, IDragHandler
	{
		public event Action<Vector2> OnTouchAction;
		public event Action<Vector2> OnReleaseAction;
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
			if (_isHolding || _deltaSwipe.magnitude < TemplateManager.Instance.TemplateSettingsData.MinimumDeltaSwipe || !_isAbleToInput)
				return;

			var absDelta = _deltaSwipe.Abs();

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