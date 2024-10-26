// using UnityEngine;
//
// [CreateAssetMenu]
// public class ShotgunAttackStrategy : AttackStrategy
// {
//     public WeaponConfig Config;
//     
//     float lastShotTime;
//
//
//     public override void Execute()
//     {
//         if (Time.time - lastShotTime < Config.DelayAfterShot)
//             return;
//         
//         lastShotTime = Time.time;
//         
//         var origin = Config.WeaponSocketPosition;
//         var dir = Config.WeaponSocketPosition;
//         var recoilStep = Config.MaxRecoil * 2 / Config.PelletsPerShot;
//         var recoilCurrentPos = -Config.MaxRecoil;
//         var dirVar = dir;
//         
//         for (int i = 0; i <= Config.PelletsPerShot; i++)
//         {   
//             var randomOffset = Random.insideUnitCircle * (recoilCurrentPos + recoilStep); 
//             dir = new Vector3(randomOffset.x, 0, randomOffset.y) + dirVar;
//             recoilCurrentPos += recoilStep;
//
//             if (!Physics.Raycast(origin, dir, out var hit, Config.Range))
//             {
//                 //visualEffect.ShowTracer(origin, dir, Config.Range, 200f);
//                 continue;
//             }
//
//             if (hit.collider.TryGetComponent<EnemyBase>(out var targetComponent))
//                 targetComponent.TakeDamage();
//             
//             if (hit.collider.TryGetComponent<Health>(out var targetHealthComponent))
//                 targetHealthComponent.TakeDamage((float)Config.Damage / Config.PelletsPerShot);
//
//             //visualEffect.ShowTracer(origin, dir, hit.distance, 200f);
//         }
//         
//         Debug.Log("Shotgun Attack! Pellets per shots was : " + Config.PelletsPerShot);
//     }
// }
