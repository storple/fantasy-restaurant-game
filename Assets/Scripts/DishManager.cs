using UnityEngine;

public class DishManager : MonoBehaviour
{
    public DishData[] availableDishes; // array of all available dishes

    public DishData GetRandomDish()
    {
        if (availableDishes.Length == 0)
        {
            Debug.LogError("No dishes available in DishManager.");
            return null;
        }

        return availableDishes[Random.Range(0, availableDishes.Length)];
    }
}
