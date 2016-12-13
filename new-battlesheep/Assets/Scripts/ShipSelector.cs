using UnityEngine;

public class ShipSelector : MonoBehaviour
{
    public GameObject ItemPrefab;
    public Transform ItemsParent;

    public static ShipSelector Main { get { return FindObjectOfType<ShipSelector>(); } }

    ShipSelectorItem CreateItem(Ship ship)
    {
        var gameObject = GameObject.Instantiate(ItemPrefab);
        var item = gameObject.GetComponent<ShipSelectorItem>();
        var transform = item.transform;

        item.SetShip(ship);

        transform.SetParent(ItemsParent);
        transform.Reset();

        Debug.Assert(item != null, "ItemPrefab must have ShipSelectorItem attached to it.");

        return item;
    }

    public ShipSelectorItem AddItem(Ship ship)
    {
        var item = CreateItem(ship);
        return item;
    }
}
