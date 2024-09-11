using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("References")]
    [SerializeField] PlayerInputReader input;
    
    [SerializeField] List<Weapon> weapons = new();
    
    [HideInInspector] public Weapon selectedWeapon;
    bool isFiring;
    
    StateMachine stateMachine;
    IState attackState;
    IState idleState;

    private void Start()
    {
        stateMachine = new StateMachine();
        attackState = new AttackState(this);
        idleState = new IdleState();
        
        At(idleState, attackState, new FuncPredicate(() => isFiring));
        At(attackState, idleState, new FuncPredicate(() => !isFiring));
        
        stateMachine.SetState(idleState);
        
        input.EnablePlayerActions();
        selectedWeapon = weapons[0];
    }
    
    void At(IState from, IState to, IPredicate condition) => stateMachine.AddTransition(from, to, condition);
    void Any(IState to, IPredicate condition) => stateMachine.AddAnyTransition(to, condition);

    private void Update()
    {
        stateMachine.Update();
    }
    
    private void OnEnable()
    {
        input.Fire += HandleFire;                  
        input.SelectPrimaryWeapon += SelectWeapon;
        input.SelectSecondaryWeapon += SelectWeapon;
    }

    private void OnDisable()
    {
        input.Fire -= HandleFire; 
        input.SelectPrimaryWeapon -= SelectWeapon;
        input.SelectSecondaryWeapon -= SelectWeapon;
    }
    
    private void SelectWeapon(int weaponIndex)
    {
        selectedWeapon.gameObject.GetComponentInChildren<Renderer>().enabled = false;
        selectedWeapon = weapons[weaponIndex];
        selectedWeapon.gameObject.GetComponentInChildren<Renderer>().enabled = true;
    }
    
    private void HandleFire(bool fireButtonPressed)
    {
        isFiring = fireButtonPressed;
    }
}