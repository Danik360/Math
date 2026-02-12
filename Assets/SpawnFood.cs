using UnityEngine;
using UnityEngine.UI;   
public class FoodSpawner : MonoBehaviour
{
    [SerializeField] private GameManager GM;
    [SerializeField] private GameObject foodPrefab;
    public Vector2 mapSize = new Vector2(20, 20);

    public void Start()
    {
        if (foodPrefab == null)
        {
            Debug.LogError("foodPrefab НЕ назначен в инспекторе!");
            return;
        }

        GM.MathExs();
        int[] numbers = { GM.Answer, GM.falseAnswer1, GM.falseAnswer2 };

        for (int i = 0; i < 3; i++)
        {
            Vector2 worldPos = new Vector2(
                Random.Range(-mapSize.x / 2f, mapSize.x / 2f),
                Random.Range(-mapSize.y / 2f, mapSize.y / 2f)
            );

            GameObject obj = Instantiate(foodPrefab, worldPos, Quaternion.identity);
            Food foodScript = obj.GetComponent<Food>();
            if (foodScript != null)
                foodScript.numberValue = numbers[i];

            GM.TextPosition(worldPos);
        }
    }

    public void SpawnNewFood()
    {
        // лучше убить всю старую еду, чем вызывать End() у одного
        GameObject[] foods = GameObject.FindGameObjectsWithTag("Food");
        for (int i = 0; i < foods.Length; i++)
            Destroy(foods[i]);

        GM.MathExs();
        int[] numbers = { GM.Answer, GM.falseAnswer1, GM.falseAnswer2 };

        for (int i = 0; i < 3; i++)
        {
            Vector2 worldPos = new Vector2(
                Random.Range(-mapSize.x / 2f, mapSize.x / 2f),
                Random.Range(-mapSize.y / 2f, mapSize.y / 2f)
            );

            GameObject obj = Instantiate(foodPrefab, worldPos, Quaternion.identity);
            Food foodScript = obj.GetComponent<Food>();
            if (foodScript != null)
                foodScript.numberValue = numbers[i];

            GM.TextPosition(worldPos);
        }
    }
}

