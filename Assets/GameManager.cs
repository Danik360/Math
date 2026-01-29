using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    [Header("UI")]
    public TextMeshProUGUI scoreText;
    public GameObject gameOverPanel;
    
    private int score = 0;
    
    void Awake()
    {
        Instance = this;
    }
    
    void Start()
    {
        scoreText.text = "Score: 0";
        gameOverPanel.SetActive(false);
    }
    
    public void AddScore(int points)
    {
        score += points;
        scoreText.text = "Score: " + score;
    }
    
    public void GameOver()
    {
        Time.timeScale = 0;  // Пауза игры
        gameOverPanel.SetActive(true);
    }
    
    public void Restart()
    {
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
