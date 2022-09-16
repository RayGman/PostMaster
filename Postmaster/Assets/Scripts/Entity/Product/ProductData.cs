using UnityEngine;

[CreateAssetMenu(menuName = "Product", fileName = "New Product")]
public class ProductData : ScriptableObject
{
    [field: SerializeField] public GameObject Product { get; private set; }
    [field: SerializeField] public float Price { get; private set; }
    [field: SerializeField] public ProductType Type { get; private set; }
    [field: SerializeField] public WeightType Weight { get; private set; }
}