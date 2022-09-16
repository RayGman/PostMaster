using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class Charisma : GameVariable
{
    private void Start()
    {
        Init();
    }

    public override void UpdateValue()
    {
        _value = _gameData.PlayerStats.Charisma;
        base.UpdateValue();
    }
}
