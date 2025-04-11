using UnityEngine;
using UnityEngine.VFX;

public class Pistol : Weapon
{
    [SerializeField] private GameObject light_muzzle_flash;
    [SerializeField] private VisualEffect vfx_muzzle_flash;
    public override void Attack()
    {
        if (Time.time >= nextAttackTime)
        {
            Debug.Log("WeaponAttack");
            animator.SetTrigger("Attack");
            nextAttackTime = Time.time + attackRate;

            // light_muzzle_flash.SetActive(true);
            vfx_muzzle_flash.SendEvent("OnPlay");
        }
    }
}
