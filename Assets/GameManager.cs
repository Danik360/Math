using UnityEngine;
using UnityEngine.UI;           

public class GameManager : MonoBehaviour
{
    public Text[] AllText;
    public static GameManager Instance;
    [SerializeField] public FoodSpawner FoodSp;

    [Header("UI")]
    public Text Text;
    public GameObject gameOverPanel;

    private int score = 0;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        gameOverPanel.SetActive(false);
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

    public void TextPosition(Vector2 Pos)
    {
        for (int i = 0; i < 4; i++)
        {
            
        }
    }

    public void MathExs()
    {
        int Num1 = Random.Range(2, 20);
        int Num2 = Random.Range(2, 20);
        Text.text = Num1 + " + " + Num2 + "= ?";
        int Answer = Num1 + Num2;
    }
}
