public class AttackState : IState
{
    Player player;
    PlayerWeapon playerWeapon;
    PlayerInputReader input;

    public AttackState(Player player, PlayerWeapon playerWeapon, PlayerInputReader input)
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
        player.Aim();

        if (player.IsRotationStopped())
            playerWeapon.Attack();
    }

    public void FixedUpdate()
    {
        player.HandleMovement();
    }
}