using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class Cash : GameVariable
{
    [Header("For game scene")]
    [SerializeField] private OrderTable _orderTable;
    //text color 2D9D28
    
    private void Start()
    {
        Init();

        if (_orderTable != null)
        {
            _orderTable.OrderComplited.AddListener(AddCash);
        }
    }

    public override void UpdateValue()
    {
        _value = _gameData.CashBalance;
        base.UpdateValue();
        _text.text += " $";
    }

    private void AddCash(float cash)
    {
        if (cash > 0)
        {
            _value = _gameData.CashBalance;
            _value += cash;
            _gameData.CashBalance = cash;
            UpdateValue();
        }
    }
}
