using UnityEngine;

public class DashState : IState
{
    Player player;
    

    public DashState(Player player)
    {
        this.player = player;
    }

    public void OnEnter()
    {
        Dash();
    }

    public void OnExit()
    {
        
    }

    public void Update()
    {
        
    }

    public void FixedUpdate()
    {
        
    }
    
    void Dash()
    {
        if (player.Movement.magnitude < 0.1f)
        {
            player.Rb.AddForce(player.Model.forward * player.LocomotionAttributes.DashForce, ForceMode.Impulse);
        }
        else
        {
            player.Model.rotation = Quaternion.LookRotation(player.Movement.ToIso(), Vector3.up);
            player.Rb.AddForce(player.Movement.normalized.ToIso() * player.LocomotionAttributes.DashForce, ForceMode.Impulse);
        }
    }
}