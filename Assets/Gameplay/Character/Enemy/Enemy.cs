using UnityEngine;  
using UnityEngine.AI;
using System.Collections;
public class Enemy : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] private int attackStrength;
    [SerializeField] private float searchRange;
    [SerializeField] private float searchAngle;
    [SerializeField] private float attackDistance; // the distance to start attack
    [SerializeField] private float attackRate;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform motionRoot;
    private NavMeshAgent navMeshAgent;
    private CharacterController characterController;
    private int currentHealth;
    private GameObject player;
    private bool alive = true;
    private bool playerInSight = false;
    private float attackTimer = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
        characterController = GetComponent<CharacterController>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        // navMeshAgent.updatePosition = false;
        navMeshAgent.updateRotation = true;
        player = GameObject.FindGameObjectWithTag("Player");
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!alive) return;
        if (player == null)
        {
            player = GameManager.Instance.GetPlayer();
        }
        //sync position with motion root
        // Vector3 move = animator.deltaPosition;
        // characterController.Move(move);

        if (IsPlayerInSight())
        {
            if (Vector3.Distance(transform.position, player.transform.position) > 2f)
            {
                Move();
            }
            else
            {
                Attack();
            }
        }
        else
        {
            Idle();
        }
    }



    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if(currentHealth <= 0)
        {
            Die();
        }
    }

    private void Move()
    {
        navMeshAgent.isStopped = false;
        navMeshAgent.SetDestination(player.transform.position);
        animator.SetBool("Moving", true);
        
        // Vector3 move = animator.deltaPosition;
        // characterController.Move(move);
        // if (navMeshAgent.velocity.magnitude > 0.1f)
        // {
            
        // }
        
    }
    private IEnumerator MoveCoroutine(float t)
    {
        yield return new WaitForSeconds(t);
        // callback for end attack
        if (alive)
        {
            navMeshAgent.isStopped = false;
        }
    }

    void OnAnimatorMove()
    {
        // Vector3 move = animator.deltaPosition;
        // characterController.Move(move);

        
    }

    private bool IsPlayerInSight()
    {
        Vector3 directionToPlayer = (player.transform.position - transform.position);
        Debug.Log($"Direction to player: {directionToPlayer}");
        float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);
        Debug.Log($"Angle to player: {angleToPlayer}");
        Debug.Log($"distance to player: {directionToPlayer.magnitude}");

        // check if player is in search angle
        if (angleToPlayer < searchAngle / 2f)
        {
            // check if player is in search range
            if (directionToPlayer.magnitude < searchRange)
            {
                Debug.Log($"Player in sight, distance: {directionToPlayer.magnitude}, angle: {angleToPlayer}");
                // move to player
                return true;
            }
        }
        return false;
    }

    private void Attack()
    {
        
        if (attackTimer <= 0f)
        {
            navMeshAgent.isStopped = true;
            animator.SetBool("Moving", false);
            animator.SetTrigger("Attack");
            attackTimer = attackRate;
            StartCoroutine(MoveCoroutine(1.867f));
        }
        else
        {
            attackTimer -= Time.deltaTime;
        }
    }

    private void Die()
    {
        alive = false;
        animator.SetBool("Dead", true);
        navMeshAgent.isStopped = true;
        characterController.enabled = false;
    }

    private void Idle()
    {
        navMeshAgent.isStopped = true;
        animator.SetBool("Moving", false);
    }
}
