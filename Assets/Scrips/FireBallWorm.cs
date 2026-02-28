using UnityEngine;

public class FireBallWorm : MonoBehaviour
{
    private Vector3 moveDirection;
    private Animator animator;
    [SerializeField] private GameObject player;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    void Start()
    {
       
        
            
    }

    // Update is called once per frame
    void Update()
    {
        if (moveDirection == Vector3.zero)
        {
            return;
        }
        transform.position += moveDirection * Time.deltaTime;
    }

    public void SetMovementDirection(Vector3 direction)
    {
        moveDirection = direction;

        
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        if (player != null && direction != Vector3.zero)
        {
            
            Vector3 targetDir = player.transform.position - transform.position;

            
            float angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg;

            
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        BlackKnight blackKnight = collision.GetComponent<BlackKnight>();
        if (collision.CompareTag("Player"))
        {
            if (blackKnight != null)
            {
                blackKnight.TakeDamage(50f);
                animator.SetTrigger("Explosion");
                Destroy(gameObject,1f);
            }
        }
    }
}
