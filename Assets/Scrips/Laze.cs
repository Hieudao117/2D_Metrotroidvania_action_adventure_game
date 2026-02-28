using UnityEngine;

public class Laze : MonoBehaviour
{
    [SerializeField] private float duration = 2f; 
    [SerializeField] private GameObject controlLaze;

    void Start()
    {
        // Tự hủy sau khi thực hiện xong đòn đánh
        Destroy(gameObject, duration);
    }

    

    public void Enable()
    {
        controlLaze.SetActive(true);
    }

    public void Disable()
    {
        controlLaze.SetActive(false);
    }

}