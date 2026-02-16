using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;  

public class Movement : MonoBehaviour
{
    [Header("Movement")]
    public float moveDistance = 0.5f;
    public float moveTime = 0.5f;

    [Header("Prefabs")]
    public GameObject bodyPrefab;

    private Vector2 direction = Vector2.right;
    private List<Transform> bodyParts = new List<Transform>();
    private List<Vector2> bodyPositions = new List<Vector2>();
    [SerializeField] public GameManager GM;
    [SerializeField] public FoodSpawner FP;
    public Text tScore;
    public int numberValue;
    int Score = 0;

    void Start()
    {
        tScore.text = $"Очки: {Score}";
        bodyPositions.Clear();
        bodyPositions.Add(transform.position); // позиция головы на шаге 0
        InvokeRepeating(nameof(Move), moveTime, moveTime);
    }



    void Update()
    {
        // Управление (WASD или стрелки)
        if (Input.GetKeyDown(KeyCode.W)) direction = Vector2.up;
        if (Input.GetKeyDown(KeyCode.S)) direction = Vector2.down;
        if (Input.GetKeyDown(KeyCode.A)) direction = Vector2.left;
        if (Input.GetKeyDown(KeyCode.D)) direction = Vector2.right;
    }

    void Move()
    {
        // сначала запоминаем текущую позицию головы
        bodyPositions.Insert(0, transform.position);

        // двигаем голову
        transform.position = (Vector2)transform.position + direction * moveDistance;

        // двигаем тело
        for (int i = 0; i < bodyParts.Count; i++)
        {
            bodyParts[i].position = bodyPositions[i + 0]; // i+1, потому что [0] — новая позиция головы
        }

        // чистим лишние позиции
        if (bodyPositions.Count > bodyParts.Count + 1)
        {
            bodyPositions.RemoveAt(bodyParts.Count + 1);
        }
    }


    void Grow()
    {
        Vector3 spawnPos;

        if (bodyPositions.Count >= 2)
        {
            // [0] — позиция головы до текущего шага
            // [1] — позиция позади головы (где она была шаг назад)
            spawnPos = bodyPositions[1];
        }
        else
        {
            // на всякий случай, если что-то пошло не так
            spawnPos = transform.position - (Vector3)direction * moveDistance;
        }

        GameObject newBody = Instantiate(bodyPrefab, spawnPos, Quaternion.identity);
        bodyParts.Add(newBody.transform);
    }



void OnTriggerEnter2D(Collider2D collision)
{
    if (collision.CompareTag("Food"))
    {
        // Берём скрипт Food с объекта, в который врезались
        Food food = collision.GetComponent<Food>();

        if (food != null)
        {
            // Передаём число в проверку
            CheckAnswer(food.numberValue);
        }

        Destroy(collision.gameObject);
    }
}


    void OnCollisionEnter2D(Collision2D collision)
    {
        GameManager.Instance.GameOver();
    }

    void CheckAnswer(int numberValue)
    {
        if (numberValue == GM.Answer)
        {
            Debug.Log("✅ ПРАВИЛЬНО! Змейка растет!");
            Score = Score + 10;
            tScore.text = $"Очки: {Score}";
            Grow();  // +1 сегмент
            FP.SpawnNewFood();
            moveTime += 0.1f;  // чуть быстрее
        }
        else
        {
            Debug.Log($"❌ Неправильно! ({numberValue} != {GM.Answer}) Змейка уменьшается!");
            Score = Score - 10;
            tScore.text = $"Очки: {Score}";
            Shrink();  // -1 сегмент
            FP.SpawnNewFood();
        }
    }
    
    public void Shrink()
    {
        if (bodyParts.Count > 0)
        {
            // Удаляем хвост
            Destroy(bodyParts[bodyParts.Count - 1].gameObject);
            bodyParts.RemoveAt(bodyParts.Count - 1);
        }
        else
        {
            // Если тело пустое — GameOver
            GM.GameOver();
        }
    }
}
