using UnityEngine;

public class Food : MonoBehaviour
{
    public int numberValue = 0;  // число на еде (задастся из FoodSpawner)
    
    void Start()
    {
        // Коллайдер должен быть Trigger!
        GetComponent<Collider2D>().isTrigger = true;
        gameObject.tag = "Food";
    }
}
