using UnityEngine;

public class ArrowTrap : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private GameObject arrow;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        BlackKnight blackKnight = collision.GetComponent<BlackKnight>();
        if (collision.CompareTag("Player"))
        {
            if (blackKnight != null)
            {
                animator.SetBool("isActive",true);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        BlackKnight blackKnight = collision.GetComponent<BlackKnight>();
        if (collision.CompareTag("Player"))
        {
            if (blackKnight != null)
            {
                animator.SetBool("isActive", false);
            }
        }
    }

    public void ShotArrow()
    {
        GameObject bullet = Instantiate(arrow, attackPoint.position, Quaternion.identity);
        FireBall fireBallBullet = bullet.GetComponent<FireBall>();
    }

}
