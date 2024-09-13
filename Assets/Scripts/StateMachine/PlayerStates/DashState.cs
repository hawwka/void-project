using UnityEngine;

public class DashState : IState
{
    PlayerController playerController;
    

    public DashState(PlayerController playerController)
    {
        this.playerController = playerController;
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
        if (playerController.Movement.magnitude < 0.1f)
        {
            playerController.Rb.AddForce(playerController.Model.forward * playerController.DashForce, ForceMode.Impulse);
        }
        else
        {
            playerController.Model.rotation = Quaternion.LookRotation(playerController.Movement.ToIso(), Vector3.up);
            playerController.Rb.AddForce(playerController.Movement.normalized.ToIso() * playerController.DashForce, ForceMode.Impulse);
        }
    }
}