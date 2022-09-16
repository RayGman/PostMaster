using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Product : MonoBehaviour
{
    public float price { get; private set; }
    public ProductType type { get; private set; }
    public WeightType Weight { get; private set; }

    private Quaternion _startRotation;
    private BoxCollider _childBoxCollider;
    private Rigidbody _rigidbody;

    public void Init(ProductData data)
    {
        _startRotation = gameObject.transform.localRotation;
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.isKinematic = false;
        _childBoxCollider = GetComponentInChildren<BoxCollider>();
        _childBoxCollider.isTrigger = false;
        price = data.Price;
        type = data.Type;
        Weight = data.Weight;
    }

    public void ToHand()
    {
        _childBoxCollider.isTrigger = true;
        _rigidbody.isKinematic = true;
        transform.localPosition = Vector3.zero;
        transform.localRotation = _startRotation;
        transform.localScale = Vector3.one;
    }
}
