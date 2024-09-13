using UnityEngine;


public class IdleState : IState
{
    PlayerController playerController;
    public IdleState(PlayerController playerController)
    {
        this.playerController = playerController;
    }
    
    public void OnEnter()
    {
        
    }

    public void Update()
    {
        
    }

    public void FixedUpdate()
    {
        playerController.HandleMovement();
    }

    public void OnExit()
    {
        
    }
}