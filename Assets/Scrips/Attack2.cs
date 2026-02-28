using UnityEngine;

public class Attack2 : MonoBehaviour
{
    [SerializeField] private float damage = 5f;

    private void OnTriggerStay2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        if (collision.CompareTag("Enemy"))
        {
            if(enemy != null)
            {
                
                enemy.TakeDamage(damage,transform.position);
                
            }
        }
    }
}
