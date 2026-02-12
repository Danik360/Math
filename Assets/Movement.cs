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
        // Сохраняем текущую позицию головы
        bodyPositions.Insert(0, transform.position);

        // Двигаем голову
        transform.position = (Vector2)transform.position + direction * moveDistance;

        // Двигаем тело
        for (int i = 0; i < bodyParts.Count; i++)
        {
            bodyParts[i].position = bodyPositions[i + 0];
        }

        // Удаляем лишние позиции
        if (bodyPositions.Count > bodyParts.Count + 1)
        {
            bodyPositions.RemoveAt(bodyParts.Count + 1);
        }
    }

    void Grow()
    {
        // Создаем новый сегмент тела
        GameObject newBody = Instantiate(bodyPrefab, bodyPositions[bodyParts.Count], Quaternion.identity);
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
            int Score =+ 10;
            tScore.text = $"Очки: ({Score})";
            Grow();  // +1 сегмент
            FP.Start();
            moveTime += 0.1f;  // чуть быстрее
        }
        else
        {
            Debug.Log($"❌ Неправильно! ({numberValue} != {GM.Answer}) Змейка уменьшается!");
            Shrink();  // -1 сегмент
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
