using UnityEngine;

/// <summary>
/// CarpenterAnt class can dynamically switch roles based on colony needs and environment.
/// </summary>
public class CarpenterAnt : Ant
{
    // The current role for this ant (can change at runtime)
    private AntRole currentRole;
    public override AntRole Role => currentRole;

    // Additional states or info
    private AntState currentState;

    // Constructor or initialization
    public CarpenterAnt()
    {
        // Default role
        currentRole = AntRole.Worker;
        currentState = AntState.Idle;
    }

    public override void Tick()
    {
        Age += Time.deltaTime;
        Sense();
        EvaluateRole();
        PerformAction();
        ConsumeEnergy(0.05f);
    }

    public override void MoveTowards(Vector2Int target)
    {
        Vector2Int delta = target - Position;
        if (delta.magnitude > 1)
        {
            // Cast to Vector2, normalize, then round to Vector2Int for a grid step
            Vector2 step = ((Vector2)delta).normalized;
            Position += Vector2Int.RoundToInt(step);
        }
        else
        {
            Position = target;
        }
    }

    /// <summary>
    /// Sense the environment and update internal state.
    /// </summary>
    public override void Sense()
    {
        // Example: Detect threats, food, or brood in range, etc.
        // Set internal flags or state as needed for decision making.
    }

    /// <summary>
    /// Decide which role to take based on colony needs and environmental factors.
    /// </summary>
    private void EvaluateRole()
    {
        // Example logic: prioritize as needed
        if (ColonyRef != null)
        {
            if (ColonyRef.NeedsDefense(Position))
            {
                currentRole = AntRole.Soldier;
                currentState = AntState.Defending;
            }
            else if (ColonyRef.NeedsFood())
            {
                currentRole = AntRole.Worker;
                currentState = AntState.Foraging;
            }
            else if (ColonyRef.NeedsNursing())
            {
                currentRole = AntRole.Nurse; // If you define a Nurse role
                currentState = AntState.Breeding;
            }
            else if (ColonyRef.NeedsDigging())
            {
                currentRole = AntRole.Worker;
                currentState = AntState.Digging;
            }
            else
            {
                currentRole = AntRole.Worker;
                currentState = AntState.Idle;
            }
        }
    }

    public override void PerformAction()
    {
        switch (currentState)
        {
            case AntState.Foraging:
                Forage();
                break;
            case AntState.Digging:
                Dig();
                break;
            case AntState.Defending:
                Defend();
                break;
            case AntState.Breeding:
                Nurse();
                break;
            case AntState.Idle:
            default:
                Wander();
                break;
        }
    }

    private void Forage()
    {
        Vector2Int? foodPos = ColonyRef?.FindNearestFood(Position, 5f);
        if (foodPos.HasValue)
        {
            MoveTowards(foodPos.Value);
            if (Position == foodPos.Value)
            {
                ColonyRef.CollectFood(foodPos.Value);
                currentState = AntState.Returning;
            }
        }
        else
        {
            Wander();
        }
    }

    private void Dig()
    {
        Vector2Int? digSpot = ColonyRef?.FindDiggable(Position);
        if (digSpot.HasValue)
        {
            MoveTowards(digSpot.Value);
            if (Position == digSpot.Value)
            {
                ColonyRef.DigAt(digSpot.Value);
                currentState = AntState.Idle;
            }
        }
        else
        {
            Wander();
        }
    }

    private void Defend()
    {
        Vector2Int? enemyPos = ColonyRef?.FindNearestEnemy(Position, 5f);
        if (enemyPos.HasValue)
        {
            MoveTowards(enemyPos.Value);
            if (Position == enemyPos.Value)
            {
                ColonyRef.AttackEnemy(enemyPos.Value);
                currentState = AntState.Idle;
            }
        }
        else
        {
            currentState = AntState.Idle;
        }
    }

    private void Nurse()
    {
        // Example: Tend to brood/larvae
        ColonyRef?.TendBrood(Position);
        currentState = AntState.Idle;
    }

    private void Wander()
    {
        // Generate a random direction, normalize it, and convert to Vector2Int
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        Vector2Int step = Vector2Int.RoundToInt(randomDirection);

        // Only move if step is not zero (sometimes normalized vector can be very small and round to zero)
        if (step != Vector2Int.zero)
            Position += step;
    }

    public override void Die()
    {
        // Clean up, remove from colony, etc.
        ColonyRef?.RemoveAnt(this);
        Destroy(gameObject);
    }
}