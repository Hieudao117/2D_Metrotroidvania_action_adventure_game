using UnityEngine;
using TMPro;
public class GameManager : MonoBehaviour
{
    public int coinNumber = 0;
    [SerializeField] private TextMeshProUGUI scoreText;
    private BlackKnight blackKnight;
    [SerializeField] private GameObject gameMenu;
    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private GameObject gamePauseMenu;

    private void Awake()
    {
        blackKnight = FindAnyObjectByType<BlackKnight>();
    }
    
    void Start()
    {
        GameMenu();
        UpdateScoreText();
    }

    
    void Update()
    {
        
    }
    public void AddCoinNumber(int value)
    {
        coinNumber += value;
        UpdateScoreText();
    }
    private void UpdateScoreText()
    {
        scoreText.text = coinNumber.ToString();
    }

    public void UpgradeWeapon()
    {
        
        if(coinNumber >= 10)
        {
            blackKnight.damage += 50f;
            coinNumber -= 10;
            UpdateScoreText() ;
        }
    }

    public void GameMenu()
    {
        gameMenu.SetActive(true);
        gameOverMenu.SetActive(false);
        gamePauseMenu.SetActive(false);
        Time.timeScale = 0f;
    }

    public void GameOverMenu()
    {
        gameOverMenu.SetActive(true);
        gamePauseMenu.SetActive(false);
        gameMenu.SetActive(false);
        Time .timeScale = 0f;
    }

    public void GamePauseMenu()
    {
        gamePauseMenu.SetActive(true);
        gameMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        Time.timeScale = 0f;
    }

    public void StartGame()
    {
        gameMenu.SetActive(false );
        gameOverMenu.SetActive(false);
        gamePauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void CountinueGame()
    {
        gameMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        gamePauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }
}
