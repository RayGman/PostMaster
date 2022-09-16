using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    private GameData _gameData;
    
    [SerializeField] private ItemView _template;
    [SerializeField] private Transform _content;
    [SerializeField] private List<Item> _items;

    private void Start()
    {
        _gameData = GameData.gameData;

        foreach (var item in _items)
        {
            if (item != null)
            {
                if (AllowItem(item) == true)
                {
                    var spawnedItem = Instantiate(_template, _content);
                    spawnedItem.Initialize(item, this);
                }
            }
        }
    }

    public void TryBuy(Item item, ItemView view)
    {
        if (AllowItem(item) == true && _gameData.CashBalance >= item.Price)
        {
            _gameData.CashBalance = -item.Price;
            switch (item.Type)
            {
                case ItemType.Stats:
                    Stats stats = _gameData.PlayerStats;
                    if (item.name == "Strength")
                    {
                        stats.Strength = stats.Strength + 1;
                        if (stats.Strength >= 10)
                        {
                            Destroy(view.gameObject);
                        }
                    }
                    else if (item.name == "Charisma")
                    {
                        stats.Charisma = stats.Charisma + 1;
                        if (stats.Charisma >= 10)
                        {
                            Destroy(view.gameObject);
                        }
                    }
                    else
                    {
                        Destroy(view.gameObject);
                    }
                    _gameData.PlayerStats = stats;
                    break;
                case ItemType.ProductType:
                    _gameData.ChangeAddProduct(item.name);
                    Destroy(view.gameObject);
                    break;
                default:
                    Destroy(view.gameObject);
                    break;
            }
        }
        else if (AllowItem(item) == false)
        {
            Destroy(view.gameObject);
        }
    }

    private bool AllowItem(Item item)
    {
        switch (item.Type)
        {
            case ItemType.Stats:
                Stats stats = _gameData.PlayerStats;
                if (item.name == "Strength" && stats.Strength < 10)
                {
                    return true;
                }
                else if (item.name == "Charisma" && stats.Charisma < 10)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            case ItemType.ProductType:
                AddProductData[] addProductData = _gameData.GetProducts();
                bool allow = true;
                foreach (var add in addProductData)
                {
                    if (item.name == add.Name)
                    {
                        allow = false;
                        break;
                    }
                }
                return allow;
            default:
                return false;
        }
    }
}