using UnityEngine;

/// <summary>
/// Represents the Queen ant. Once she is tended enough times, she lays a new brood for the colony.
/// </summary>
public class Queen : Ant
{
    public int TendingProgress { get; private set; }
    public int TendingNeeded = 3; // Number of times needed before laying

    // Position of the Queen in the colony
    public override Vector2Int Position { get; set; }
    public override AntRole Role => AntRole.Queen;

    public override Colony ColonyRef { get; set; }
    public override float Age { get; set; }

    public void Tend()
    {
        if (TendingProgress < TendingNeeded)
        {
            TendingProgress++;
            if (TendingProgress >= TendingNeeded)
            {
                // Lay a new brood and reset progress
                NotifyColonyToLayBrood();
                TendingProgress = 0;
            }
        }
    }

    private void NotifyColonyToLayBrood()
    {
        if (ColonyRef != null)
        {
            ColonyRef.LayBrood(Position);
        }
    }

    // Optional: If you want the Queen to age, sense, etc.
    public override void Tick()
    {
        Age += Time.deltaTime;
        // Queen-specific logic here
    }

    public override void MoveTowards(Vector2Int target) {}
    public override void PerformAction() {}
    public override void Sense() {}
    public override void Die()
    {
        ColonyRef?.RemoveAnt(this);
        Destroy(gameObject);
    }
}
