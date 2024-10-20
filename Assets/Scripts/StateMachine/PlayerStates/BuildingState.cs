public class BuildingState : IState
{
    PlayerController playerController;
    StateMachine stateMachine;
    PlayerInputReader input;
    
    
    public BuildingState(PlayerController playerController, PlayerInputReader input)
    {
        this.input = input;
        this.playerController = playerController;
    }
    
    public void OnEnter()
    {
        playerController.HideWeapon();
    }

    public void Update() { }

    public void FixedUpdate()
    {
        playerController.HandleMovement();
    }

    public void OnExit()
    {
        playerController.ShowWeapon();
    }
}