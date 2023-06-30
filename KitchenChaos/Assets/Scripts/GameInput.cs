using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
	private const string PLAYER_PREFS_BINDINGS = "InputBindings";
	public static GameInput Instance { get; private set; }

	public event EventHandler OnInteractAction;
	public event EventHandler OnInteractAlternateAction;
	public event EventHandler OnPauseAction;

	public enum Bindings
	{
		Move_up,
		Move_down,
		Move_left,
		Move_right,
		Interact,
		InteractAlternate,
		Pause,
	}

	private PlayerInputActions playerInputActions;

	private void Awake()
	{
		Instance = this;
		playerInputActions = new PlayerInputActions();

		if (PlayerPrefs.HasKey(PLAYER_PREFS_BINDINGS))
		{
			playerInputActions.LoadBindingOverridesFromJson(PlayerPrefs.GetString(PLAYER_PREFS_BINDINGS));
		}

		playerInputActions.Player.Enable();
		playerInputActions.Player.Interact.performed += Interact_performed;
		playerInputActions.Player.InteractAlternate.performed += InteractAlternate_performed;
		playerInputActions.Player.Pause.performed += Pause_performed;
	}

	private void OnDestroy()
	{
		playerInputActions.Player.Interact.performed -= Interact_performed;
		playerInputActions.Player.InteractAlternate.performed -= InteractAlternate_performed;
		playerInputActions.Player.Pause.performed -= Pause_performed;

		playerInputActions.Dispose();
	}

	private void Pause_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
	{
		OnPauseAction?.Invoke(this, EventArgs.Empty);
	}

	private void InteractAlternate_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
	{
		OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);
	}

	private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
	{
		OnInteractAction?.Invoke(this, EventArgs.Empty);
	}

	public Vector2 GetInputVector2Normalised()
	{
		Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();

		

		inputVector = inputVector.normalized;
		
		return inputVector;
	}

	public string GetBinding(Bindings bindings)
	{
		switch (bindings)
		{
			default:
			case Bindings.Move_up:
				return playerInputActions.Player.Move.bindings[1].ToDisplayString();
				
			case Bindings.Move_down:
				return playerInputActions.Player.Move.bindings[2].ToDisplayString();
				
			case Bindings.Move_left:
				return playerInputActions.Player.Move.bindings[3].ToDisplayString();
				
			case Bindings.Move_right:
				return playerInputActions.Player.Move.bindings[4].ToDisplayString();
				
			case Bindings.Interact:
				return playerInputActions.Player.Interact.bindings[0].ToDisplayString();
				
			case Bindings.InteractAlternate:
				return playerInputActions.Player.InteractAlternate.bindings[0].ToDisplayString();
				
			case Bindings.Pause:
				return playerInputActions.Player.Pause.bindings[0].ToDisplayString();
				
		}
	}

	public void RebindBinding(Bindings binding, Action onActionRebound	)
	{
		playerInputActions.Player.Disable();

		InputAction inputAction;
		int bindingIndex;
		switch (binding)
		{
			default:
			case Bindings.Move_up:
				inputAction = playerInputActions.Player.Move;
				bindingIndex = 1;
				break;
			case Bindings.Move_down:
				inputAction = playerInputActions.Player.Move;
				bindingIndex = 2;
				break;
			case Bindings.Move_left:
				inputAction = playerInputActions.Player.Move;
				bindingIndex = 3;
				break;
			case Bindings.Move_right:
				inputAction = playerInputActions.Player.Move;
				bindingIndex = 4;
				break;
			case Bindings.Interact:
				inputAction = playerInputActions.Player.Interact;
				bindingIndex = 0;
				break;
			case Bindings.InteractAlternate:
				inputAction = playerInputActions.Player.InteractAlternate;
				bindingIndex = 0;
				break;
			case Bindings.Pause:
				inputAction = playerInputActions.Player.Pause;
				bindingIndex = 0;
				break;


		}

		inputAction.PerformInteractiveRebinding(bindingIndex)
			.OnComplete(callback =>
			{
				callback.Dispose();
				playerInputActions.Player.Enable();
				onActionRebound();

				PlayerPrefs.SetString(PLAYER_PREFS_BINDINGS, playerInputActions.SaveBindingOverridesAsJson());
				PlayerPrefs.Save();
			})
			.Start();
	}
}
