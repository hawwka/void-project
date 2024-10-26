// using UnityEngine;
// using UnityEngine.Serialization;
//
// public sealed class Attackable : MonoBehaviour, IAttackable
// {
//     public Transform WeaponSocket;
//     
//     [FormerlySerializedAs("ShootingStrategy")]
//     public AttackStrategy AttackStrategy;
//     
//     [SerializeField] 
//     WeaponVisualEffect visualEffect;
//
//     
//     [SerializeField] 
//     public int MagazineCapacity;
//      
//     Timer reloadTimer;
//     public int ShotsInMagazine;
//
//     void Start()
//     {
//         reloadTimer = new Timer(WeaponConfig.ReloadTime);
//         
//         reloadTimer.TimerFinished += OnReloadFinished;
//     }
//
//     public void Update()
//     {
//         reloadTimer.Tick(Time.deltaTime);
//     }
//
//     public void Attack()
//     {
//         AttackStrategy.Execute();
//     }
//
//     public void Reload()
//     {
//         reloadTimer.Run();
//     }
//
//     void OnReloadFinished()
//     {
//         ShotsInMagazine = MagazineCapacity;
//     }
//
//     bool IsMagazineEmpty()
//     {
//         return ShotsInMagazine <= 0;
//     }
//
//     public bool IsAttackAllowed()
//     {
//         return !IsMagazineEmpty() && !reloadTimer.IsRunning;
//     }
// }