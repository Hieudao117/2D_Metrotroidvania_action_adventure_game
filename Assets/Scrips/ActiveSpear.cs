using UnityEngine;

public class ActiveSpear : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        BlackKnight blackKnight = collision.GetComponent<BlackKnight>();
        if (collision.CompareTag("Player"))
        {
            if(blackKnight != null)
            {
                blackKnight.Die();
            }
        }
    }
}
