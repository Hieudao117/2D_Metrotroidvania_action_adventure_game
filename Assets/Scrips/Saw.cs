using UnityEngine;

public class Saw : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float range = 6f;
    private float rightLimit;
    private float leftLimit;
    private Vector2 startPoint;
    private bool movingRight = true;

    void Start()
    {
        startPoint = transform.position;
        rightLimit = startPoint.x + range;
        leftLimit = startPoint.x - range;
    }

    
    void Update()
    {
        Move();
    }

    private void Move()
    {
        if(movingRight)
        {
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
            if(transform.position.x > rightLimit)
            {
                movingRight = false;
            }
        }
        else
        {
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
            if(transform.position.x < leftLimit)
            {
                movingRight = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        BlackKnight blackKnight = collision.GetComponent<BlackKnight>();
        if (collision.CompareTag("Player"))
        {
            if (blackKnight != null)
            {
                blackKnight.Die();
            }
        }
    }
}
