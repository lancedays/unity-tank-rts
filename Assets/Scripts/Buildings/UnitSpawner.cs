using Mirror;
using System.Collections;
using System.Collections.Generic;
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
        for (var i = 0; i < 1; i++)
        {
            GameObject unitIsntance = Instantiate(unitPrefab, unitSpawnPoint.position, unitSpawnPoint.rotation);
            NetworkServer.Spawn(unitIsntance, connectionToClient);
        }
    }




    #endregion

    #region Client

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left) { return; }
        if (!hasAuthority) { return; }
        CmdSpawnUnit();
    }

    #endregion
}
