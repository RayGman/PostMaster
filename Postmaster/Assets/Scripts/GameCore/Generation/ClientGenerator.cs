using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientGenerator : MonoBehaviour
{
    private GameData _gameData;

    [SerializeField] private List<ClientData> _clients = new List<ClientData>();
	[SerializeField] private ClientData _standartClient;

    [SerializeField] private List<ProductData> _products = new List<ProductData>();
    [SerializeField] private ProductData _standartProduct;

    [SerializeField] private float returnDelay;

    private void Start()
    {
        _gameData = GameData.gameData;
        AddProductData[] addDataActivated = _gameData.GetProducts();

        foreach (AddProductData addData in addDataActivated)
        {
            var addType = Resources.LoadAll<ProductData>("Products/" + addData.Name + "/");
            foreach (ProductData data in addType)
            {
                _products.Add(data);
            }
        }      
    }

    #region Generation
    public ClientData GetClient()
    {
        if (_clients.Count > 0)
        {			
            int rand = Random.Range(0, _clients.Count);

            ClientData client = _clients[rand];

            client.Product = GetRandomProduct();

            _clients.RemoveAt(rand);
			
            return client;
        }
        else
        {
            return _standartClient;
        }
    }

    private ProductData GetRandomProduct()
    {
        if (_products.Count > 0)
        {
            int rand = Random.Range(0, _products.Count);

            ProductData product = _products[rand];

            return product;
        }
        else
        {
            return _standartProduct;
        }
    }
    #endregion

    #region Return
    public void ReturnClient(ClientData data)
    {
        StartCoroutine(Return(data));
    }

    private IEnumerator Return(ClientData data)
    {
        yield return new WaitForSeconds(returnDelay);
        _clients.Add(data);
    }
    #endregion
}
