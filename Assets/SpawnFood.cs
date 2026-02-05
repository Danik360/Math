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
        
        for (int i = 0; i < 3; i++)
        {
            // Инициализируем сразу полностью!
            Vector2 worldPos = Vector2.zero;  // или new Vector2(0,0)
            
            worldPos.x = Mathf.Round(Random.Range(-mapSize.x / 2, mapSize.x / 2));
            worldPos.y = Mathf.Round(Random.Range(-mapSize.y / 2, mapSize.y / 2));
            
            // Теперь используем
            Instantiate(foodPrefab, worldPos, Quaternion.identity);
            GM.TextPosition(worldPos);
        }
    }
}
