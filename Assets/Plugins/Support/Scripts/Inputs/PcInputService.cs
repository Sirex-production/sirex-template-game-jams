using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Support.Inputs
{
	public sealed class PcInputService : ITickable
	{
		private PcInputActions _pcInputActions = new();
		private bool _isEnabled = false;

		private InputAction _movement;
		private InputAction _rotate;
		private InputAction _jump;
		private InputAction _crouch;
		private InputAction _shift;
		private InputAction _attack;
		private InputAction _aim;
		
		public event Action<Vector2> OnMoveInput;
		public event Action<Vector2> OnRotateInput;
		public event Action OnJumpInput;
		public event Action OnCrouchInput;
		public event Action OnShiftInput;
		public event Action OnAttackInput;
		public event Action OnAimInput;

		public bool IsEnabled
		{
			get => _isEnabled;
			set
			{
				_isEnabled = value;
				
				if(_isEnabled)
					_pcInputActions.Enable();
				else
					_pcInputActions.Disable();
			}
		}

		public PcInputService()
		{
			IsEnabled = true;

			_movement = _pcInputActions.Control.Movement;
			_rotate = _pcInputActions.Control.Rotation;
			_jump = _pcInputActions.Control.Jump;
			_crouch = _pcInputActions.Control.Crouch;
			_shift = _pcInputActions.Control.Shift;

			_attack = _pcInputActions.Combat.Attack;
			_aim = _pcInputActions.Combat.Aim;
		}

		public void Tick()
		{
			ReceiveControlInput();
			ReceiveCombatInput();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void ReceiveControlInput()
		{
			var movementInputVector = _movement.ReadValue<Vector2>();
			var rotationDeltaInputVector = _rotate.ReadValue<Vector2>();

			OnMoveInput?.Invoke(movementInputVector);
			OnRotateInput?.Invoke(rotationDeltaInputVector);
			
			if(_jump.WasPerformedThisFrame())
				OnJumpInput?.Invoke();
			
			if(_crouch.WasPerformedThisFrame())
				OnCrouchInput?.Invoke();
			
			if(_shift.WasPerformedThisFrame())
				OnShiftInput?.Invoke();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void ReceiveCombatInput()
		{
			if(_attack.WasPerformedThisFrame())
				OnAttackInput?.Invoke();
			
			if(_aim.WasPerformedThisFrame())
				OnAimInput?.Invoke();
		}
	}
}