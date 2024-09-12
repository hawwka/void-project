using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] PlayerInputReader input;
    [SerializeField] List<Weapon> weapons = new();
    [SerializeField] Transform model;

    [Header("Settings")]
    [SerializeField] float movementSpeed = 8;
    [SerializeField] float aimSpeedPenaltyFactor = 0.4f;
    [SerializeField] float dashForce = 50;
    [SerializeField] float dashDuration = 0.25f;

    public Weapon SelectedWeapon { get; private set; }
    public Rigidbody Rb { get; private set; }
    public Vector3 Movement => movement;
    public Transform Model => model;
    public float DashForce => dashForce;

    bool isFiring; 
    bool isAiming;

    Vector3 movement;
   
    StateMachine stateMachine;
    
    Timer dashTimer;


    void Awake()
    {
        Rb = GetComponent<Rigidbody>();
        Rb.freezeRotation = true;
        
        SelectedWeapon = weapons[0];
    }

    void Start()
    {
        SetupStateMachine();

        dashTimer = new Timer(dashDuration);
        
        input.EnablePlayerActions(); // Мне кажется странным, включение инпута происходит в старте, и нигде не отключается.  
    }

    void SetupStateMachine()
    {
        stateMachine = new StateMachine();

        var attackState = new AttackState(this);
        var idleState = new IdleState(this);
        var dashState = new DashState(this);
        var dashAttackState = new DashAttackState(this);

        CreateTransition(idleState, attackState, new FuncPredicate(() => isFiring));
        CreateTransition(attackState, idleState, new FuncPredicate(() => !isFiring));
        CreateTransition(idleState, dashState, new FuncPredicate(() => dashTimer.IsRunning));
        CreateTransition(dashState, idleState, new FuncPredicate(() => !dashTimer.IsRunning));
        CreateTransition(attackState, dashState, new FuncPredicate(() => dashTimer.IsRunning));
        CreateTransition(dashState, dashAttackState, new FuncPredicate(() => isFiring));
        CreateTransition(dashAttackState, attackState, new FuncPredicate(() => !dashTimer.IsRunning));
        
        CreateAnyTransition(idleState, new FuncPredicate(() => !isFiring && !dashTimer.IsRunning));

        stateMachine.SetState(idleState);
    }
    
    void CreateTransition(IState from, IState to, IPredicate condition) => stateMachine.AddTransition(from, to, condition);

    void CreateAnyTransition(IState to, IPredicate condition) => stateMachine.AddAnyTransition(to, condition);

    void OnEnable()
    {
        input.Fire += HandleFire;                  
        input.SelectPrimaryWeapon += SelectWeapon;
        input.SelectSecondaryWeapon += SelectWeapon;
        input.Dash += OnDash;
        input.Aim += ProcessAimInput;
    }
    
    void OnDisable()
    {
        input.Fire -= HandleFire; 
        input.SelectPrimaryWeapon -= SelectWeapon;
        input.SelectSecondaryWeapon -= SelectWeapon;
        input.Dash -= OnDash;
        input.Aim -= ProcessAimInput;
    }

    void Update()
    { 
        stateMachine.Update();

        dashTimer.Tick(Time.deltaTime);
    }

    void FixedUpdate() => stateMachine.FixedUpdate();
    
    void OnDash()
    {
        if (dashTimer.IsRunning)
            return;
        
        dashTimer.Run();
    }

    public void HandleMovement()
    {
        movement = new Vector3(input.Direction.x, 0, input.Direction.y);

        if (movement.magnitude < 0.1f)
            return;
        
        var normalizedInput = movement.normalized.ToIso();
        var forwardTransform = model.forward;
    
        if (isAiming)
        {
            Rb.MovePosition(transform.position + normalizedInput * (movementSpeed * aimSpeedPenaltyFactor * Time.deltaTime));
            return;
        }
     
        Rb.MovePosition(transform.position + forwardTransform * (movementSpeed * Time.deltaTime));
    }

    public void HandleAiming()
    {
        if (isAiming)
        {
            var lookPos = Helpers.MouseToWorldPostion(input.MousePosition);
            
            lookPos.y = model.position.y;

            model.LookAt(lookPos);
            return;
        }
        
        if (movement.magnitude < 0.1f)
            return;
        
        model.rotation = Quaternion.LookRotation(movement.ToIso(), Vector3.up);
    }

    void ProcessAimInput(bool isRMousePressed)
    {
        isAiming = isRMousePressed;
    }

    void SelectWeapon(int weaponIndex)
    {
        SelectedWeapon.gameObject.GetComponentInChildren<Renderer>().enabled = false;
        SelectedWeapon = weapons[weaponIndex];
        SelectedWeapon.gameObject.GetComponentInChildren<Renderer>().enabled = true;
    }

    void HandleFire(bool fireButtonPressed)
    {
        isFiring = fireButtonPressed;
    }
}