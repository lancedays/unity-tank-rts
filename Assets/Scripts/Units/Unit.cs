using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Unit : NetworkBehaviour
{
    [SerializeField] private UnitMovement unitMovement = null;
    [SerializeField] private UnityEvent onSelected = null;
    [SerializeField] private UnityEvent onDeselected = null;

    public UnitMovement GetUnitMovement()
    {
        return unitMovement;
    }


    #region Server
    #endregion

    #region Client
    [Client]
    public void Select()
    {
        if(!hasAuthority) { return; }
        onSelected?.Invoke();
    }

    [Client]
    public void Deslect()
    {
        if(!hasAuthority){ return; }
        onDeselected?.Invoke();
    }
    #endregion
}
