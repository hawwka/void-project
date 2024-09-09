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
}
