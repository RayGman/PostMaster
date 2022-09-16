using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OrderTable : MonoBehaviour
{
    public UnityEvent<float> OrderComplited = new UnityEvent<float>();

    public OrderStatus Order;
    private Product _product;
    private Transform _spawnTo;
    
    #region Order
    [SerializeField] private Renderer _board;
    [SerializeField] private Material _default;
    [SerializeField] private Material _activation;
    [Space(10)]
    [SerializeField] private MeshRenderer _orderIcon;
    [SerializeField] private List<Material> _icons;
    #endregion

    [Space(10)]
    [SerializeField] private AudioSource _audio;
    [SerializeField] private AudioClip _order;
    [SerializeField] private AudioClip _check;

    private void Start()
    {
        Order = OrderStatus.Free;
        _spawnTo = transform;
    }

    public Product GiveProduct()
    {
        if (_product != null)
        {
            Order = OrderStatus.WaitCheck;
            return _product;
        }
        else
        {
            Order = OrderStatus.Free;
            OrderComplited?.Invoke(0f);
            return null;
        }
    }

    public void TakeProduct(ProductData data)
    {
        if (Order == OrderStatus.Free)
        {
            if (data != null && data.Product != null)
            {
                GameObject prefab = Instantiate(data.Product, _spawnTo.position, data.Product.transform.rotation);
                _product = prefab.GetComponent<Product>();
                _product.Init(data);
                Order = OrderStatus.HasProduct;
                _audio.clip = _order;
                _audio.volume = 0.5f;
                _audio.Play();
            }
            else
            {
                Order = OrderStatus.Free;
                OrderComplited?.Invoke(0f);
            }
            SetTableColor();
        }
    }

    public void TakeCheck(Check check)
    {
        if (Order == OrderStatus.WaitCheck && check != null)
        {
            Order = OrderStatus.Free;
            OrderComplited?.Invoke(check.GetCash());
            SetTableColor();
            _audio.clip = _check;
            _audio.volume = 0.25f;
            _audio.Play();
        }
    }

    private void SetTableColor()
    {
        if (Order == OrderStatus.Free)
        {
            _board.material = _default;
            SetOrderIcon(true);
        }
        else
        {
            _board.material = _activation;
            SetOrderIcon(false);
        }
        
    }

    private void SetOrderIcon(bool tableIsFree)
    {
        _orderIcon.gameObject.SetActive(!tableIsFree);

        if (tableIsFree == false)
        {
            if (_product != null)
            {
                foreach (Material item in _icons)
                {
                    if (_product.type.ToString() == item.name)
                    {
                        _orderIcon.material = item;
                        break;
                    }
                }
            }
        }
    }
}