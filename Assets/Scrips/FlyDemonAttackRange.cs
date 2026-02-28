using UnityEngine;

public class FlyDemonAttackRange : MonoBehaviour
{
    private FlyEnemy flyenemy;
    void Start()
    {

        flyenemy = GetComponentInParent<FlyEnemy>();
    }

    // Update is called once per frame
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
                flyenemy.isPlayerInRange = true;

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
                flyenemy.isPlayerInRange = false;
            }
        }
    }
}
