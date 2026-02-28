using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;
    void Start()
    {
        
    }

    
    void Update()
    {
        Move();
    }
    public void Move()
    {
        transform.Translate(Vector2.down * moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        BlackKnight blackKnight = collision.GetComponent<BlackKnight>();
        if (collision.CompareTag("Player"))
        {
            if(blackKnight != null)
            {
                blackKnight.TakeDamage(50f);
                Destroy(gameObject);
            }
        }

        
    }
}
