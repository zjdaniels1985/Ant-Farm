using UnityEngine;

/// <summary>
/// Represents a food source in the colony simulation.
/// Quantity decreases as ants forage from it.
/// When depleted, the food object destroys itself.
/// </summary>
public class Food : MonoBehaviour
{
    public Vector2Int Position { get; set; }
    public int Quantity = 10; // Starting amount of food

    /// <summary>
    /// Called when an ant forages from this food source.
    /// Decreases quantity and destroys the object if depleted.
    /// </summary>
    /// <param name="amount">Amount of food taken (default 1).</param>
    public void Forage(int amount = 1)
    {
        Quantity -= amount;
        if (Quantity <= 0)
        {
            OnDepleted();
        }
    }

    /// <summary>
    /// Handles logic when food is fully depleted.
    /// </summary>
    private void OnDepleted()
    {
        // Optionally: Notify the colony or manager here
        Destroy(gameObject);
    }
}