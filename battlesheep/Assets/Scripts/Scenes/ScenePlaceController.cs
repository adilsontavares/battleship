using UnityEngine;

public class ScenePlaceController : MonoBehaviour
{
    public GameObject[] ShipPrefabs;
    int _currentShip = -1;

    void Start()
    {
        BoardPlacer.Main.OnPlaceItem += ItemWasPlaced;

        PlaceNextShip();
    }

    void ItemWasPlaced(BoardItem item)
    {
        PlaceNextShip();
    }

    void PlaceNextShip()
    {
        _currentShip++;

        if (_currentShip < ShipPrefabs.Length)
            PlaceShip(ShipPrefabs[_currentShip]);
    }

    void PlaceShip(GameObject prefab)
    {
        var go = (GameObject)Instantiate(prefab);
        var item = go.GetComponent<BoardItem>();

        BoardPlacer.Main.BeginPlacement(item);
    }
}