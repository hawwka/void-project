public class AttackState : IState
{
    Player player;

    public AttackState(Player player)
    {
        this.player = player;
    }

    public void OnEnter(){ }

    public void OnExit(){ }

    public void Update()
    {
        player.selectedWeapon.Attack();
    }

    public void FixedUpdate()
    {
        
    }
}