using UnityEngine;

public class GoblinAttackRange : MonoBehaviour
{
    private Enemy enemy;
    void Start()
    {

        enemy = GetComponentInParent<Enemy>();
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
                enemy.isPlayerInTrigger = true;

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
                enemy.isPlayerInTrigger = false;
            }
        }
    }
}
