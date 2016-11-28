using Tween.Animation;
using Tween.Animation.Ease;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenePlaceController : MonoBehaviour
{
    [SerializeField]
    GameObject _playerBoard;

    [SerializeField]
    GameObject _enemyBoard;

    public GameObject[] ShipPrefabs;
    int _currentShip = -1;

    [SerializeField]
    Panel _waitingPlayersPanel;

    [SerializeField]
    CameraContainer _gameCameraContainer;

    void Start()
    {
        SocketManager.Main.Listener.OnStartGame += OnStartGame;
        
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
        else
            PlacementDidFinish();
    }

    void PlaceShip(GameObject prefab)
    {
        var go = (GameObject)Instantiate(prefab);
        var item = go.GetComponent<BoardItem>();

        BoardPlacer.Main.BeginPlacement(item);
    }

    void PlacementDidFinish()
    {
        SocketManager.Main.Sender.SendInitialBoard(Board.Main);
        _waitingPlayersPanel.Show();
    }

    void OnStartGame(SocketMessage message)
    {
        _waitingPlayersPanel.Hide();
        CameraController.Main.MoveToContainer(_gameCameraContainer);

        _enemyBoard.SetActive(true);
    }
}