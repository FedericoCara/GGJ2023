using System;
using UnityEngine;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
	public class StarterAssetsInputs : MonoBehaviour
	{
		[Header("Character Input Values")]
		public Vector2 move;
		public Vector2 look;
		public bool jump;
		public bool sprint;
        public bool crouch;
		public InputActionAsset attackInput;
        public bool attack;
		public bool preparingAttack;

        [Header("Testing")] 
        public bool respawn;
        
        [Header("Movement Settings")]
		public bool analogMovement;

		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;

		private InputActionMap inputActions;

		private void Awake()
		{
			inputActions = attackInput.FindActionMap("Player");
			inputActions.Enable();
			InputAction Attack = inputActions.FindAction("Attack");
			Attack.performed+= AttackCallback;
			Attack.started +=PrepareAttackCallback;
			Attack.canceled += CancelAttack;
		}

		private void CancelAttack(InputAction.CallbackContext obj)
		{
            PrepareAttack(false);
        }
		private void PrepareAttackCallback(InputAction.CallbackContext obj)
		{
			PrepareAttack(true);
		}

		private void AttackCallback(InputAction.CallbackContext obj)
		{
            PrepareAttack(false);
            AttackInput(true);
		}

#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
		public void OnMove(InputValue value)
		{
			MoveInput(value.Get<Vector2>());
		}

		public void OnLook(InputValue value)
		{
			if(cursorInputForLook)
			{
				LookInput(value.Get<Vector2>());
			}
		}

		public void OnJump(InputValue value)
		{
			JumpInput(value.isPressed);
		}

		public void OnSprint(InputValue value)
		{
			SprintInput(value.isPressed);
		}
		public void OnCrouch(InputValue value)
		{
			CrouchInput(value.isPressed);
		}
        public void OnAttack(InputValue value)
        {
            //AttackInput(value.isPressed);
        }

        public void OnRespawn(InputValue value)
		{
			RespawnInput(value.isPressed);
		}

#endif


		public void MoveInput(Vector2 newMoveDirection)
		{
			move = newMoveDirection;
		}

		public void LookInput(Vector2 newLookDirection)
		{
			look = newLookDirection;
		}

		public void JumpInput(bool newJumpState)
		{
			jump = newJumpState;
		}

		public void SprintInput(bool newSprintState)
		{
			sprint = newSprintState;
		}

		public void CrouchInput(bool newCrouchState)
		{
			crouch = newCrouchState;
		}
		public void PrepareAttack(bool newPrepareAttackState)
		{
			preparingAttack = newPrepareAttackState;
        }
        public void AttackInput(bool newAttackState)
        {
            attack = newAttackState;
		}

        private void RespawnInput(bool newRespawnState)
		{
			respawn = newRespawnState;
		}

		private void OnApplicationFocus(bool hasFocus)
		{
			SetCursorState(cursorLocked);
		}

		private void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}
	}
	
}