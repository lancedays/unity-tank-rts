using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UnitCommandGiver : MonoBehaviour
{
    [SerializeField] private UnitSelectionHandler unitSelectionHandler = null;
    [SerializeField] private LayerMask layerMask = new LayerMask();
    private Camera mainCamera;
    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        // If the right mouse button was pressed this frame
        if (!Mouse.current.rightButton.wasPressedThisFrame) { return; }
        // Get the screen point
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        // If the raycast hit
        if (!Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask)) { return; }

        TryMove(hit.point);
    }

    private void TryMove(Vector3 pos)
    {
        // We have a reference to the selected units, for each selected unit move to position
        foreach (Unit unit in unitSelectionHandler.SelectedUnits)
        {
            unit.GetUnitMovement().CmdMove(pos);
        }
    }
}