using UnityEngine;
using UnityEngine.UI;           

public class GameManager : MonoBehaviour
{
    public Text[] AllText;
    public static GameManager Instance;
    [SerializeField] public FoodSpawner FoodSp;
    private int Usetext;
    private int Answer;
    private int falseAnswer1;
    private int falseAnswer2;

    [Header("UI")]
    public Text Text;
    public GameObject gameOverPanel;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        gameOverPanel.SetActive(false);
        Usetext = 0;
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
        if (Usetext == 0)
        {
            Debug.Log("сработал текст номер 1");
            AllText[Usetext].text = Answer.ToString();
            AllText[Usetext].transform.position = Pos;
            Usetext++;
        }
        else if (Usetext == 1)
        {
            Debug.Log("сработал текст номер 2");
            AllText[Usetext].text = falseAnswer1.ToString();
            AllText[Usetext].transform.position = Pos;
            Usetext++;
        }
        else
        {
            Debug.Log("сработал текст номер 3");
            AllText[Usetext].text = falseAnswer2.ToString();
            AllText[Usetext].transform.position = Pos;
        }
        Debug.Log($"Текст {Usetext} позиция: {Pos}, активен: {AllText[Usetext].gameObject.activeSelf}");
        AllText[Usetext].transform.position = Pos;
        AllText[Usetext].gameObject.SetActive(true);  // принудительно включи

    }

        public void MathExs()
    {
        int Numex1 = Random.Range(2,10);
        int Numex2 = Random.Range(2,10);
        int Num1 = Random.Range(2, 20);
        int Num2 = Random.Range(2, 20);
        int Type = Random.Range(1, 3);  // 1-2, а не 1-2 (было 1,2 одинаково)

        if (Type == 1)
        {
            Text.text = Num1 + " + " + Num2 + "= ?";
            Answer = Num1 + Num2;              // убрать int (локальную)
            falseAnswer1 = Num1 + Num2 + Numex1;
            falseAnswer2 = Num1 + Num2 - Numex2;
        }
        else  // Type == 2
        {
            if (Num1 >= Num2)
            {
                Text.text = Num1 + " - " + Num2 + "= ?";
                Answer = Num1 - Num2;
                falseAnswer1 = Num1 - Num2 + Numex1;
                falseAnswer2 = Num1 - Num2 - Numex2;
            }
            else
            {
                Text.text = Num2 + " - " + Num1 + "= ?";
                Answer = Num2 - Num1;
                falseAnswer1 = Num2 - Num1 + Numex1;
                falseAnswer2 = Num2 - Num1 - Numex2;
            }
        }
        
        Debug.Log($"Правильный: {Answer}, Ложные: {falseAnswer1}, {falseAnswer2}");  // для проверки
    }
}
