using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    public GameObject foodPrefab;
    public Vector2 mapSize = new Vector2(20, 20);

    void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            Vector2 randomPos = new Vector2
            (
                Mathf.Round(Random.Range(-mapSize.x / 2, mapSize.x / 2)),
                Mathf.Round(Random.Range(-mapSize.y / 2, mapSize.y / 2))
            );
            Instantiate(foodPrefab, randomPos, Quaternion.identity);
        }
    }
}
