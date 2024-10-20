public class LocomotionState : IState
{
    PlayerController playerController;
    PlayerInputReader input;
    
    
    public LocomotionState(PlayerController playerController, PlayerInputReader input)
    {
        this.playerController = playerController;
        this.input = input;
    }
    
    public void OnEnter()
    {
        input.SelectPrimaryWeapon += playerController.SelectWeapon;
        input.SelectSecondaryWeapon += playerController.SelectWeapon;
    }

    public void OnExit()
    {
        input.SelectPrimaryWeapon -= playerController.SelectWeapon;
        input.SelectSecondaryWeapon -= playerController.SelectWeapon;
    }
    
    public void Update() { }

    public void FixedUpdate()
    {
        playerController.HandleMovement();
    }
}