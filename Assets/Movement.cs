using UnityEngine;
using System.Collections.Generic;

public class SnakeController : MonoBehaviour
{
    [Header("Movement")]
    public float moveDistance = 0.5f;
    public float moveTime = 0.5f;
    
    [Header("Prefabs")]
    public GameObject bodyPrefab;
    
    private Vector2 direction = Vector2.right;
    private List<Transform> bodyParts = new List<Transform>();
    private List<Vector2> bodyPositions = new List<Vector2>();
    
    void Start()
    {
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
            Grow();
            Destroy(collision.gameObject);
        }
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        GameManager.Instance.GameOver();
    }
}
