using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTSPlayer : NetworkBehaviour
{
    [SerializeField] private List<Unit> myUnits = new List<Unit>();


    #region Server
    public override void OnStartServer()
    {
        // Subscribe to unit spawn/despawn events
        Unit.ServerOnUnitSpawned += ServerHandleUnitSpawned;
        Unit.ServerOnUnitDespawned += ServerHandleUnitDespawned;
    }

    public override void OnStopServer()
    {
        // Un-Subscribe to unit spawn/despawn events
        Unit.ServerOnUnitSpawned -= ServerHandleUnitSpawned;
        Unit.ServerOnUnitDespawned -= ServerHandleUnitDespawned;
    }

    private void ServerHandleUnitSpawned(Unit unit)
    {
        // SELF NOTE: I believe that because this script is attached to the player object, it will
        // be run for each player, each time a unit is spawned. So if there are 4 players and a single
        // unit is spawned on the server then this function will be invoked 4 times. This could cause
        // performance issues.


        // Check the owner of the unit, if the unit belongs to player, add it to the list
        if (unit.connectionToClient.connectionId != connectionToClient.connectionId) { return; }
        myUnits.Add(unit);
    }

    private void ServerHandleUnitDespawned(Unit unit)
    {
        // SELF NOTE: I believe that because this script is attached to the player object, it will
        // be run for each player, each time a unit is spawned. So if there are 4 players and a single
        // unit is spawned on the server then this function will be invoked 4 times. This could cause
        // performance issues.


        // Check the owner of the unit, if the unit belongs to player, remove it from the list
        if (unit.connectionToClient.connectionId != connectionToClient.connectionId) { return; }
        myUnits.Remove(unit);
    }
    #endregion

    #region Client
    public override void OnStartClient()
    {
        if (!isClientOnly) { return; }
        Unit.AuthorityOnUnitSpawned += AuthorityHandleUnitSpawned;
        Unit.AuthorityOnUnitDespawned += AuthorityHandleUnitDespawned;
    }

    public override void OnStopClient()
    {
        if (!isClientOnly) { return; }
        Unit.AuthorityOnUnitSpawned -= AuthorityHandleUnitSpawned;
        Unit.AuthorityOnUnitDespawned -= AuthorityHandleUnitDespawned;
    }

    private void AuthorityHandleUnitSpawned(Unit unit)
    {
        if (!hasAuthority) { return; }
        myUnits.Remove(unit);
    }

    private void AuthorityHandleUnitDespawned(Unit unit)
    {
        if (!hasAuthority) { return; }
        myUnits.Remove(unit);
    }

    #endregion
}
