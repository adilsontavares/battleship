using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ShipSelectorItem : MonoBehaviour
{
    Button _button;

    [SerializeField]
    Image _shipImage;

    [SerializeField]
    Image _statusImage;

    Action<Ship> _callback;

    Ship _ship;

    void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(ButtonWasPressed);
    }

    public void SetCallback(Action<Ship> callback)
    {
        _callback = callback;
    }

    public void SetShip(Ship ship)
    {
        _ship = ship;
        UpdateContent();
    }

    void UpdateContent()
    {
        _shipImage.sprite = _ship.ShipSprite;
    }

    void ButtonWasPressed()
    {
        if (_callback != null)
            _callback.Invoke(_ship);
    }
}
