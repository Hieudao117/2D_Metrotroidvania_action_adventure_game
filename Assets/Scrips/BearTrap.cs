using UnityEngine;

public class BearTrap : MonoBehaviour
{
    [SerializeField] private float damage = 50f;
    private Animator animator;

   
    private BlackKnight targetPlayer;
    private float originalSpeed;
    private float originalJumpForce;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            BlackKnight blackKnight = collision.GetComponent<BlackKnight>();
            if (blackKnight != null) 
            {
                targetPlayer = blackKnight;

                
                animator.SetBool("isActive", true);

                
                targetPlayer.TakeDamage(damage);

                
                originalSpeed = targetPlayer.moveSpeed;
                originalJumpForce = targetPlayer.jumpForce;
                targetPlayer.moveSpeed = 0f;
                targetPlayer.jumpForce = 0f;
            }
        }
    }

    
    public void EndActive()
    {
        animator.SetBool("isActive", false);

        if (targetPlayer != null)
        {
            
            targetPlayer.moveSpeed = originalSpeed;
            targetPlayer.jumpForce = originalJumpForce;

            
            targetPlayer = null;
        }
    }
}
