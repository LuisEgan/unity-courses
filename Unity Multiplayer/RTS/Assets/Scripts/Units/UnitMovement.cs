using Mirror;
using UnityEngine;
using UnityEngine.AI;

public class UnitMovement : NetworkBehaviour
{
    [SerializeField] private NavMeshAgent agent = null;

    [ServerCallback]
    private void Update()
    {
        // * This check stops from clearing the path in the same frame
        // * it calculates it
        if (!agent.hasPath) { return; }

        // * Check if the unit is within stopping distance
        // * The stopping distance is set in the NavMeshAgent component in Unity
        if (agent.remainingDistance > agent.stoppingDistance) { return; }

        // * if it is, clear the path so it stops
        agent.ResetPath();
    }


    #region Server

    [Command]
    public void CmdMove(Vector3 position)
    {
        // * Only let the user move witihn the NavMesh baked area
        if (!NavMesh.SamplePosition(position, out NavMeshHit hit, 1f, NavMesh.AllAreas))
        {
            return;
        }

        agent.SetDestination(hit.position);
    }

    #endregion
}
