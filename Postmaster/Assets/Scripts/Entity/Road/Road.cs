using UnityEngine;

public class Road : MonoBehaviour
{
    [SerializeField] private Transform _nextPoint;

	public Transform WhereNext { get => _nextPoint; }

	public virtual void OnTriggerEnter(Collider other)
	{
		if (other.TryGetComponent(out Client client) == true)
		{
			client.GoTo(_nextPoint);
		}
	}
}
