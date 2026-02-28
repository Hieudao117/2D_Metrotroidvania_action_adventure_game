using UnityEngine;

public class FireWormRange : MonoBehaviour
{

    private FireWorm fireWorm;
    void Start()
    {

        fireWorm = GetComponentInParent<FireWorm>();
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
                fireWorm.isPlayerInRange = true;

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
                fireWorm.isPlayerInRange = false;
            }
        }
    }
}


