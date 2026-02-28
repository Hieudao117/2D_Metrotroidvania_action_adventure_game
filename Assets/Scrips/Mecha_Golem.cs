using System.Diagnostics;
using UnityEngine;

public class Mecha_Golem : MonoBehaviour
{
    
    [SerializeField] private float moveSpeed = 2f;
    public bool isPlayerInRange;
    [SerializeField] private GameObject player;
    [SerializeField] private float normalAttackDamage = 100f;
    [SerializeField] private LayerMask layerPlayer;
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private Transform normalAttackPoint;

    [SerializeField] private GameObject armBullet;
    [SerializeField] private Transform shotArmPoint;
    [SerializeField] private float armSpeed = 15f;

    [SerializeField] private GameObject lazeBullet;
    [SerializeField] private Transform shotLaze;

    [SerializeField] private float maxHp = 1000f;
    private float currentHp;
    private Animator animator;

    private float nextSkillTime;
    [SerializeField] private float cooldown = 2f;
    private bool isDeath = false;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        currentHp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDeath)
        {
            return;
        }

        if (isPlayerInRange)
        {
            Boss();
        }
        else
        {
            moveSpeed = 0f;
        }
    }

    private void Move(Vector2 direction)
    {
        transform.Translate(direction * moveSpeed * Time.deltaTime);
        if(direction == Vector2.right)
        {
            transform.localScale = new Vector3(1,1,1);
        }

        else
        {
            transform.localScale = new Vector3(-1,1,1);
        }
    }
    private void Boss()
    {

        moveSpeed = 2f;
        Vector2 vectorDistanceToPlayer = (player.transform.position.x > transform.position.x) ? Vector2.right : Vector2.left;
        animator.SetBool("isDie", false);
        Move(vectorDistanceToPlayer);
        if (Time.time >= nextSkillTime)
        {
            UseSkill();
        }


    }
    private void WhenPlayerNear()
    {

        int randomNear = Random.Range(0, 4);
        switch(randomNear)
        {
            case 0:
                animator.SetTrigger("NormalAttack");
                break;
            case 1:
                animator.SetTrigger("ShotArm");
                
                break;
            case 2:
                animator.SetTrigger("ShotLaze");
                
                break;
            case 3:
                animator.SetTrigger("Heal");
                
                break;
        }
    }
    private void WhenPlayerFar()
    {
        
        int randomFar = Random.Range(0, 3);
        switch (randomFar)
        {
            
            case 0:
                animator.SetTrigger("ShotArm");
                
                break;
            case 1:
                animator.SetTrigger("ShotLaze");
                
                break;
            case 2:
                animator.SetTrigger("Heal");
                
                break;
        }
    }


    
    public void NormalAttack()
    {
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(normalAttackPoint.position, attackRange,layerPlayer);   
        foreach(Collider2D player1 in hitPlayer )
        {
            BlackKnight blackKnight = player1.GetComponent<BlackKnight>();
            if( blackKnight != null)
            {
                blackKnight.TakeDamage(normalAttackDamage);
            }
        }
    }
    public void ShotArm()
    {
        if(player != null)
        {
            Vector3 directionToPlayer = player.transform.position - shotArmPoint.position;
            directionToPlayer.Normalize();
            GameObject bullet = Instantiate(armBullet, shotArmPoint.position, Quaternion.identity);
            ArmBullet arm = bullet.GetComponent<ArmBullet>();
            arm.SetMovementDirection(directionToPlayer * armSpeed);
            
        }
    }

    public void ShotLaze()
    {
        
        if (player != null && shotLaze != null)
        {
            
            Vector3 directionToPlayer = player.transform.position - shotLaze.position;
            float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle));

            
            GameObject laser = Instantiate(lazeBullet, shotLaze.position, rotation);

            
            laser.transform.SetParent(shotLaze);
        }

    }
    public void TakeDamae(float damage)
    {
        currentHp -= damage;
        currentHp = Mathf.Max(currentHp, 0);
        if(currentHp <= 0)
        {
            Die();
        }
    }
    public void Heal(float hpHeal)
    {
        currentHp += hpHeal;
        currentHp = Mathf.Min(currentHp,maxHp);
    }

    private void Die()
    {
        if (isDeath)
        {
            return;
        }
        isDeath = true;
        animator.SetBool("isDie",true);
        moveSpeed = 0f;
        Destroy(gameObject, 1f);
    }

    private void UseSkill()
    {
        nextSkillTime = Time.time + cooldown;
        float DistanceToPlayer = Vector2.Distance(player.transform.position, transform.position);
        if (DistanceToPlayer < 3f)
        {
            WhenPlayerNear();
        }

        else
        {
            WhenPlayerFar();
            
        }
    }
    



}
