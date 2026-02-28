using UnityEngine;

public class Mecha_Golem_Range : MonoBehaviour
{
    private Mecha_Golem mecha_Golem;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mecha_Golem = GetComponentInParent<Mecha_Golem>();
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
                mecha_Golem.isPlayerInRange = true;

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
                mecha_Golem.isPlayerInRange = false;
            }
        }
    }
}
