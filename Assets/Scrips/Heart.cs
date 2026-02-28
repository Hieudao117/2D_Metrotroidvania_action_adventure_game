using UnityEngine;

public class Heart : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        BlackKnight blackKnight = collision.GetComponent<BlackKnight>();
        if (collision.CompareTag("Player"))
        {
            if (blackKnight != null)
            {
                blackKnight.RestoreHp(50f);
                Destroy(gameObject);
            }
        }
    }
}
