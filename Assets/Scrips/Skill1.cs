using UnityEngine;

public class Skill1 : MonoBehaviour
{
    [SerializeField] private float damaeSkill1 = 50f;
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        FlyEnemy flyEnemy = collision.GetComponent<FlyEnemy>();
        Mecha_Golem mecha_Golem = collision.GetComponent<Mecha_Golem>();
        FireWorm fireWorm = collision.GetComponent<FireWorm>();
        if (collision.CompareTag("Enemy"))
        {
            if(enemy != null)
            {
                enemy.TakeDamage(damaeSkill1,transform.position);
            }
            if(flyEnemy != null)
            {
                flyEnemy.TakeDamage(damaeSkill1 ,transform.position);
            }
            if(mecha_Golem != null)
            {
                mecha_Golem.TakeDamae(damaeSkill1 );
            }
            if(fireWorm != null)
            {
                fireWorm.TakeDamage(damaeSkill1 );
            }
        }
    }
}
