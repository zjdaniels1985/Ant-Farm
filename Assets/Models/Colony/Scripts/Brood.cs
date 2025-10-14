using UnityEngine;

/// <summary>
/// Represents brood (egg/larva/pupa) in the colony that can be tended by ants and matures into a CarpenterAnt.
/// </summary>
public class Brood : MonoBehaviour
{
    public Vector2Int Position { get; set; }
    public int TendingProgress { get; private set; }
    public int TendingNeeded = 5; // Number of times it needs to be tended before hatching

    public Colony ColonyRef { get; set; }
    public bool IsReadyToHatch => TendingProgress >= TendingNeeded;

    // Call this when an ant tends to this brood
    public void Tend()
    {
        if (!IsReadyToHatch)
        {
            TendingProgress++;
            if (IsReadyToHatch)
            {
                Hatch();
            }
        }
    }

    // Hatches into a new CarpenterAnt
    private void Hatch()
    {
        // Create a new CarpenterAnt at this position
        GameObject antObj = new GameObject("CarpenterAnt");
        CarpenterAnt newAnt = antObj.AddComponent<CarpenterAnt>();
        newAnt.Position = this.Position;
        newAnt.ColonyRef = this.ColonyRef;
        ColonyRef.Ants.Add(newAnt);

        // Remove this brood from colony brood list (if you track it)
        if (ColonyRef.Broods != null)
        {
            ColonyRef.Broods.Remove(this);
        }

        // Destroy the Brood game object
        Destroy(gameObject);
    }
}
