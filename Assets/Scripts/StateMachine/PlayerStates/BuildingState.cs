// public class BuildingState : IState
// {
//     Player player;
//     StateMachine stateMachine;
//     PlayerInputReader input;
//     BuildingSystem buildingSystem;
//
//     
//     public BuildingState(Player player, PlayerInputReader input, BuildingSystem buildingSystem)
//     {
//         this.input = input;
//         this.player = player;
//         this.buildingSystem = buildingSystem;
//     }
//
//     public void OnEnter()
//     {
//         player.HideWeapon();
//         
//         buildingSystem.SelectBuilding();
//         
//         input.Fire += buildingSystem.Place;
//         input.OnAlphaPressed += buildingSystem.SelectBuilding;
//         input.RotateLeft += buildingSystem.HandleRotationLeft;
//         input.RotateRight += buildingSystem.HandleRotationRight;
//     }
//
//     public void OnExit()
//     {
//         player.ShowWeapon();
//         
//         buildingSystem.HideBuilding();
//
//         input.Fire -= buildingSystem.Place;
//         input.OnAlphaPressed -= buildingSystem.SelectBuilding;
//         input.RotateLeft -= buildingSystem.HandleRotationLeft;
//         input.RotateRight -= buildingSystem.HandleRotationRight;
//     }
//     
//     public void Update() { }
//
//     public void FixedUpdate()
//     {
//         player.HandleMovement();
//     }
// }