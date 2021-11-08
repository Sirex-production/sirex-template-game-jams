using System;
using Extensions;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Support
{
	/// <summary>
	/// Class that is responsible for reading user input
	/// </summary>
	public class InputSystem : MonoSingleton<InputSystem>, IPointerDownHandler, IPointerUpHandler, IDragHandler
	{
		[SerializeField] private float minimumDeltaSwipe = 2f;
		
		/// <summary> Event that activates when user touches the screen. Takes a Vector2 that represent touch position on the screen </summary>
		public event Action<Vector2> OnTouchAction;
		/// <summary> Event that activates when user releases the screen. Takes a Vector2 that represent release position on the screen </summary>
		public event Action<Vector2> OnReleaseAction;
		/// <summary> Event that activates when user performs swipe. Takes a Vector2 that represent swipe direction </summary>
		public event Action<Vector2> OnDirectionalSwipeAction;
		/// <summary> Event that activates when user performs swipe. Takes a SwipeDirection that represent swipe direction </summary>
		public event Action<SwipeDirection> OnSwipeAction;
		/// <summary> Event that activates when user drags. Takes a Vector2 that represent drag direction </summary>
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

			if(absDelta.magnitude <= 0)
				return;
			
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