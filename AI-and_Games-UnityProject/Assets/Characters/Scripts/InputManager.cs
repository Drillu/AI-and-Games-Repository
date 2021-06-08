using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; set; }
    public Vector2 MousePosition { get;private set; }
    public bool IsMouseRightButtonDown { get;private set; }
    public bool IsMouseLeftButtonDown { get; private set; }

    private void Awake()
    {
        if(Instance && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
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
        if (ctx.performed)
        {
            IsMouseRightButtonDown = true;
        }
        else
        {
            IsMouseRightButtonDown = false;
        }
    }

    public void OnMouseLeftButtonClicked(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            IsMouseLeftButtonDown = true;
        }
        else
        {
            IsMouseLeftButtonDown = false;
        }
    }

    public void OnMouseScrolled(InputAction.CallbackContext ctx)
    {

    }

}
