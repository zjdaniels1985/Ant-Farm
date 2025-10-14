using UnityEngine;

/// <summary>
/// Handles spawning of Food objects in the world when requested (e.g. via UI).
/// Attach this script to a GameObject in your scene.
/// </summary>
public class FoodSpawner : MonoBehaviour
{
    [Header("Food Prefab Reference")]
    public GameObject foodPrefab;

    // Optionally, set a default quantity for spawned food
    public int defaultFoodQuantity = 10;

    /// <summary>
    /// Spawns a food object at the specified world position.
    /// Call this from your UI event handler, passing the desired position.
    /// </summary>
    /// <param name="worldPosition">The world position to spawn the food.</param>
    public void SpawnFood(Vector3 worldPosition)
    {
        if (foodPrefab == null)
        {
            Debug.LogError("Food prefab is not assigned!");
            return;
        }

        GameObject foodObj = Instantiate(foodPrefab, worldPosition, Quaternion.identity);
        Food foodComponent = foodObj.GetComponent<Food>();
        if (foodComponent != null)
        {
            foodComponent.Quantity = defaultFoodQuantity;
            // If you want to set Position property (grid/2D), convert worldPosition to Vector2Int accordingly
            foodComponent.Position = Vector2Int.RoundToInt(new Vector2(worldPosition.x, worldPosition.y));
        }
    }

    // Example UI callback (call this from your UI button, passing mouse or chosen position)
    public void OnUserPlaceFood(Vector3 worldPosition)
    {
        SpawnFood(worldPosition);
    }
}