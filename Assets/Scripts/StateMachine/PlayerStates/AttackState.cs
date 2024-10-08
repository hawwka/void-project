public class AttackState : IState
{
    PlayerController playerController;

    public AttackState(PlayerController playerController)
    {
        this.playerController = playerController;
    }

    public void OnEnter()
    {
        
    }

    public void OnExit()
    {
        
    }

    public void Update()
    {
        playerController.HandleAiming();
        
        if(playerController.IsRotationStopped())
            playerController.SelectedWeapon.Attack();
    }

    public void FixedUpdate()
    {
        playerController.HandleMovement();
    }
}