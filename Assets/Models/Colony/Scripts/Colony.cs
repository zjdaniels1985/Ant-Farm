
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Colony : MonoBehaviour
{
    public List<Ant> Ants = new List<Ant>(); // Track all ants in the colony
    public List<Brood> Broods = new List<Brood>(); // Track all broods in the colony
    public int Food;
    public int BroodCount => Broods.Count;
    public int[,] Map; // 0 = dirt, 1 = tunnel

    public bool NeedsDefense(Vector2Int position)
    {
        // Example: return true if an enemy is near the given position
        // TODO: Implement actual logic
        return false;
    }

    public bool NeedsFood()
    {
        // Example: return true if colony food is below some threshold
        // TODO: Implement actual logic
        return false;
    }

    public bool NeedsNursing()
    {
        // Example: return true if there are untended larvae/brood
        // TODO: Implement actual logic
        return false;
    }

    public bool NeedsDigging()
    {
        // Example: return true if expansion is needed
        // TODO: Implement actual logic
        return false;
    }
    
    // Find the nearest food position within a given radius from a position.
    public Vector2Int? FindNearestFood(Vector2Int position, float radius)
    {
        // TODO: Implement actual food-finding logic
        // For now, return null to indicate no food found
        return null;
    }

    // Collect food from a specific position.
    public void CollectFood(Vector2Int foodPosition)
    {
        // TODO: Implement actual food collection and update Food count
        // For now, just increment Food
        Food++;
    }
    
    // Find a diggable (dirt) spot adjacent to the given position.
    public Vector2Int? FindDiggable(Vector2Int position)
    {
        // Example: Check adjacent tiles for diggable dirt (value 0)
        Vector2Int[] directions = {
            Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right
        };
        foreach (var dir in directions)
        {
            Vector2Int checkPos = position + dir;
            // Check bounds and if dirt
            if (checkPos.x >= 0 && checkPos.x < Map.GetLength(0) &&
                checkPos.y >= 0 && checkPos.y < Map.GetLength(1) &&
                Map[checkPos.x, checkPos.y] == 0)
            {
                return checkPos;
            }
        }
        return null;
    }

    // Dig at the specified position (convert dirt to tunnel)
    public void DigAt(Vector2Int digSpot)
    {
        // Set tile to tunnel (1)
        if (digSpot.x >= 0 && digSpot.x < Map.GetLength(0) &&
            digSpot.y >= 0 && digSpot.y < Map.GetLength(1))
        {
            Map[digSpot.x, digSpot.y] = 1;
        }
    }
    
    // Find the nearest enemy position within a given radius from a position.
    public Vector2Int? FindNearestEnemy(Vector2Int position, float radius)
    {
        // TODO: Implement actual enemy-finding logic
        // For now, return null to indicate no enemy found
        return null;
    }

    // Attack the enemy at a specific position.
    public void AttackEnemy(Vector2Int enemyPosition)
    {
        // TODO: Implement actual attack logic (e.g., remove enemy, trigger animation, etc.)
        // For now, this is just a stub
    }
    
    
    // Lay a new brood at a specific position
    public void LayBrood(Vector2Int position)
    {
        GameObject broodObj = new GameObject("Brood");
        Brood newBrood = broodObj.AddComponent<Brood>();
        newBrood.Position = position;
        newBrood.ColonyRef = this;
        Broods.Add(newBrood);
    }

    // Tend to brood/larvae at a specific position.
    public void TendBrood(Vector2Int position)
    {
        Brood brood = Broods.Find(b => b.Position == position);
        if (brood != null)
        {
            brood.Tend();
        }
    }
    
    // Remove ant from colony when it dies.
    public void RemoveAnt(Ant ant)
    {
        Ants.Remove(ant);
    }
}
