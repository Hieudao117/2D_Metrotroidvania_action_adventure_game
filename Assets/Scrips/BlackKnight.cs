using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BlackKnight : MonoBehaviour
{
    [SerializeField] public float moveSpeed = 3f;
    [SerializeField] public float jumpForce = 10f;
    [SerializeField] private LayerMask groundlayer;
    [SerializeField] private Transform groundCheck;
    private bool isGrounded;
    private Rigidbody2D rb;
    private Animator animator;
    ///[SerializeField] private GameObject attack1;
    [SerializeField] private GameObject attack2;
    [SerializeField] private GameObject skill1;
    [SerializeField] private float maxHp = 200f;
    public float currentHp;

    [SerializeField] private Transform attackPoint;
    [SerializeField]private float attackRange = 1f;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] public float damage = 100f;
    /*[SerializeField] private Transform attack1Point;
    [SerializeField] private Vector2 boxSize = new Vector2(1f, 0.5f);
    [SerializeField] private float damage1 = 50f;*/
    private Skeleton skeleton;

    [Header("Roll Settings")]
    [SerializeField] private float rollDistance = 5f; 
    [SerializeField] private float rollDuration = 0.5f; 
    private bool isRolling = false;

    [SerializeField] private Image hpBar;

    private GameManager gameManager;

    

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        gameManager = FindAnyObjectByType<GameManager>();
        
    }
    void Start()
    {
        currentHp = maxHp;
        skeleton = GetComponent<Skeleton>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            gameManager.GamePauseMenu();
        }
        if (isRolling) return;
        Move();
        Jump();
        UpdateAnimation();
        if (Input.GetMouseButtonDown(0))
        {
            Attack0();
            
        }
        else if(Input.GetMouseButtonDown(1))
        {
           Attack1();
        }
        else if(Input.GetKeyDown(KeyCode.Q))
        {
            Skill1();
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            Roll();
        }
    }

    private void Roll()
    {
        
        StartCoroutine(RollRoutine());
    }

    private IEnumerator RollRoutine()
    {
        isRolling = true;
        animator.SetBool("isRoll", true);

        
        float rollDir = transform.localScale.x;

        
        float rollVelocity = rollDistance / rollDuration;

        float timer = 0f;
        while (timer < rollDuration)
        {
            
            rb.linearVelocity = new Vector2(rollDir * rollVelocity, rb.linearVelocity.y);

            timer += Time.deltaTime;
            yield return null; 
        }

        
        rb.linearVelocity = new Vector2(0, rb.linearVelocity.y); 
        animator.SetBool("isRoll", false);
        isRolling = false;
    }

    private void Move()
    {
        if (isRolling) return;
        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(moveInput*moveSpeed , rb.linearVelocity.y);
        if (moveInput > 0)
        {
            transform.localScale = new Vector3(1,1,1);
        }
        else if(moveInput < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        } 
        isGrounded = Physics2D.OverlapCircle(groundCheck.position,0.3f,groundlayer);
    }
    
    private void UpdateAnimation()
    {
        bool isRunning = Mathf.Abs(rb.linearVelocity.x) > 0.1f;
        bool isJumping = !isGrounded;
        animator.SetBool("isRunning", isRunning);
        animator.SetBool("isJumping", isJumping);


    }

    public void AttackPerformance()
    {
        Collider2D[] hitEnemy = Physics2D.OverlapCircleAll(attackPoint.position,attackRange,enemyLayer);
        foreach (Collider2D enemy in hitEnemy)
        {
            Enemy enemy1 = enemy.GetComponent<Enemy>();
            FlyEnemy flyEnemy = enemy.GetComponent<FlyEnemy>();
            Mecha_Golem mecha_Golem = enemy.GetComponent<Mecha_Golem>();
            FireWorm fireWorm = enemy.GetComponent<FireWorm>();
            if (enemy1 != null)
            {
                enemy1.TakeDamage(damage, transform.position);
            }
            if(flyEnemy != null)
            {
                flyEnemy.TakeDamage(damage, transform.position);
            }
            if(mecha_Golem != null)
            {
                mecha_Golem.TakeDamae(damage);
            }
            if (fireWorm != null)
            {
                fireWorm.TakeDamage(damage);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {
            gameManager.AddCoinNumber(1);
        }
    }


    private void Attack0()
    {
        animator.SetBool("isAttacking0",true);
        animator.SetBool("isSkill1",false);
        animator.SetBool("isAttacking1", false);
    }

    public void EndAttack0()
    {
        animator.SetBool("isAttacking0",false);
        moveSpeed = 5f;
    }

    
   
    
    private void Attack1()
    {
        animator.SetBool("isAttacking0",false );
        animator.SetBool("isAttacking1",true) ;
        animator.SetBool("isSkill1", false);
        moveSpeed = 0.2f;
    }

   
    public void EndAttack1()
    {
        animator.SetBool("isAttacking1", false);

        moveSpeed = 5f;
    }

    private void Skill1()
    {
        animator.SetBool("isSkill1",true );
        animator.SetBool("isAttacking0", false);
        animator.SetBool("isAttacking1", false);
        moveSpeed = 1f;
    }
    public void EndSkill1()
    {
        animator.SetBool("isSkill1", false);
        moveSpeed = 5f;
    }
   
    public void EnableAttack2()
    {
        attack2.SetActive(true);
    }
    public void DisableAttack2()
    {
        attack2.SetActive(false);
    }
    public void EnableSkill1()
    {
        skill1.SetActive(true);
    }

    public void DisableSkill1()
    {
        skill1.SetActive(false);
    }

    public void TakeDamage(float damage)
    {
        currentHp -= damage;
        currentHp = Mathf.Max(currentHp, 0);
        UpdateHpBar();
        if (currentHp <= 0)
        {
            Die();
        }
    }

    

    public void RestoreHp(float valueHp)
    {
        currentHp += valueHp;
        currentHp = Mathf.Max(currentHp,maxHp);
        UpdateHpBar();
    }

    private void UpdateHpBar()
    {
        if (hpBar != null)
        {
            hpBar.fillAmount = currentHp / maxHp;
        }
    }
    public void Die()
    {

        gameManager.GameOverMenu();
        
    }




}
