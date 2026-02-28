using Unity.VisualScripting;
using UnityEngine;

public class FireWorm : MonoBehaviour
{
    
    public bool isPlayerInRange;
    [SerializeField] private GameObject player;
    private Animator animator;
    [SerializeField] private float maxHp = 200f;
    private float currentHp;

    [SerializeField] private GameObject fireBallWorm;
    [SerializeField] private Transform shotPoint;
    [SerializeField] private float speedFireBall = 3f;
    [SerializeField] private float cooldown = 2f;
    private float lastShootTime;
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
        lastShootTime = -cooldown;
        Control();

    }

    private void Flip(Vector3 positionPlayer)
    {
        Vector3 filpToPlayer = (positionPlayer.x > transform.position.x)? new Vector3(1,1,1) : new Vector3(-1,1,1);
        transform.localScale = filpToPlayer;
    }
    private void Control()
    {
        
        if(isPlayerInRange)
        {
            animator.SetBool("isAttacking", true);
            Flip(player.transform.position);
        }
        else
        {
            animator.SetBool("isAttacking",false);
        }
    }

    public void ShotFireBall()
    {
        if (player != null)
        {
            if (Time.time > lastShootTime + cooldown)
            {
                Vector3 directionToPlayer = player.transform.position - shotPoint.position;
                directionToPlayer.Normalize();
                GameObject bullet = Instantiate(fireBallWorm, shotPoint.position, Quaternion.identity);
                FireBallWorm fireballWorm = bullet.GetComponent<FireBallWorm>();
                
                fireballWorm.SetMovementDirection(directionToPlayer * speedFireBall);
                lastShootTime = Time.time;
            }

        }
    }

    public void TakeDamage(float damage)
    {
        currentHp -= damage;
        currentHp = Mathf.Max(currentHp, 0);
        animator.SetTrigger("Hurt");
        if(currentHp <= 0)
        {
            animator.SetBool("isDie",true);
            Destroy(gameObject, 1.5f);
        }
        else
        {
            animator.SetBool("isDie", false);
        }
    }


}
