using UnityEngine;
using Mirror;
using UnityEngine.Events;
using System;

public class Unit : NetworkBehaviour
{
    [SerializeField] private UnitMovement unitMovement = null;
    [SerializeField] private UnityEvent onSelected = null;
    [SerializeField] private UnityEvent onDeselected = null;

    public static event Action<Unit> ServerOnUnitSpawned;
    public static event Action<Unit> ServerOnUnitDespawned;

    public static event Action<Unit> AuthorityOnUnitSpawned;
    public static event Action<Unit> AuthorityOnUnitDespawned;

    public UnitMovement GetUnitMovement()
    {
        return unitMovement;
    }

    #region Server

    public override void OnStartServer()
    {
        ServerOnUnitSpawned?.Invoke(this);
    }

    public override void OnStopServer()
    {
        ServerOnUnitDespawned?.Invoke(this);
    }

    #endregion

    #region Client

    public override void OnStartClient()
    {
        // * if the user is also the host or doesn't have authority, don't add to the list
        // * if the user is host, the ServerOnUnitSpawned method was already called so this avoids duplicates
        // * And only run for Units that belong to the client that owns them
        if (!isClientOnly || !this.hasAuthority) { return; }
        AuthorityOnUnitSpawned?.Invoke(this);
    }

    public override void OnStopClient()
    {
        // * if the user is also de host or doesn't have authority, don't invoke
        if (!isClientOnly || !this.hasAuthority) { return; }
        AuthorityOnUnitDespawned?.Invoke(this);
    }

    [Client]
    public void Select()
    {
        if (!this.hasAuthority) { return; }
        onSelected?.Invoke();
    }

    [Client]
    public void Deselect()
    {
        if (!this.hasAuthority) { return; }
        onDeselected?.Invoke();
    }

    #endregion
}
