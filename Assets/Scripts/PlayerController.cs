using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] 
    PlayerInputReader input;
    [SerializeField] 
    List<Weapon> weapons = new();
    [SerializeField] 
    Transform model;
    [SerializeField]
    BuildingSystem buildingSystem;
    
    [Header("Settings"), SerializeField] 
    float movementSpeed = 8;
    [SerializeField] 
    float firePenalty = 0.4f;
    [SerializeField] 
    float dashForce = 50;
    [SerializeField] 
    float dashDuration = 0.25f;
    [SerializeField]  
    float rotationSpeed = 1600f;
    
    private Quaternion targetRotation;
    
    public Weapon SelectedWeapon { get; private set; }
    public Rigidbody Rb { get; private set; }
    public Vector3 Movement => movement;
    public Transform Model => model;
    public float DashForce => dashForce;

    bool isFiring;
    bool isBuilding;
    
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
        
        input.EnablePlayerActions();  
    }

    void SetupStateMachine()
    {
        stateMachine = new StateMachine();

        var locomotionState = new LocomotionState(this, input);
        var attackState = new AttackState(this, input);
        var dashState = new DashState(this);
        var buildingState = new BuildingState(this, input, buildingSystem);

        
        stateMachine.AddTransition(locomotionState, dashState, new FuncPredicate(() => dashTimer.IsRunning));
        stateMachine.AddTransition(locomotionState, attackState, new FuncPredicate(() => isFiring));
        stateMachine.AddTransition(attackState, locomotionState, new FuncPredicate(() => !isFiring));
        stateMachine.AddTransition(buildingState, locomotionState, new FuncPredicate(() => !isBuilding));
        stateMachine.AddTransition(dashState, locomotionState, new FuncPredicate(() => !dashTimer.IsRunning));
        
        stateMachine.AddAnyTransition(buildingState, new FuncPredicate(() => isBuilding));
        
        stateMachine.SetState(locomotionState);
    }
    
    void OnEnable()
    {
        input.Fire += HandleFire;                  
        input.Dash += OnDash;
        input.Building += OnBuiding;
        input.Reload += OnReload;
    }
    
    void OnDisable()
    {
        input.Fire -= HandleFire; 
        input.Dash -= OnDash;
        input.Building -= OnBuiding;
        input.Reload -= OnReload;
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

    void OnBuiding()
    {
        isBuilding = !isBuilding;
    }

    public void OnReload()
    {
        SelectedWeapon.Reload();
    }

    public void HandleMovement()
    {
        movement = new Vector3(input.Direction.x, 0, input.Direction.y);

        if (movement.magnitude < 0.1f)
            return;
        
        var normalizedInput = movement.normalized.ToIso();

        if (isFiring)
        {
            Rb.MovePosition(transform.position + normalizedInput * (movementSpeed * firePenalty * Time.deltaTime));
            return;
        }
     
        Rb.MovePosition(transform.position + normalizedInput * (movementSpeed * Time.deltaTime));
        
        model.rotation = Quaternion.RotateTowards(model.rotation, Quaternion.LookRotation(normalizedInput), rotationSpeed * Time.deltaTime);
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
        
        model.rotation = Quaternion.RotateTowards(model.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
    
    public void SelectWeapon(int weaponIndex)
    {
        SelectedWeapon.gameObject.GetComponentInChildren<Renderer>().enabled = false;
        SelectedWeapon = weapons[weaponIndex];
        SelectedWeapon.gameObject.GetComponentInChildren<Renderer>().enabled = true;
    }
    
    public void HideWeapon()
    {
        SelectedWeapon.gameObject.GetComponentInChildren<Renderer>().enabled = false;
    }
    
    public void ShowWeapon()
    {
        SelectedWeapon.gameObject.GetComponentInChildren<Renderer>().enabled = true;
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