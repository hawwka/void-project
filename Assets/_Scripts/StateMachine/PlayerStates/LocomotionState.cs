public class LocomotionState : IState
{
    Player player;
    PlayerWeapon playerWeapon;
    PlayerInputReader input;


    public LocomotionState(Player player, PlayerWeapon playerWeapon, PlayerInputReader input)
    {
        this.player = player;
        this.playerWeapon = playerWeapon;
        this.input = input;
    }

    public void OnEnter()
    {
        input.OnAlphaPressed += playerWeapon.SelectWeapon;
        input.OnAlphaPressed += playerWeapon.SelectWeapon;
    }

    public void OnExit()
    {
        input.OnAlphaPressed -= playerWeapon.SelectWeapon;
        input.OnAlphaPressed -= playerWeapon.SelectWeapon;
    }

    public void Update()
    {
    }

    public void FixedUpdate()
    {
        player.HandleMovement();
    }
}