using UnityEngine;

public class PostBox : MonoBehaviour
{
	private GameData _gameData;

	[SerializeField] private ProductType _boxType;
	public ProductType BoxType
	{
		get { return _boxType; }
		protected set { }
	}

    public void Start()
    {
		_gameData = GameData.gameData;
		gameObject.SetActive(_gameData.AddIsActivated(_boxType));
	}
}
