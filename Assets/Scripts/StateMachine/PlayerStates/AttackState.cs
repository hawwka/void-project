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
        playerController.SelectedWeapon.Attack();

        playerController.HandleAiming();
    }

    public void FixedUpdate()
    {
        playerController.HandleMovement();
    }
}