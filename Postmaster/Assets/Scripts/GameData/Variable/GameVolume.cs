using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class GameVolume : GameVariable
{
    [SerializeField] private Slider _slider;

    private void Start()
    {
        Init();
    }

    public override void UpdateValue()
    {
        _value = _gameData.Volume;
        _slider.value = _value;
        base.UpdateValue();
        _text.text += " %";
    }

    public void ChangeVolume()
    {
        _gameData.Volume = _slider.value;
        UpdateValue();
    }
}
