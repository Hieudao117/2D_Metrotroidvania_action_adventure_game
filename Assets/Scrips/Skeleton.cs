using UnityEngine;

public class Skeleton : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    private float range = 5f;
    private float rightLimit;
    private float leftLimit;
    private bool movingRight = true;
    [SerializeField] private float maxHp = 100f;
    private float currentHp;
    private Animator animator;
    private float stopDistanceToPlayer = 1f;
    [SerializeField] private GameObject player;
    public bool isAttack = false;
    public bool canHurt = true;
    private Rigidbody2D rb;
    [SerializeField] private float knockbackForce = 5f;
    private Vector2 startPoint;
    public bool isBack = false;
    ///[SerializeField] private GameObject skeletonAttack;
    [SerializeField] private Transform attackpoint;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private Vector2 boxSize = new Vector2(1,0.5f);
    [SerializeField] private float damage = 100f;
    

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        
    }
    void Start()
    {
        rightLimit = transform.position.x + range;
        leftLimit = transform.position.x - range;
        currentHp = maxHp;
        startPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToStart = Vector2.Distance(transform.position, startPoint);
        if(!isBack && distanceToStart >= 10f)
        {
            isBack = true;
            
        }
        if(isBack == true)
        {
            Back();
            
        }
        
       
        

        else if (isAttack == true)
        {
            Attack();
        }
        else  
        {
            Move();
        }
    }

    private void Move()
    {
        if (movingRight)
        {
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
            transform.localScale = new Vector3(1, 1, 1);
            if (transform.position.x >= rightLimit)
            {
                movingRight = false;

            }
        }
        else
        {
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
            transform.localScale = new Vector3(-1, 1, 1);
            if (transform.position.x <= leftLimit)
            {
                movingRight = true;
            }
        }
    }

    public void Hurt()
    {
        if (!canHurt)
        {
            return;
        }
        animator.SetTrigger("Hurt");
    }

    public void TakeDamage(float damage, Vector2 positionPlayer)
    {
        currentHp -= damage;
        currentHp = Mathf.Max(currentHp, 0);
        Vector2 knockbackVector = ((Vector2)transform.position - positionPlayer).normalized;
        rb.linearVelocity = Vector2.zero;
        rb.AddForce(new Vector2(knockbackVector.x, 0.5f) * knockbackForce, ForceMode2D.Impulse);
        
        if (currentHp > 0)
        {
            animator.SetBool("isDie", false);
            canHurt = true;
        }
        else
        {
            
            animator.SetBool("isDie",true);
            canHurt = false;
            moveSpeed = 0f;
            Die();
        }
    }
    public void Die()
    {
        Destroy(gameObject,1.7f);
        
    }

    public void Attack()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        if (distanceToPlayer > stopDistanceToPlayer)
        { 
            animator.SetBool("isAttacking", false);
            float vectorAttack = player.transform.position.x - transform.position.x;
            if (vectorAttack > 0)
            {
                transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if (vectorAttack < 0)
            {
                transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
                transform.localScale = new Vector3(-1, 1, 1);
            }


        }
        else if (distanceToPlayer <= stopDistanceToPlayer)
        {
            float vectorAttack = player.transform.position.x - transform.position.x;
            if (vectorAttack > 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if (vectorAttack < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            animator.SetBool("isAttacking", true);
        }
    }

    public void Back()
    {
        float distanceToStartPoint = Vector2.Distance(transform.position, startPoint);
        float vectorDistance = transform.position.x - startPoint.x;
        if(distanceToStartPoint > 0.2f)
        {
            if(vectorDistance > 0.001)
            {
                transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else if(vectorDistance <= -0.001)
            {
                transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
                transform.localScale = new Vector3(1, 1, 1);
            }
        }

        if(distanceToStartPoint <= 0.2f)
        {
            isBack = false;
            
            isAttack = false;
            animator.SetBool("isAttacking", false);
            
            

        }
    }

   
    public void PerformAttack()
    {
        Collider2D[] hitPlayer = Physics2D.OverlapBoxAll(attackpoint.position, boxSize, 0f, playerLayer);
        foreach(Collider2D player in hitPlayer)
        {
            BlackKnight blackKnight = player.GetComponent<BlackKnight>();
            if(blackKnight != null)
            {
                blackKnight.TakeDamage(damage);
                if(blackKnight.currentHp <= 0)
                {
                    animator.SetBool("isAttacking",false);
                }
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        if (attackpoint == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackpoint.position, boxSize);
    }





}
