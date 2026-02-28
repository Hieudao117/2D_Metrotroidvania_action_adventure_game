using UnityEngine;

public class Bringer : Enemy
{
    [SerializeField] private float damage = 100f;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private Vector2 boxSize = new Vector2(1f, 0.5f);
    [SerializeField] private LayerMask playerLayer;




    protected override void Start()
    {
        base.Start();
    }

    protected override void Move(Vector2 direction)
    {
        transform.Translate(direction * moveSpeed * Time.deltaTime);

        if (direction == Vector2.right)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    protected override void FlipWhenAttack(Vector3 positionPlayer)
    {
        Vector3 flipToPlayer = (positionPlayer.x > transform.position.x) ? new Vector3(-1, 1, 1) : new Vector3(1, 1, 1);
        transform.localScale = flipToPlayer;
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
