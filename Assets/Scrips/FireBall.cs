using UnityEngine;

public class FireBall : MonoBehaviour
{
    private Vector3 moveDirection;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(moveDirection == Vector3.zero)
        {
            return;
        }
        transform.position += moveDirection * Time.deltaTime;
    }

    public void SetMovementDirection(Vector3 direction)
    {
        moveDirection = direction;
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
