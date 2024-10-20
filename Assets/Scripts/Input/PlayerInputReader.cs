using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static InputActions;

[CreateAssetMenu(fileName = "PlayerInputReader", menuName = "ScriptableObjects/Input/PlayerInputReader")]
public class PlayerInputReader : ScriptableObject, IPlayerActions
{
    public event UnityAction Dash = delegate {  };
    public event UnityAction<bool> Fire = delegate {  };
    public event UnityAction<int> OnAlphaPressed = delegate {  };
    public event UnityAction<bool> Aim = delegate {  };
    
    public event UnityAction Building = delegate {  };
    

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

    public void OnMove(InputAction.CallbackContext context)
    {
        // noon
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        // noon
    }
    
    public void OnBuilding(InputAction.CallbackContext context)
    {
        Building.Invoke();
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
        OnAlphaPressed?.Invoke(2);
    }

    public void OnAim(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Started:
                Aim.Invoke(true);
                break;
            case InputActionPhase.Canceled:
                Aim.Invoke(false);
                break;
        }
    }
}
