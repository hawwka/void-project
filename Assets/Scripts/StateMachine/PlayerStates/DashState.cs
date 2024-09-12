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
        if (playerController.Movement == Vector3.zero)
            playerController.Rb.AddForce(playerController.Model.forward * playerController.DashForce, ForceMode.Impulse);
        else
            playerController.Rb.AddForce(playerController.Movement.normalized.ToIso() * playerController.DashForce, ForceMode.Impulse);
    }
}