using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static InputActions;

[CreateAssetMenu(fileName = "PlayerInputReader", menuName = "ScriptableObjects/Input/PlayerInputReader")]
public class PlayerInputReader : ScriptableObject, IPlayerActions
{
    public event UnityAction<Vector2> Move = delegate {  }; 
    public event UnityAction Dash = delegate {  };
    public event UnityAction<bool> Fire = delegate {  };
    public event UnityAction<int> SelectPrimaryWeapon = delegate {  };
    public event UnityAction<int> SelectSecondaryWeapon = delegate {  };
    

    private InputActions inputActions;


    private void OnEnable()
    {
        if (inputActions == null)
        {
            inputActions = new InputActions();
            inputActions.Player.SetCallbacks(this);
        }
    }

    public void EnablePlayerActions()
    {
        inputActions.Enable();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Move.Invoke(context.ReadValue<Vector2>());
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        //noop
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
}
