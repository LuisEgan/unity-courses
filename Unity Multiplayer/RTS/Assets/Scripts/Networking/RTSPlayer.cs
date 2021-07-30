using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class RTSPlayer : NetworkBehaviour
{
    [SerializeField] private List<Unit> myUnits = new List<Unit>();

    public List<Unit> GetMyUnits()
    {
        return myUnits;
    }


    #region Server

    public override void OnStartServer()
    {
        // * Subscribe to the events
        Unit.ServerOnUnitSpawned += ServerHandleUnitSpawned;
        Unit.ServerOnUnitDespawned += ServerHandleUnitDespawned;
    }

    public override void OnStopServer()
    {
        // * Unsubscribe to the events
        Unit.ServerOnUnitSpawned -= ServerHandleUnitSpawned;
        Unit.ServerOnUnitDespawned -= ServerHandleUnitDespawned;
    }

    private void ServerHandleUnitSpawned(Unit unit)
    {
        // * Check if the client is the owner of the unit to add it to their list
        if (unit.connectionToClient.connectionId != this.connectionToClient.connectionId) { return; }

        myUnits.Add(unit);
    }

    private void ServerHandleUnitDespawned(Unit unit)
    {
        // * Check if the client is the owner of the unit to remove it from their list
        if (unit.connectionToClient.connectionId != this.connectionToClient.connectionId) { return; }

        myUnits.Remove(unit);
    }

    #endregion

    #region Client

    public override void OnStartClient()
    {
        // * Ensuring the host only subscribes once
        if (!isClientOnly)
        {
            Unit.AuthorityOnUnitSpawned += AuthorityHandleUnitSpawned;
            Unit.AuthorityOnUnitSpawned += AuthorityHandleUnitDespawned;
        }
    }

    public override void OnStopClient()
    {
        // * Ensuring the host only subscribes once
        if (!isClientOnly)
        {
            Unit.AuthorityOnUnitDespawned -= AuthorityHandleUnitSpawned;
            Unit.AuthorityOnUnitDespawned -= AuthorityHandleUnitDespawned;
        }
    }

    private void AuthorityHandleUnitSpawned(Unit unit)
    {
        // * Check if the client is the owner of the unit to add it to their list
        if (!this.hasAuthority) { return; }
        myUnits.Add(unit);
    }


    private void AuthorityHandleUnitDespawned(Unit unit)
    {
        // * Check if the client is the owner of the unit to remove it from their list
        if (!this.hasAuthority) { return; }
        myUnits.Remove(unit);
    }

    #endregion

}
