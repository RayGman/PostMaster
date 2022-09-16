using UnityEngine;
using UnityEngine.UI;

public class ItemView : MonoBehaviour
{
    [SerializeField] private Text _name;
    [SerializeField] private Image _itemIcon;
    [SerializeField] private Text _priceText;   
    [SerializeField] private Button _button;
    private Item _item;
    private Shop _shop;

    private void OnEnable()
    {
        _button.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnButtonClick);
    }

    public void Initialize(Item item, Shop shop)
    {
        _shop = shop;
        _item = item;
        _name.text = _item.name;
        _itemIcon.sprite = _item.Icon;
        _priceText.text = _item.Price.ToString();
    }

    private void OnButtonClick()
    {
        _shop.TryBuy(_item, this);
    }
}
