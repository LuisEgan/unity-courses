using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : NetworkBehaviour
{
    [SerializeField] private NavMeshAgent agent = null;
    private Camera mainCamera;

    #region Server

    [Command]
    private void CmdMove(Vector3 position)
    {
        // * Only let the user move witihn the NavMesh baked area
        if (!NavMesh.SamplePosition(position, out NavMeshHit hit, 1f, NavMesh.AllAreas))
        {
            return;
        }

        agent.SetDestination(hit.position);
    }

    #endregion

    #region Client

    public override void OnStartAuthority()
    {
        mainCamera = Camera.main;
    }

    // * Prevent the server from running this method
    [ClientCallback]
    private void Update()
    {
        // * Check the player belongs to the client
        if (!this.hasAuthority) { return; }

        // * Don't do anything if the right click wasn't pressed
        if (!Input.GetMouseButtonDown(1)) { return; }

        // * Figure out where user clicked
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        // * Cast the ray from the camera into the scene to see what it hits, and don't
        // * let the user move if nothing in the scene was hit
        if (!Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
        {
            return;
        }

        // * Move!
        CmdMove(hit.point);

    }

    #endregion
}
