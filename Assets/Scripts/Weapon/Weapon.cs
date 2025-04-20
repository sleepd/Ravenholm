using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] protected Animator animator;
    [SerializeField] protected float attackRate;
    
    protected float nextAttackTime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void Attack()
    {

    }
}
