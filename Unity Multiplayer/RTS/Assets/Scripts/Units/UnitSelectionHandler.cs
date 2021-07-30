using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.InputSystem;

public class UnitSelectionHandler : MonoBehaviour
{
    [SerializeField] private RectTransform unitSelectionArea = null;
    [SerializeField] private LayerMask layerMask = new LayerMask();

    private Vector2 startPosition;
    private RTSPlayer player;
    private Camera mainCamera;
    public List<Unit> SelectedUnits { get; } = new List<Unit>();

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (player == null)
        {
            // * Even if this script is Monobehaviour, one can get the Player from the NetworkClient like so
            player = NetworkClient.connection.identity.GetComponent<RTSPlayer>();
        }

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            StartSelectionArea();
        }
        else if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            ClearSelectionArea();
        }
        else if (Mouse.current.leftButton.isPressed)
        {
            UpdateSelectionArea();
        }
    }

    private void StartSelectionArea()
    {
        // * Check if the user is pressing the left shift key to know if 
        // * the selection should be reset or just adding more units
        if (!Keyboard.current.leftShiftKey.isPressed)
        {
            // * Clear the list
            foreach (Unit selectedUnit in SelectedUnits)
            {
                selectedUnit.Deselect();
            }
            SelectedUnits.Clear();
        }

        // * Show the Selecting box UI
        unitSelectionArea.gameObject.SetActive(true);

        // * Store the initial mouse position when clicking
        startPosition = Mouse.current.position.ReadValue();

        UpdateSelectionArea();
    }

    private void UpdateSelectionArea()
    {
        // * update the UI
        Vector2 mousePosition = Mouse.current.position.ReadValue();

        // * Get the selection box dimensions from the current mouse pos relative to the initial one
        float areaWidth = mousePosition.x - startPosition.x;
        float areaHeight = mousePosition.y - startPosition.y;

        // * Set the box dims
        unitSelectionArea.sizeDelta = new Vector2(Mathf.Abs(areaWidth), Mathf.Abs(areaHeight));

        // * Where's the box in the screen?
        // * This sets the MIDDLE of the box, so you go from the initial mouse position
        // * half way the width and then half way the height.
        // * This with the BOTTOM LEFT ANCHOR set in Rec Transform in the Canvas child of this SelectionHandler
        unitSelectionArea.anchoredPosition = startPosition + new Vector2(areaWidth / 2, areaHeight / 2);

    }

    private void ClearSelectionArea()
    {
        unitSelectionArea.gameObject.SetActive(false);

        // * If the selection box size is zero when unpressing the mouse click
        // * it means that the use just clicked and did not drag, so the selection of a unit
        // * must that of a single unit
        if (unitSelectionArea.sizeDelta.magnitude == 0)
        {

            Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

            // * Check if something was hit that belongs to the correct Unity Layer
            if (!Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask))
            {
                return;
            }

            // * Check if what was hit was a Unit
            if (!hit.collider.TryGetComponent<Unit>(out Unit unit)) { return; };

            // * Check if the unit belongs to the client
            if (!unit.hasAuthority) { return; }

            SelectedUnits.Add(unit);

            foreach (Unit selectedUnit in SelectedUnits)
            {
                selectedUnit.Select();
            }

            return;
        }

        // * Get the min point (bottom left) and max point (top right) of the selection box
        Vector2 min = unitSelectionArea.anchoredPosition - (unitSelectionArea.sizeDelta / 2);
        Vector2 max = unitSelectionArea.anchoredPosition + (unitSelectionArea.sizeDelta / 2);

        foreach (Unit unit in player.GetMyUnits())
        {
            // * Don't add the same unit more than once
            if (SelectedUnits.Contains(unit)) { continue; }

            // * Get the unit's position in the 2D screen from the 3D position (if it's not visible by the camera, it'd 0) 
            Vector3 screePosition = mainCamera.WorldToScreenPoint(unit.transform.position);

            // * Check if the unit position is within the selection box, if it is, add it to the selected units
            if (
                screePosition.x > min.x &&
                screePosition.x < max.x &&
                screePosition.y > min.y &&
                screePosition.y < max.y
            )
            {
                SelectedUnits.Add(unit);
                unit.Select();
            }
        }
    }
}
