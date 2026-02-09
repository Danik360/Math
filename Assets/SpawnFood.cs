using UnityEngine;
using UnityEngine.UI;  // для Canvas

public class FoodSpawner : MonoBehaviour
{
    [SerializeField] public GameManager GM;
    public GameObject foodPrefab;
    public Vector2 mapSize = new Vector2(20, 20);

    [SerializeField] private Canvas uiCanvas;  // перетащи Canvas в инспекторе
    [SerializeField] private Camera mainCamera;  // главная камера

    void Start()
    {
        GM.MathExs();

        int[] numbers = { GM.Answer, GM.falseAnswer1, GM.falseAnswer2 };

        for (int i = 0; i < 3; i++)
        {
            Vector2 worldPos = new Vector2(
                Random.Range(-mapSize.x / 2f, mapSize.x / 2f),
                Random.Range(-mapSize.y / 2f, mapSize.y / 2f)
            );

            GameObject food = Instantiate(foodPrefab, worldPos, Quaternion.identity);
            Food foodScript = food.GetComponent<Food>();
            foodScript.numberValue = numbers[i];  // задаем число!
            GM.TextPosition(worldPos);

            // TextMeshPro над едой (если используешь)
            // ...
        }
    }
    
    public void SpawnNewFood()
    {
        // Новая математика!
        GM.MathExs();
        
        int[] numbers = { GM.Answer, GM.falseAnswer1, GM.falseAnswer2 };
        Vector2 newPos = new Vector2(
            Random.Range(-mapSize.x / 2f, mapSize.x / 2f),
            Random.Range(-mapSize.y / 2f, mapSize.y / 2f)
        );
        
        GameObject newFood = Instantiate(foodPrefab, newPos, Quaternion.identity);
        Food foodScript = newFood.GetComponent<Food>();
        if (foodScript != null)
        {
            foodScript.numberValue = numbers[Random.Range(0, 3)];  // случайное число из новых
        }
    }
}
