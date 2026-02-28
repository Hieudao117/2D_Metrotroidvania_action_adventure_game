using UnityEngine;

public class Shadow_Boss : MonoBehaviour
{
    
    public enum State
    {
        Idle,
        Move,
        Attack1,
        Attack2,
        Attack3,
    }
    public State currentState;
    [SerializeField] private GameObject player;
    [SerializeField] private float moveSpeed = 3f;
    public bool isPlayerInTrigger= false;
    private Animator animator;


    [SerializeField] private LayerMask wallLayer; // Gán layer "Wall" vào đây trong Inspector
    [SerializeField] private float attack3Speed = 10f;
    private int wallHitCount = 0;
    private Vector2 attackDirection;
    private bool isDoingAttack3 = false;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }


    void Start()
    {
        
    }

    
    void Update()
    {
        switch (currentState)
        {
            case State.Idle: IdleState(); break;
            case State.Move: MoveState(); break;
            case State.Attack1: Attack1State(); break;
            case State.Attack3: Attack3State(); break;
        }
    }

    private void IdleState()
    {
        moveSpeed = 0f;
        if(isPlayerInTrigger)
        {
            currentState = State.Move;
            moveSpeed = 3f;
        }
    }

    private void Move(Vector2 direction)
    {
        transform.Translate(direction * moveSpeed * Time.deltaTime);
        if( direction == Vector2.right )
        {
            transform.localScale = new Vector3(1, 1, 1);    

        }
        if( direction == Vector2.left)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    
    private void MoveState()
    {
        Vector2 vectorDistanceToPlayer = (player.transform.position.x > transform.position.x) ? Vector2.right : Vector2.left;

        animator.SetTrigger("Walk");
        Move(vectorDistanceToPlayer);
        float distanceToPlayer = Vector2.Distance(player.transform.position, transform.position);
        if(distanceToPlayer >= 10f)
        {
            currentState = State.Attack3;
        }
        

        if(distanceToPlayer <= 2f )
        {
            currentState = State.Attack1;
        }
        
        
    }

    private void Attack1State()
    {
        animator.SetTrigger("Attack1");
        float  distanceToPlayer = Vector2.Distance(player.transform.position, transform.position);  
        if(distanceToPlayer > 2f)
        {
            currentState = State.Move;
        }
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Kiểm tra nếu vật thể va chạm thuộc wallLayer
        if (((1 << collision.gameObject.layer) & wallLayer) != 0 && currentState == State.Attack3)
        {
            wallHitCount++;

            // Đổi hướng (Lật hướng)
            attackDirection = (attackDirection == Vector2.right) ? Vector2.left : Vector2.right;

            if (wallHitCount >= 2)
            {
                FinishAttack3();
            }
        }
    }
    private void Attack3State()
    {
        // Nếu đây là frame đầu tiên bước vào trạng thái Attack3
        if (!isDoingAttack3)
        {
            isDoingAttack3 = true;
            wallHitCount = 0;
            animator.SetTrigger("Attack3");

            // Xác định hướng ban đầu lao về phía Player
            attackDirection = (player.transform.position.x > transform.position.x) ? Vector2.right : Vector2.left;
        }

        // Luôn di chuyển theo hướng hiện tại
        MoveInAttack3(attackDirection);
    }

    private void MoveInAttack3(Vector2 direction)
    {
        transform.Translate(direction * attack3Speed * Time.deltaTime);

        // Lật nhân vật theo hướng lao đi
        if (direction == Vector2.right)
            transform.localScale = new Vector3(1, 1, 1);
        else
            transform.localScale = new Vector3(-1, 1, 1);
    }

    private void FinishAttack3()
    {
        isDoingAttack3 = false;
        wallHitCount = 0;
        currentState = State.Move; // Kết thúc thì quay lại trạng thái di chuyển
    }


}
