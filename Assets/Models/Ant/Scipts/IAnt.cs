using UnityEngine;

/// <summary>
/// Interface for all Ant types in the colony simulation.
/// Enforces basic properties and actions that all Ants must implement.
/// </summary>
public interface IAnt
{
    // The position of the ant in the world/grid.
    Vector2Int Position { get; set; }

    // The role of the ant (e.g., Worker, Soldier, Queen, etc.)
    AntRole Role { get; }

    // The age of the ant (in days, hours, etc.)
    float Age { get; set; }

    // Reference to the colony this ant belongs to.
    Colony ColonyRef { get; set; }

    // Called each simulation tick to update the ant's logic.
    void Tick();

    // Move the ant towards a target position.
    void MoveTowards(Vector2Int target);

    // Perform the ant's primary action (forage, dig, defend, etc.)
    void PerformAction();

    // Sense the environment (food, threats, tunnels, etc.)
    void Sense();

    // Called when the ant dies (cleanup, events, etc.)
    void Die();
}
