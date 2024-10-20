public class AttackState : IState
{
    PlayerController playerController;
    PlayerInputReader input;
    
    public AttackState(PlayerController playerController, PlayerInputReader input)
    {
        this.input = input;
        this.playerController = playerController;
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

    public void Update()
    {
        playerController.HandleAiming();
        
        if (playerController.IsRotationStopped())
            playerController.SelectedWeapon.Attack();
    }

    public void FixedUpdate()
    {
        playerController.HandleMovement();
    }
}