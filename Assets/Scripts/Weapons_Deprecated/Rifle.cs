// using UnityEngine;
// using Random = UnityEngine.Random;
//
// public class Rifle : Weapon
// {
//     private float lastAttackedTime;
//
//     public override void Attack()
//     {
//         if (!IsAttackAllowed())
//             return;
//         if (Time.time - lastAttackedTime < WeaponConfig.DelayAfterShot)
//             return;
//     
//         ShotsInMagazine--;
//         lastAttackedTime = Time.time;
//      
//         var origin = WeaponSocket.position;
//         var dir = WeaponSocket.TransformDirection(Vector3.forward);
//         var randomOffset = Random.insideUnitCircle * WeaponConfig.MaxRecoil; 
//         
//         dir += new Vector3(randomOffset.x, 0, randomOffset.y);
//         
//         if (!Physics.Raycast(origin, dir, out var hit, WeaponConfig.Range))
//         {
//             visualEffect.ShowTracer(origin, dir, WeaponConfig.Range, 200f);
//             return;
//         }
//
//         if (hit.collider.TryGetComponent<EnemyBase>(out var targetComponent))
//             targetComponent.TakeDamage();
//         if (hit.collider.TryGetComponent<Health>(out var targetHealthComponent))
//             targetHealthComponent.TakeDamage(WeaponConfig.Damage);
//         
//         visualEffect.ShowTracer(origin, dir, hit.distance, 200f);
//     }
// }
