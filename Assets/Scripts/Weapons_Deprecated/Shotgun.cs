using System;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class Shotgun : MonoBehaviour
{
    // private float lastAttackedTime;
    //
    // [SerializeField]
    // private int pelletsPerShot = 10;
    //
    // public void Attack()
    // {
    //     if (!IsAttackAllowed())
    //         return;
    //     if (Time.time - lastAttackedTime < WeaponConfig.DelayAfterShot)
    //         return;
    //
    //     
    //     lastAttackedTime = Time.time;
    //     
    //     var origin = WeaponSocket.position;
    //     var dir = WeaponSocket.TransformDirection(Vector3.forward);
    //     var recoilStep = WeaponConfig.MaxRecoil * 2 / pelletsPerShot;
    //     var recoilCurrentPos = -WeaponConfig.MaxRecoil;
    //     var dirVar = dir;
    //     
    //     for (int i = 0; i <= pelletsPerShot; i++)
    //     {   
    //         var randomOffset = Random.insideUnitCircle * (recoilCurrentPos + recoilStep); 
    //         dir = new Vector3(randomOffset.x, 0, randomOffset.y) + dirVar;
    //         recoilCurrentPos += recoilStep;
    //
    //         if (!Physics.Raycast(origin, dir, out var hit, WeaponConfig.Range))
    //         {
    //             visualEffect.ShowTracer(origin, dir, WeaponConfig.Range, 200f);
    //             continue;
    //         }
    //
    //         if (hit.collider.TryGetComponent<EnemyBase>(out var targetComponent))
    //             targetComponent.TakeDamage();
    //         if (hit.collider.TryGetComponent<Health>(out var targetHealthComponent))
    //             targetHealthComponent.TakeDamage(WeaponConfig.Damage / pelletsPerShot);
    //
    //         visualEffect.ShowTracer(origin, dir, hit.distance, 200f);
    //     }
    // }
}


