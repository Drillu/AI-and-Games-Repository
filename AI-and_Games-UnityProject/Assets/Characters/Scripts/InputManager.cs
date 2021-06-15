using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
	public static InputManager Instance { get; set; }
	public Vector2 MousePosition { get; private set; }
	public bool IsMouseRightButtonDown { get; private set; }
	public bool IsMouseLeftButtonDown { get; private set; }
	public bool IsCancelButtonPressed { get; private set; }
	public bool IsShowInventoryButtonPressed { get; private set; }

	private void Awake()
	{
		if (Instance && Instance != this)
		{
			Destroy(gameObject);
		}
		else
		{
			Instance = this;
			DontDestroyOnLoad(this);
		}
	}

	private void LateUpdate()
	{
		IsCancelButtonPressed = false;
		IsShowInventoryButtonPressed = false;
		IsMouseLeftButtonDown = false;
		IsMouseRightButtonDown = false;
	}

	public void OnMouseMove(InputAction.CallbackContext ctx)
	{
		if (ctx.performed)
		{
			MousePosition = ctx.ReadValue<Vector2>();
		}
	}

	public void OnMouseRightButtonClicked(InputAction.CallbackContext ctx)
	{
		IsMouseRightButtonDown = ctx.canceled;
	}

	public void OnMouseLeftButtonClicked(InputAction.CallbackContext ctx)
	{
		IsMouseLeftButtonDown = ctx.canceled;
	}

	public void OnMouseScrolled(InputAction.CallbackContext ctx)
	{

	}

	public void OnCancelButtonDown(InputAction.CallbackContext ctx)
	{
		IsCancelButtonPressed = ctx.canceled;
		if (ctx.canceled)
		{
			Debug.Log("ESC pressed");
		}
	}

	public void OnShowInventoryButtonPressed(InputAction.CallbackContext ctx)
	{
		IsShowInventoryButtonPressed = ctx.canceled;
	}

}
