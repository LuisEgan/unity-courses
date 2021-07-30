using Mirror;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnitSpawner : NetworkBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject unitPrefab = null;
    [SerializeField] private Transform unitSpawnPoint = null;

    #region Server

    [Command]
    private void CmdSpawnUnit()
    {
        // * Instantiate on the server
        GameObject unitInstance = Instantiate(unitPrefab, unitSpawnPoint.position, unitSpawnPoint.rotation);
        // * Spawn on the network for all clients
        // * 2nd parameter, this.connectionToClient is which client is the owner so the authority is set to the player that
        // * spawns it
        NetworkServer.Spawn(unitInstance, this.connectionToClient);
    }


    #endregion

    #region Client

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left) { return; }

        if (!this.hasAuthority) { return; }

        CmdSpawnUnit();
    }

    #endregion
}
