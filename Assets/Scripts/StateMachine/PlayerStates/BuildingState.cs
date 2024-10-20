public class BuildingState : IState
{
    PlayerController playerController;
    StateMachine stateMachine;
    PlayerInputReader input;
    BuildingSystem buildingSystem;

    
    public BuildingState(PlayerController playerController, PlayerInputReader input, BuildingSystem buildingSystem)
    {
        this.input = input;
        this.playerController = playerController;
        this.buildingSystem = buildingSystem;
    }

    public void OnEnter()
    {
        playerController.HideWeapon();
        
        buildingSystem.SelectBuilding();
        
        input.Fire += buildingSystem.Place;
        input.OnAlphaPressed += buildingSystem.SelectBuilding;
        input.RotateLeft += buildingSystem.HandleRotationLeft;
        input.RotateRight += buildingSystem.HandleRotationRight;
    }

    public void OnExit()
    {
        playerController.ShowWeapon();
        
        buildingSystem.HideBuilding();

        input.Fire -= buildingSystem.Place;
        input.OnAlphaPressed -= buildingSystem.SelectBuilding;
        input.RotateLeft -= buildingSystem.HandleRotationLeft;
        input.RotateRight -= buildingSystem.HandleRotationRight;
    }
    
    public void Update() { }

    public void FixedUpdate()
    {
        playerController.HandleMovement();
    }
}