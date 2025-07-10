using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerWeapon playerWeapon;
    public PlayerInputReader input;
    public Transform model;
    
    public PlayerLocomotionAttributes LocomotionAttributes;
    
    private Quaternion targetRotation;
    
    
    public Rigidbody Rb { get; private set; }
    public Vector3 Movement => movement;
    public Transform Model => model;

    bool isFiring;
    bool isBuilding;
    
    Vector3 movement;
   
    StateMachine stateMachine;
    
    Timer dashTimer;


    void Awake()
    {
        Rb = GetComponent<Rigidbody>();
        Rb.freezeRotation = true;
    }

    void Start()
    {
        SetupStateMachine();

        dashTimer = new Timer(LocomotionAttributes.DashDuration);
        
        input.EnablePlayerActions();  
    }

    void SetupStateMachine()
    {
        stateMachine = new StateMachine();

        var locomotionState = new LocomotionState(this, playerWeapon, input);
        var attackState = new AttackState(this, playerWeapon, input);
        var dashState = new DashState(this);

        
        stateMachine.AddTransition(locomotionState, dashState, new FuncPredicate(() => dashTimer.IsRunning));
        stateMachine.AddTransition(locomotionState, attackState, new FuncPredicate(() => isFiring));
        stateMachine.AddTransition(attackState, locomotionState, new FuncPredicate(() => !isFiring));
        stateMachine.AddTransition(dashState, locomotionState, new FuncPredicate(() => !dashTimer.IsRunning));
        
        stateMachine.SetState(locomotionState);
    }
    
    void OnEnable()
    {
        input.Fire += HandleFire;                  
        input.Dash += OnDash;
        input.Reload += playerWeapon.Reload;
    }
    
    void OnDisable()
    {
        input.Fire -= HandleFire; 
        input.Dash -= OnDash;
        input.Reload -= playerWeapon.Reload;
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

        if (isFiring)
        {
            Rb.MovePosition(transform.position + normalizedInput * (LocomotionAttributes.MovementSpeed * playerWeapon.SpeedPenalty * Time.deltaTime));
            return;
        }
     
        Rb.MovePosition(transform.position + normalizedInput * (LocomotionAttributes.MovementSpeed * Time.deltaTime));
        
        model.rotation = Quaternion.RotateTowards(model.rotation, Quaternion.LookRotation(normalizedInput), LocomotionAttributes.RotationSpeed * Time.deltaTime);
    }

    public bool IsRotationStopped()
    {
        return model.rotation == targetRotation;
    }
    
    public void Aim()
    {
        var lookPos = Helpers.MouseToWorldPostion(input.MousePosition);
            
        lookPos.y = model.position.y;

        targetRotation = Quaternion.LookRotation(lookPos - transform.position);
        
        model.rotation = Quaternion.RotateTowards(model.rotation, targetRotation, LocomotionAttributes.RotationSpeed * Time.deltaTime);
    }
    
    void HandleFire(bool value)
    {
        isFiring = value;
    }
    
    void Die()
    {
        Destroy(gameObject);
    }
}