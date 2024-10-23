using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static InputActions;

[CreateAssetMenu(fileName = "PlayerInputReader", menuName = "ScriptableObjects/Input/PlayerInputReader")]
public class PlayerInputReader : ScriptableObject, IPlayerActions
{
    public event UnityAction Dash = delegate { };
    public event UnityAction<bool> Fire = delegate { };
    public event UnityAction<int> OnAlphaPressed = delegate { };
    public event UnityAction Building = delegate { };
    public event UnityAction Reload = delegate { };
    public event UnityAction<bool> RotateLeft = delegate { };
    public event UnityAction<bool> RotateRight = delegate { };
    public event UnityAction RightMouseClicked = delegate { };

    private InputActions inputActions;

    public Vector3 Direction => inputActions.Player.Move.ReadValue<Vector2>();
    public Vector2 MousePosition => inputActions.Player.Look.ReadValue<Vector2>();

    private void OnEnable()
    {
        if (inputActions != null) return;

        inputActions = new InputActions();
        inputActions.Player.SetCallbacks(this);
    }

    public void EnablePlayerActions()
    {
        inputActions.Enable();
    }
    
    public void OnBuilding(InputAction.CallbackContext context)
    {
        Building.Invoke();
    }

    public void OnRotateLeft(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Started:
                RotateLeft.Invoke(true);
                break;
            case InputActionPhase.Canceled:
                RotateLeft.Invoke(false);
                break;
        }
    }

    public void OnRotateRight(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Started:
                RotateRight.Invoke(true);
                break;
            case InputActionPhase.Canceled:
                RotateRight.Invoke(false);
                break;
        }
    }

    public void OnMouseRightClick(InputAction.CallbackContext context)
    {
        if (context.action.IsPressed())
            RightMouseClicked.Invoke();
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Started:
                Fire.Invoke(true);
                break;
            case InputActionPhase.Canceled:
                Fire.Invoke(false);
                break;
        }
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.action.IsPressed())
            Dash.Invoke();
    }

    public void OnAlpha1(InputAction.CallbackContext context)
    {
        OnAlphaPressed.Invoke(0);
    }

    public void OnAlpha2(InputAction.CallbackContext context)
    {
        OnAlphaPressed.Invoke(1);
    }

    public void OnAlpha3(InputAction.CallbackContext context)
    {
        OnAlphaPressed.Invoke(2);
    }

    public void OnReload(InputAction.CallbackContext context)
    {
        Reload.Invoke();
    }    
    public void OnMove(InputAction.CallbackContext context)
    {
        // ignore
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        // ignore
    }
}