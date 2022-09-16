using UnityEngine;

public class EndRoad : Road
{
    public override void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Client client) == true)
        {
            Destroy(client.gameObject);
        }
    }
}
