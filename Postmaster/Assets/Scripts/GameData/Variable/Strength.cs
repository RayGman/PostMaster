using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class Strength : GameVariable
{
    private void Start()
    {
        Init();
    }

    public override void UpdateValue()
    {
        _value = _gameData.PlayerStats.Strength;
        base.UpdateValue();
    }
}
