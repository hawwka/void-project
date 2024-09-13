using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static InputActions;

[CreateAssetMenu(fileName = "PlayerInputReader", menuName = "ScriptableObjects/Input/PlayerInputReader")]
public class PlayerInputReader : ScriptableObject, IPlayerActions
{
    public event UnityAction Dash = delegate {  };
    public event UnityAction<bool> Fire = delegate {  };
    public event UnityAction<int> SelectPrimaryWeapon = delegate {  };
    public event UnityAction<int> SelectSecondaryWeapon = delegate {  };
    public event UnityAction<bool> Aim = delegate {  };
    

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
        if(context.action.IsPressed())
            Dash.Invoke();
    }

    public void OnPrimaryWeapon(InputAction.CallbackContext context)
    {
        SelectPrimaryWeapon.Invoke(0);
    }

    public void OnSecondWeapon(InputAction.CallbackContext context)
    {
        SelectSecondaryWeapon.Invoke(1);
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
