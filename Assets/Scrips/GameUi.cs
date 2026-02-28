using UnityEngine;
using UnityEngine.SceneManagement;
public class GameUi : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void StartGame()
    {
        gameManager.StartGame();
    }

    public void CountinueGame()
    {
        gameManager.CountinueGame();
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


}
