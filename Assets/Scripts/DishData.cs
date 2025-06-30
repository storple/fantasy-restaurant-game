using UnityEngine;

[CreateAssetMenu(fileName = "NewDish", menuName = "Restaurant/Dish")]
public class DishData : ScriptableObject
{
    public string dishName; // name of the dish
    public Sprite dishIcon; // sprite icon for the dish
    public string[] requiredStations; // list of stations required to prepare the dish (e.g., "stove", "oven", "grill")
}
