using UnityEngine;

public class SlayerDemon : Enemy
{
    [SerializeField] private float damage = 100f;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private Vector2 boxSize = new Vector2(1f, 0.5f);
    [SerializeField] private LayerMask playerLayer;




    protected override void Start()
    {
        base.Start();
    }

    public void performAttack()
    {
        Collider2D[] hitPlayer = Physics2D.OverlapBoxAll(attackPoint.position, boxSize, 0f, playerLayer);
        foreach (Collider2D player in hitPlayer)
        {
            BlackKnight blackKnight = player.GetComponent<BlackKnight>();
            if (blackKnight != null)
            {
                blackKnight.TakeDamage(damage);
                if (blackKnight.currentHp <= 0)
                {
                    animator.SetBool("isAttacking", false);
                }
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackPoint.position, boxSize);
    }
}
