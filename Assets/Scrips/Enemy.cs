using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public abstract class Enemy : MonoBehaviour
{
    public enum State
    {
        Patrol,
        Chase,
        Attack,
       
        Back,
        Die
    }

    public State currentState;
    [SerializeField] protected GameObject player;
    [SerializeField] protected float moveSpeed = 3f;
    [SerializeField] protected float range = 5f;
    [SerializeField] protected float chaseRange = 10f;

    protected Vector2 startPoint;
    protected float rightLimit;
    protected float leftLimit;
    [SerializeField]protected float stopDistanceToPlayer = 1f;
    protected bool movingRight = true;
    protected Animator animator;
    public bool isPlayerInTrigger;

    
    [SerializeField] protected float maxHp = 200f;
    protected float currentHp;
    [SerializeField] protected float knockBackForce = 5f;
    protected Rigidbody2D rb;
    protected bool isHurt = false;
    [SerializeField] protected Image hpBar;

    [SerializeField] protected GameObject coin;
    [SerializeField] protected Transform coinPoint;
    protected void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void Start()
    {
        startPoint = transform.position;
        rightLimit = startPoint.x + range;
        leftLimit = startPoint.x - range;
        currentHp = maxHp;
        updateHpBar();
    }

    protected void Update()
    {
        if(currentState == State.Die)
        {
            return;
        }
        switch(currentState)
        {
            case State.Patrol:
                PatrolState();
                break;
            case State.Chase:
                ChaseState(); 
                break;
            case State.Attack: 
                AttackState(); 
                break;
            case State.Back:
                BackState();
                break;
            
        }
    }

    protected virtual void Move(Vector2 direction)
    {
        transform.Translate(direction * moveSpeed * Time.deltaTime);

        if(direction == Vector2.right )
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

    }

    protected virtual void PatrolState()
    {
        animator.SetBool("isAttacking",false);
        
        if (movingRight)
        {
            Move(Vector2.right);
            if(transform.position.x > rightLimit)
            {
                movingRight = false;
            }

        }
        else
        {
            Move(Vector2.left);
            if(transform.position.x < leftLimit)
            {
                movingRight = true;
            }
        }

        if(isPlayerInTrigger == true)
        {
            currentState = State.Chase;
        }
    }

    protected virtual void ChaseState()
    {
        float distanceToPlayer = Vector2.Distance(player.transform.position, transform.position);
        float distanceToStartPoint = Vector2.Distance(startPoint, transform.position);

        if(distanceToStartPoint > chaseRange)
        {
            currentState = State.Back;
            return;
        }

        Vector2 vectorDistanceToPlayer = (player.transform.position.x > transform.position.x)? Vector2.right : Vector2.left;
        Move(vectorDistanceToPlayer);

        if(distanceToPlayer <= stopDistanceToPlayer)
        {
            currentState = State.Attack;

        }

        if(isPlayerInTrigger == false)
        {
            currentState = State.Patrol;
        }
    }

    protected virtual void FlipWhenAttack(Vector3 positionPlayer)
    {
        Vector3 flipToPlayer = (positionPlayer.x > transform.position.x) ? new Vector3(1, 1, 1) : new Vector3(-1, 1, 1);
        transform.localScale = flipToPlayer;
    }

    protected virtual void AttackState()
    {
        
        animator.SetBool("isAttacking",true);
        FlipWhenAttack(player.transform.position);
        float distanceToPlayer = Vector2.Distance(player.transform.position,transform.position);
        if(distanceToPlayer > stopDistanceToPlayer)
        {
            animator.SetBool("isAttacking",false);
            currentState = State.Chase;
        }
        
        
    }

    protected virtual void BackState()
    {

        float vectorDistanceToStartPoint = startPoint.x - transform.position.x;
        float distanceToStartPoint = Mathf.Abs(vectorDistanceToStartPoint);
        if(distanceToStartPoint > 0.2f)
        {
            Vector2 vectorToStartPoint = (startPoint.x > transform.position.x) ? Vector2.right : Vector2.left;
            Move(vectorToStartPoint);

        }

        else
        {
            currentState = State.Patrol;
            movingRight = (transform.position.x < rightLimit);
        }


    }

    

    


    public void TakeDamage(float damage, Vector2 positionPlayer)
    {
        currentHp -= damage;
        currentHp = Mathf.Max(currentHp, 0);
        updateHpBar();
        animator.SetTrigger("Hurt");  
        if (currentHp > 0)
        {


           

           
            Vector2 knockbackVector = ((Vector2)transform.position - positionPlayer).normalized;
            rb.linearVelocity = Vector2.zero;
            rb.AddForce(new Vector2(knockbackVector.x, 0.5f) * knockBackForce, ForceMode2D.Impulse); 
            animator.SetBool("isDie", false);
        }
        

        
        
            
        
        else
        {
            currentState = State.Die;
            
            animator.SetBool("isDie", true);
            
            moveSpeed = 0f;
            Die();
        }
    }
    protected void updateHpBar()
    {
        if(hpBar != null)
        {
            hpBar.fillAmount = currentHp/maxHp;
        }
    }

    protected void Die()
    {
        
        Destroy(gameObject,1.6f);
        Instantiate(coin, coinPoint.position, Quaternion.identity);

    }

    

}


