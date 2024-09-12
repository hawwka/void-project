using UnityEngine;

public class DashAttackState : IState
{
    PlayerController playerController;
    

    
    public DashAttackState(PlayerController playerController)
    {
        this.playerController = playerController;
    }

    public void OnEnter()
    {
        
    }

    public void OnExit()
    {
        
    }

    public void Update()
    {
        playerController.SelectedWeapon.Attack();
        
        playerController.HandleAiming();
    }

    public void FixedUpdate()
    {
        
    }
}