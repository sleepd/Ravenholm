using UnityEngine;
using UnityEngine.VFX;

public class Pistol : Weapon
{
    [SerializeField] private GameObject light_muzzle_flash;
    [SerializeField] private VisualEffect vfx_muzzle_flash;
    [SerializeField] private VisualEffect vfx_hit_effect;
    [SerializeField] private float maxDistance = 100f;
    [SerializeField] private LayerMask enemyLayer;
    private Camera mainCamera;

    protected override void Start()
    {
        base.Start();
        mainCamera = Camera.main;
    }
    public override void Attack()
    {
        if (Time.time >= nextAttackTime)
        {
            Debug.Log("WeaponAttack");
            animator.SetTrigger("Attack");
            nextAttackTime = Time.time + attackRate;
            
            // shoot
            RaycastHit hit;
            Ray ray = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            if (Physics.Raycast(ray, out hit, maxDistance, enemyLayer))
            {
                Debug.Log("Hit " + hit.point);
                vfx_hit_effect.SetVector3("Position", hit.point);
                vfx_hit_effect.SendEvent("OnPlay");
                Enemy enemy = hit.collider.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.TakeDamage(damage);
                }
            }
            
            // light_muzzle_flash.SetActive(true);
            vfx_muzzle_flash.SendEvent("OnPlay");
            
        }
    }
}