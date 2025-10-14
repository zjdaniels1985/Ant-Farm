using UnityEngine;

/// <summary>
/// Abstract base class for all ant types in the colony simulation.
/// Implements IAnt interface and provides common properties and base logic.
/// </summary>
public abstract class Ant : MonoBehaviour, IAnt
{
    public virtual Vector2Int Position { get; set; }
    public abstract AntRole Role { get; }
    public virtual float Age { get; set; }
    public virtual Colony ColonyRef { get; set; }

    // Optional: Common property for all ants
    public float Energy { get; set; }

    // Abstract methods to be implemented by child classes
    public abstract void Tick();
    public abstract void MoveTowards(Vector2Int target);
    public abstract void PerformAction();
    public abstract void Sense();
    public abstract void Die();

    // Optionally: Provide some shared logic or helper methods here
    protected void ConsumeEnergy(float amount)
    {
        Energy -= amount;
        if (Energy <= 0)
        {
            Die();
        }
    }
}
