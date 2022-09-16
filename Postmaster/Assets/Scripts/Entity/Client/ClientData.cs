using UnityEngine;

[CreateAssetMenu(menuName = "Client", fileName = "New Client")]
public class ClientData : ScriptableObject
{
    [field: SerializeField] public GameObject Prefab { get; private set; }
    [HideInInspector] public ProductData Product;
}
