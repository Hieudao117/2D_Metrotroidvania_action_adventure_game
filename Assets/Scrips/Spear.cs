using UnityEngine;

public class Spear : MonoBehaviour
{
    [SerializeField] private GameObject activeSpear;
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
    public void EnableSpear()
    {
        activeSpear.SetActive(true);
    }
    public void DisableSpear()
    {
        activeSpear.SetActive(false);
    }
}
