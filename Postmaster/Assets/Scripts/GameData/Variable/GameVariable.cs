using UnityEngine;
using UnityEngine.UI;

public class GameVariable : MonoBehaviour
{
    protected GameData _gameData;
    protected Text _text;
    protected float _value;
    private bool first = true;

    protected void Init()
    {
        _text = GetComponent<Text>();
        _gameData = GameData.gameData;
        UpdateValue();
        first = false;
    }

    public virtual void UpdateValue()
    {        
        if (_value < 0)
        {
            _value = 0.0f;
        }
        _text.text = _value.ToString();
    }

    private void OnEnable()
    {
        if (first != true)
        {
            UpdateValue();
        }
    }
}
