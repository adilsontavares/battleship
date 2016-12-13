using System;
using UnityEngine;
using UnityEngine.Events;

public class ShipPlacer : MonoBehaviour
{
    public Board Board;
    public GameObject[] ShipPrefabs;

    Ship[] _ships;

    public static ShipPlacer Main { get { return FindObjectOfType<ShipPlacer>(); } }

    public bool Placing { get { return _currentShip != null; } }

    Ship _currentShip = null;
    public Ship CurrentShip { get { return _currentShip; } }

    int _current = 0;
    public int Current { get { return _current; } }

    public string PlaceButton = "Fire1";
    public string RotateButton = "Fire2";

    void Start()
    {
        _ships = new Ship[ShipPrefabs.Length];

        CreateShipSelectorItems();
    }

    void CreateShipSelectorItems()
    {
        var shipSelector = ShipSelector.Main;

        for (var i = 0; i < ShipPrefabs.Length; ++i)
        {
            var prefab = ShipPrefabs[i];
            var ship = InstantiateShip(prefab);
            var item = shipSelector.AddItem(ship);

            ship.gameObject.SetActive(false);

            item.SetShip(ship);
            item.SetCallback(StartPlacement);

            _ships[i] = ship;
        }
    }

    void StartPlacement(Ship ship)
    {
        if (Placing)
            CancelPlacement();

        ship.gameObject.SetActive(true);
        
        var transform = ship.transform;

        transform.SetParent(Board.transform);
        transform.Reset();

        _currentShip = ship;
        _currentShip.Board = Board;
    }

    void Update()
    {
        if (Placing)
        {
            UpdatePlacement();

            if (Input.GetButtonDown(RotateButton))
                Rotate();
            else if (Input.GetButtonDown(PlaceButton))
                Place();
        }
    }

    void Rotate()
    {
        _currentShip.Rotate();
    }

    void UpdatePlacement()
    {
        var pointer = BoardPointer.Main;
        var index = pointer.FindIndex(Board);

        _currentShip.Index = index;
        _currentShip.UpdateTransform();
    }

    Ship InstantiateShip(GameObject prefab)
    {
        var gameObject = GameObject.Instantiate(prefab);
        var ship = gameObject.GetComponent<Ship>();

        Debug.Assert(ship != null, "Ship prefab must have a Ship script attached to it.");

        return ship;
    }

    public void StartPlacement()
    {
        if (Done())
            return;

        StartPlacement(_ships[Current]);
    }

    public void Place()
    {
        if (!Placing)
            return;

        FinishPlacement(true);
    }

    public void CancelPlacement()
    {
        if (!Placing)
            return;

        Destroy(_currentShip.gameObject);

        FinishPlacement(false);
    }

    void FinishPlacement(bool success)
    {
        _currentShip.gameObject.SetActive(success);
        _currentShip.IsPlaced = success;

        if (success)
            _current++;
       
        _currentShip = null;
    }

    public bool Done()
    {
        return Current >= _ships.Length;
    }
}
