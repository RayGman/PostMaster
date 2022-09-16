using UnityEngine;

[CreateAssetMenu(menuName = "Shop", fileName = "New Item")]
public class Item : ScriptableObject
{
    [field: SerializeField] public ItemType Type { get; private set; }
    [field: SerializeField] public Sprite Icon { get; private set; }
    [field: SerializeField] public float Price { get; private set; }
}
